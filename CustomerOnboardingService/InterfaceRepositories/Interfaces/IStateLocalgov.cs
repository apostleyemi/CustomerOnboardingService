using CustomerOnboardingService.DTOs;

namespace CustomerOnboardingService.InterfaceRepositories.Interfaces
{
	public interface IStateLocalgov
	{

		Task<ICollection<StateListDTO>> GetStateLocalGov();
		Task<List<LocalgovernmentDto>> GetLocalGovernmentByStateId(int stateId);
	}
}
