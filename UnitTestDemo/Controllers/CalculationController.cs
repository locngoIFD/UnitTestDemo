using Microsoft.AspNetCore.Mvc;
using UnitTestDemo.Models;
using UnitTestDemo.Services;

namespace UnitTestDemo.Controllers
{
	[ApiController]
	[Route("calculation")]
	public class CalculationController : ControllerBase
	{
		private readonly ILogger<CalculationController> _logger;
		private readonly CalculationService _service;

		public CalculationController(ILogger<CalculationController> logger, CalculationService service)
		{
			_logger = logger;
			_service = service;
		}

		[HttpPost("gas")]
		public decimal CalculationForGas([FromBody] CalculationRequest request)
		{
			decimal result = _service.CalculateCostGas(request);
			return result;
		}

		[HttpPost("elk")]
		public decimal CalculationForElk([FromBody] CalculationRequest request)
		{
			decimal result = _service.CalculateCostGas(request);
			return result;
		}
	}
}
