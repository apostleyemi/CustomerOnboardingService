using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOnboardingService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StateLocalGovController : ControllerBase
	{
		private readonly IStateLocalgov _stateLocalgov;

		public StateLocalGovController(IStateLocalgov stateLocalgov)
		{
			_stateLocalgov = stateLocalgov;
		}

		[HttpGet]
		[Route("GetStateAndLocalGovernment")]
		public async Task<IActionResult> GetStateAndLocalGovernment() {

			var response = await _stateLocalgov.GetStateLocalGov();

			return Ok(response);
		
		}
		[HttpGet]
		[Route("GetStateAndLocalGovernmentByStateId")]
		public async Task<IActionResult> GetStateAndLocalGovernmentByStateId(int stateId)
		{

			var response = await _stateLocalgov.GetLocalGovernmentByStateId(stateId);

			return Ok(response);

		}
	}
}
