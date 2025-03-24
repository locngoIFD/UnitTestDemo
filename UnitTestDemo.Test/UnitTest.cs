using Microsoft.Extensions.Logging;
using Moq;
using UnitTestDemo.Models;
using UnitTestDemo.Services;

namespace UnitTestDemo.Test
{
	public class Tests
	{
		private CalculationService _service;

		[SetUp]
		public void Setup()
		{
			var mockHttp = new Mock<ILogger<CalculationService>>();
			_service = new CalculationService(mockHttp.Object);
		}

		[Test]
		public void CalculateCostGas()
		{
			CalculationRequest request = new CalculationRequest
			{
				TargetMonth = DateTime.Now,
				PriceSingle = 0.5m,
				OnPeak = 10,
				TaxPercent = new List<decimal> { 0.9m, 0.1m }

			};
			decimal result = _service.CalculateCostGas(request);
			Assert.That(result, Is.EqualTo(8.28m));
		}

		[Test]
		public void CalculateCostElk()
		{
			CalculationRequest request = new CalculationRequest
			{
				IsElk = true,
				IsDouble = true,
				TargetMonth = DateTime.Now,
				PriceOnPeak = 1,
				PriceOffPeak = 0.25m,
				OnPeak = 10,
				OffPeak = 10,
				ReturnOnPeak = 15,
				ReturnTariff = 0.5m,
				TaxPercent = new List<decimal> { 1 }

			};
			decimal result = _service.CalculateCostElk(request);
			Assert.That(result, Is.EqualTo(7.2m));
		}
	}
}