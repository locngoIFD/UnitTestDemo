using Microsoft.Extensions.Logging;
using Moq;
using UnitTestDemo.Services;

namespace XUnitTestDemo.Test
{
	public class CalculationServiceFixture : IDisposable
	{
		public CalculationService _service { get; private set; }

		public CalculationServiceFixture()
		{
			var mockHttp = new Mock<ILogger<CalculationService>>();
			_service = new CalculationService(mockHttp.Object);
		}

		public void Dispose()
		{
			_service = null;
		}
	}
}
