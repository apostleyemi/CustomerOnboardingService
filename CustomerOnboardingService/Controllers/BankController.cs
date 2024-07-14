using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOnboardingService.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class BankController : ControllerBase
	{
		private readonly IBank _bank;

		public BankController(IBank bank)
		{
			_bank = bank;
		}

		[HttpGet]
		[Route("GetBankList")]
		public async Task<IActionResult> GetBankList()
		{
			var response= await _bank.GetBankList();
			return Ok(response);

		}
	}
}
