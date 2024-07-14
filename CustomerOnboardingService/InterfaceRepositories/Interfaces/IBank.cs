using CustomerOnboardingService.DTOs;

namespace CustomerOnboardingService.InterfaceRepositories.Interfaces
{
	public interface IBank
	{
		Task<List<BankDto>> GetBankList();
	}
}
