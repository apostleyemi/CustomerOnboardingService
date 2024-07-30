using AutoMapper;
using CustomerOnboardingService.Data;
using CustomerOnboardingService.DTOs;
using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace CustomerOnboardingService.InterfaceRepositories.Repositories
{
	public class StateLocalGovRepository : IStateLocalgov
	{
		private readonly AppDbContext _dbContext;
		private readonly IMapper _mapper;
		public StateLocalGovRepository(AppDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<StateListDTO> GetLocalGovernmentByStateId(int stateId)
		{
			var statedata=await _dbContext
				.stateLists.Where(s=>s.Id==stateId)
				.Include(l=>l.lacalGovernments).FirstOrDefaultAsync();


			var stateAndLocalGovs =
				_mapper.Map<StateListDTO>(statedata);
			/*var stateAndLocalGov = new StateListDTO();


			stateAndLocalGov.Id = statedata.Id;
			stateAndLocalGov.StateName = statedata.StateName;

			var localGovCollection=new Collection<LocalgovernmentDto>();

			foreach (var localgov in stateAndLocalGov.LocalgovernmentList)
			{
				var localgovDto=new LocalgovernmentDto();


				localgovDto.Id = localgov.Id;
				localgovDto.LgaName = localgov.LgaName;

				localGovCollection.Add(localgovDto);
			}*/

			/*	stateAndLocalGov.LocalgovernmentList = localGovCollection;*/


			return stateAndLocalGovs;
		}

		public async Task<ICollection<StateListDTO>> GetStateLocalGov()
		{
			var stateData = await _dbContext.stateLists
				.Include(l => l.lacalGovernments).ToListAsync();

			var stateDtoList =_mapper.Map<List<StateListDTO>>(stateData);
			//var stateDtoList = new Collection<StateListDTO>();

			/*foreach (var state in stateData)
			{
				var stateDto = new StateListDTO();
				stateDto.StateName = state.StateName;
				stateDto.Id = state.Id;

				var stateLocaGovList = new Collection<LocalgovernmentDto>();

				foreach (var statelocal in state.lacalGovernments)
				{
					LocalgovernmentDto localGov = new LocalgovernmentDto();
					localGov.Id = statelocal.Id;
					localGov.LgaName = statelocal.LgaName;

					stateLocaGovList.Add(localGov);
				}


				stateDto.LocalgovernmentList = stateLocaGovList;

				stateDtoList.Add(stateDto);
			}*/

			return stateDtoList;
		}

	}
}
