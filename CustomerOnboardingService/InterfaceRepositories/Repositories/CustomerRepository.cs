using AutoMapper;
using CustomerOnboardingService.Data;
using CustomerOnboardingService.DTOs;
using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using CustomerOnboardingService.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CustomerOnboardingService.InterfaceRepositories.Repositories
{
	public class CustomerRepository : ICustomer
	{
		private readonly AppDbContext _dbContext;
		private readonly ILogger<CustomerRepository> _logger;
		private readonly IOtp _otpService;
		private readonly IMapper _mapper;


		public CustomerRepository(AppDbContext dbContext, ILogger<CustomerRepository> logger, IOtp otpservice, IMapper mapper)
		{
			_dbContext = dbContext;
			_logger = logger;
			_otpService = otpservice;
			_mapper = mapper;
		}

		public async Task<CustomerDTO> OnBoardCustomer(CustomerDTO model)
		{
			try
			{
				var IsCustomerIsExist = await _dbContext.customers
			.Where(c => c.Email == model.Email).FirstOrDefaultAsync();


				if (IsCustomerIsExist != null)
				{
					return null;
				}

				// use auto mapper here 
				var newCustomer = _mapper.Map<Customer>(model);

				/*var newCustomer = new Customer
				{
					Email = model.Email,
					LgaId = model.LgaId,
					Password= BCrypt.Net.BCrypt.HashPassword(model.Password),
					Phone = model.Phone,			

				};*/

				await _dbContext.customers.AddAsync(newCustomer);
				var result =_dbContext.SaveChanges();
				if(result >0)
				{
					var OtpVerifyModel = new OtpValidatorDTO
					{
						Email = model.Email,
						Otp = ""
					};
					// generate otp 
					var otpGenerated = _otpService.GenerateNewOtp(OtpVerifyModel);
					string response=await _otpService.SaveOtp(newCustomer, otpGenerated.ToString());
					if (response == "Success!")
					{
						return _mapper.Map<CustomerDTO>(newCustomer);

					}



				}
				// failed to register 
				return null;
			}
			catch (Exception ex)
			{
                Console.WriteLine(	ex);

            }
			return null;


		}

		

		public async Task<List<CustomerDTO>> GetOnBoardedCustomer()
		{

			var customers = await _dbContext.customers
				.Where(c => c.IsVarified == true).ToListAsync();
			if(customers.Count == 0) { return null; }

			var customerDtoList = _mapper.Map<List<CustomerDTO>>(customers);

			/*var customerDtoList=new List<CustomerDTO>();

			foreach (var custom in customers)
			{
				var Customer=new CustomerDTO
				{
					Email = custom.Email,
					IsVarified = custom.IsVarified,
					Id = custom.Id,
					LgaId = custom.LgaId,
					Phone= custom.Phone,
					

				};

				customerDtoList.Add(Customer);

			}*/

			return customerDtoList;



		}

		

		public async Task<string> VerifyCustomer(VerifyCustomerDto model)
		{

			var customerIsVarified = await _dbContext.customers
				.Where(c => c.Email == model.Email).FirstOrDefaultAsync();
			if (customerIsVarified == null) { return "Not Found"; }

			if (customerIsVarified.IsVarified) { return "Already verified"; }

			// check verification code 

			var OtpCheck =await _dbContext.otpVerifications
				.Where(o=>o.Email == model.Email).FirstOrDefaultAsync();

			if (!OtpCheck.IsValid || OtpCheck.ValidTill < DateTime.Now 
				|| OtpCheck.Otp !=model.OtpReceived)
			{ return "Otp is no longer valid, rquest for new"; }


			if (OtpCheck.IsValid && OtpCheck.ValidTill >= DateTime.Now
				&& OtpCheck.Otp == model.OtpReceived)
			{
				// update Isverified 
				customerIsVarified.IsVarified = true;

				_dbContext.customers.Update(customerIsVarified);
				var verificationResult = await _dbContext.SaveChangesAsync() > 0;

				// invalidate the code 




				if (verificationResult) {
					await _otpService.InvalidateOtp(model);
					
					return "Customer Successfully verified"; }
			}

			return "failed";


		}
	}
}
