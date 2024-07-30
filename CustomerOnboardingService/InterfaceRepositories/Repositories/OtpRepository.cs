using CustomerOnboardingService.Data;
using CustomerOnboardingService.DTOs;
using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using CustomerOnboardingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CustomerOnboardingService.InterfaceRepositories.Repositories
{
	public class OtpRepository : IOtp
	{
		private readonly ILogger<OtpRepository> _logger;
		private readonly AppDbContext _dbContext;

		public OtpRepository(ILogger<OtpRepository> logger, AppDbContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		public async Task<string> InvalidateOtp(VerifyCustomerDto model)
		{
			var otpData =await _dbContext.otpVerifications
				.Where(o=>o.Email == model.Email).FirstOrDefaultAsync();

			if (otpData == null) { return "The specified Otp does not exist"; }

			// update otp status 

			otpData.IsValid = false;

			_dbContext.otpVerifications.Update(otpData);
			var resp=await _dbContext.SaveChangesAsync()>0;
			if(resp) { return "Otp Invalidated "; }

			return null;


			
		}

		public  string  SendOtp(string otp)
		{
			try
			{
				_logger.LogInformation("OTP " + DateTime.Now + "Code generated:  " + otp);
				return "Otp Sent ";
			}
			catch (Exception ex)
			{

                Console.WriteLine(	ex);
            }
			return "";
		}


		/*public  async Task<string> VerifyOtp(OtpValidatorDTO model)
		{
			var validOtpData = await _dbContext.otpVerifications
				.Where(e=>e.Email== model.Email).FirstOrDefaultAsync();

			if(validOtpData.IsValid==true && validOtpData.ValidTill <=DateTime.Now 
				&& validOtpData.Otp==model.Otp && validOtpData.Email==model.Email)
			{


				// update the opt IsValid to false
				validOtpData.IsValid = false;

				_dbContext.otpVerifications.Update(validOtpData);

			    var result=	await _dbContext.SaveChangesAsync() >0;
				if(result) return "Otp validated";
			}

			return "Faled! Otp not valid ";
			
		  
		 
			

		}*/

		public async Task<string> GenerateNewOtp(GetOtpDTO model)
		{
			var customerData = await _dbContext.customers
				.Where(e => e.Email == model.Email).FirstOrDefaultAsync();

			if (customerData.IsVarified == true)
			{ return "Customer Already verified"; }

			var newOtp=GenerateOtp();

			// log the otp with customer details 
			var otpGenerated = new OtpVerification
			{
				Email = model.Email,
				IsValid = false,
				Otp = newOtp,
				CreatedAt = DateTime.Now,
				ValidTill = DateTime.Now.AddMinutes(30)

			};

		  var  otpdata=	await _dbContext.otpVerifications.AddAsync(otpGenerated);

			var result = await _dbContext.SaveChangesAsync();


			return newOtp;





		}
		public string GenerateOtp()
		{
			Random randN = new Random();

			return randN.Next(5000, 100000).ToString();
		}

		public async Task<string> SaveOtp(Customer model, string otpGenerated)
		{

			// save  otp  
			var otpData = new OtpVerification
			{
				CreatedAt = DateTime.Now,
				Email = model.Email,
				IsValid = true,
				Otp = otpGenerated,
				ValidTill = DateTime.Now.AddMinutes(10),

			};

			await _dbContext.otpVerifications.AddAsync(otpData);
			var UpdateResult = await _dbContext.SaveChangesAsync() > 0;

			// send otp generated 
			var otpSentResult = SendOtp(otpGenerated);

			return "Success!";
		}
	}
}
