using CustomerOnboardingService.DTOs;

namespace CustomerOnboardingService.InterfaceRepositories.Interfaces
{
	public interface ICustomer
	{
		Task<CustomerDTO> OnBoardCustomer(CustomerDTO model);
		Task<List<CustomerDTO>> GetOnBoardedCustomer();

		Task<string> VerifyCustomer(VerifyCustomerDto model);


	}
}
