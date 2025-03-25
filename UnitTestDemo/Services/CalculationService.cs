using UnitTestDemo.Models;
using UnitTestDemo.Shared;

namespace UnitTestDemo.Services
{
	public class CalculationService
	{
		private readonly ILogger<CalculationService> _logger;
		private readonly List<EnergyTax> _energyTaxes = new List<EnergyTax>
		{
			new EnergyTax
			{
				StartDate = DateTime.MinValue,
				EndDate = new DateTime(2024, 12, 31),
				Zone = 0,
				Cost = 0.5m,
			},
			new EnergyTax
			{
				StartDate = DateTime.MinValue,
				EndDate = new DateTime(2024, 12, 31),
				Zone = 5000,
				Cost = 1,
			},
			new EnergyTax
			{
				StartDate = new DateTime(2025, 1, 1),
				EndDate = new DateTime(2025, 12, 31),
				Zone = 0,
				Cost = 0.1m,
			},
			new EnergyTax
			{
				StartDate = new DateTime(2025, 1, 1),
				EndDate = new DateTime(2025, 12, 31),
				Zone = 10000,
				Cost = 1,
			},
			new EnergyTax
			{
				IsElk = true,
				StartDate = new DateTime(2025, 1, 1),
				EndDate = new DateTime(2025, 12, 31),
				Zone = 0,
				Cost = 0.2m,
			},
			new EnergyTax
			{
				IsElk = true,
				StartDate = new DateTime(2025, 1, 1),
				EndDate = new DateTime(2025, 12, 31),
				Zone = 10000,
				Cost = 1,
			},
			new EnergyTax
			{
				StartDate = new DateTime(2026, 1, 1),
				EndDate = DateTime.MaxValue,
				Zone = 0,
				Cost = 0,
			},
			new EnergyTax
			{
				StartDate = new DateTime(2026, 1, 1),
				EndDate = DateTime.MaxValue,
				Zone = 1000,
				Cost = 1,
			}
		};

		public CalculationService(ILogger<CalculationService> logger)
		{
			_logger = logger;
		}

		public decimal CalculateCostGas(CalculationRequest request)
		{
			List<EnergyTax> energyTaxes = GetEnergyTaxes(request.IsElk, request.TargetMonth);

			decimal dependentCost = 0;
			string formula = String.Empty;

			decimal variable = request.OnPeak;

			decimal price = request.PriceSingle;
			if (request.Category == 1)
			{
				price = request.PriceOnPeak;
			}
			else if (request.Category == 2)
			{
				price = request.PriceOffPeak;
			}
			dependentCost += request.OnPeak * price;

			var taxedAmount = CalculateDependentTax(variable, energyTaxes, request.TaxPercent);
			dependentCost += taxedAmount;

			dependentCost += dependentCost * Constants.VAT;
			return dependentCost;
		}

		public decimal CalculateCostElk(CalculationRequest request)
		{
			List<EnergyTax> energyTaxes = GetEnergyTaxes(request.IsElk, request.TargetMonth);

			decimal dependentCost = 0;
			decimal returnTariff = request.ReturnTariff.GetValueOrDefault(Constants.RETURN_TARIFF);
			if (request.IsDouble)
			{
				decimal returnUsage = request.ReturnOnPeak + request.ReturnOffPeak;

				dependentCost += request.OnPeak * request.PriceOnPeak;

				dependentCost += request.OffPeak * request.PriceOffPeak;

				dependentCost -= returnUsage * returnTariff;
			}
			else
			{
				decimal usage = request.OnPeak + request.OffPeak;
				decimal returnUsage = request.ReturnOnPeak + request.ReturnOffPeak;
				dependentCost += usage * request.PriceSingle;
				dependentCost -= returnUsage * returnTariff;
			}
			var taxedAmount = CalculateDependentTax(request.OnPeak + request.OffPeak - request.ReturnOnPeak - request.ReturnOffPeak, energyTaxes, request.TaxPercent);
			dependentCost += taxedAmount;
			dependentCost += dependentCost* Constants.VAT;
			return dependentCost;
		}

		public List<EnergyTax> GetEnergyTaxes(bool isElk, DateTime targetMonth)
		{
			return _energyTaxes.Where(x => x.IsElk == isElk && x.StartDate <= targetMonth && targetMonth <= x.EndDate).ToList();
		}

		public decimal CalculateDependentTax(decimal usage, bool isElk, DateTime targetMonth, List<decimal> taxPercent)
		{
			List<EnergyTax> energyTaxes = GetEnergyTaxes(isElk, targetMonth);
			return CalculateDependentTax(usage, energyTaxes, taxPercent);
		}

		private decimal CalculateDependentTax(decimal usage, List<EnergyTax> energyTaxes, List<decimal> taxPercent)
		{
			decimal taxedAmount = 0;
			if (usage <= 0)
			{
				return taxedAmount;
			}
			foreach (var zone in energyTaxes.OrderBy(p => p.Zone).Select((value, i) => (value, i)))
			{
				if (zone.i < taxPercent.Count())
				{
					taxedAmount += usage * taxPercent[zone.i] * zone.value.Cost;
				}
				else
				{
					break;
				}
			}
			return taxedAmount;
		}
	}
}
