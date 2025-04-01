using UnitTestDemo.Models;

namespace XUnitTestDemo.Test
{
	public class XUnitTestTheory : IClassFixture<CalculationServiceFixture>
	{
		private CalculationServiceFixture _fixture;

		public XUnitTestTheory(CalculationServiceFixture fixture)
		{
			_fixture = fixture;
		}

		[Theory]
		[InlineData(10, 10, 0.5, 15, 0.5, 4.2)]
		[InlineData(10, 10, 0.5, 20, 0.5, 0)]
		public void CalculateCostElkSingle(decimal onPeak, decimal offPeak, decimal price, decimal production, decimal tariff, decimal result)
		{
			CalculationRequest request = new CalculationRequest
			{
				IsElk = true,
				OnPeak = onPeak,
				OffPeak = offPeak,
				PriceSingle = price,
				ReturnTariff = tariff,
				ReturnOnPeak = production,
				TaxPercent = new List<decimal> { 1 },
				TargetMonth = new DateTime(2025, 1, 1)

			};
			Assert.Equal(result, _fixture.service.CalculateCostElk(request));
		}
	}
}