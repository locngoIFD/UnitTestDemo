using Microsoft.Extensions.Logging;
using Moq;
using UnitTestDemo.Services;

namespace XUnitTestDemo.Test
{
	public class CalculationServiceFixture : IDisposable
	{
		public CalculationService CalculationService { get; private set; }

		public CalculationServiceFixture()
		{
			var mockHttp = new Mock<ILogger<CalculationService>>();
			CalculationService = new CalculationService(mockHttp.Object);
		}

		public void Dispose()
		{
			CalculationService = null;
		}
	}
}
