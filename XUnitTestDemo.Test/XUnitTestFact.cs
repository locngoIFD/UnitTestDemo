using UnitTestDemo.Models;

namespace XUnitTestDemo.Test
{
	public class XUnitTestFact : IClassFixture<CalculationServiceFixture>, IDisposable
	{
		private CalculationServiceFixture _fixture;

		public XUnitTestFact(CalculationServiceFixture fixture)
		{
			_fixture = fixture;
		}

		public void Dispose()
		{
			_fixture = null;
		}

		[Fact]
		public void CalculateCostElkSingle()
		{
			CalculationRequest request = new CalculationRequest
			{
				IsElk = true,
				TargetMonth = new DateTime(2025, 1, 1),
				PriceSingle = 0.5m,
				OnPeak = 10,
				OffPeak = 10,
				ReturnOnPeak = 15,
				ReturnTariff = 0.5m,
				TaxPercent = new List<decimal> { 1 }

			};
			decimal result = _fixture._service.CalculateCostElk(request);
			Assert.Equal(4.2m, result);

			//A cheat sheet of Asserts for xUnit.net in C#
			//https://gist.github.com/jonesandy/f622874e78d9d9f356896c4ac63c0ac3
		}
	}
}