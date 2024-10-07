using Microsoft.AspNetCore.Mvc;
using OE.Tudasbazis.Logic.Services;

namespace OE.Tudasbazis.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly TestService _testService;

		public TestController(TestService testService)
		{
			_testService = testService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			string result = await _testService.Get();

			return Ok(result);
		}
	}
}
