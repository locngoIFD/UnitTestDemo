using Microsoft.Extensions.Logging;
using Moq;
using UnitTestDemo.Services;

namespace XUnitTestDemo.Test
{
	public class CalculationServiceFixture
	{
		public CalculationServiceFixture()
		{
			var mockHttp = new Mock<ILogger<CalculationService>>();
			service = new CalculationService(mockHttp.Object);
		}

		public CalculationService service { get; private set; }
	}
}
