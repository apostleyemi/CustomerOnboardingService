using AutoMapper;
using CustomerOnboardingService.DTOs;
using CustomerOnboardingService.Models;

namespace CustomerOnboardingService.Utilities
{
	public class MappingProfile :Profile
	{

        public MappingProfile()
        {

			// customer 
			CreateMap<CustomerDTO, Customer>();
			CreateMap<Customer, CustomerDTO>();


			// state and Local government 
			CreateMap<StateList, StateListDTO>()
			.ForMember(dest => dest.LocalgovernmentList,
			opt => opt.MapFrom(src => src.lacalGovernments));
			CreateMap<LacalGovernment, LocalgovernmentDto>();
			CreateMap<StateListDTO, StateList>()
				.ForMember(dest => dest.lacalGovernments,
				opt => opt.MapFrom(src => src.LocalgovernmentList));
			CreateMap<LocalgovernmentDto, LacalGovernment>();

		}
    }
}
