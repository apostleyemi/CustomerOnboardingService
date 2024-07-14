using CustomerOnboardingService.DTOs;

namespace CustomerOnboardingService.InterfaceRepositories.Interfaces
{
	public interface IStateLocalgov
	{

		Task<ICollection<StateListDTO>> GetStateLocalGov();
		Task<StateListDTO> GetLocalGovernmentByStateId(int stateId);
	}
}
