using Microsoft.Extensions.Logging;
using Moq;
using UnitTestDemo.Models;
using UnitTestDemo.Services;

namespace XUnitTestDemo.Test
{
	public class XUnitTest
	{
		private CalculationService _service;

		public XUnitTest()
		{
			var mockHttp = new Mock<ILogger<CalculationService>>();
			_service = new CalculationService(mockHttp.Object);
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
			decimal result = _service.CalculateCostElk(request);
			Assert.Equal(4.2m, result);

			//A cheat sheet of Asserts for xUnit.net in C#
			//https://gist.github.com/jonesandy/f622874e78d9d9f356896c4ac63c0ac3
			//https://xunit.net/docs/comparisons
		}

		//In case of parallel test is needed
		//https://github.com/meziantou/Meziantou.Xunit.ParallelTestFramework
	}
}