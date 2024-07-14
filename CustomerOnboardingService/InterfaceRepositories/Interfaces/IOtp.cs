using CustomerOnboardingService.DTOs;
using CustomerOnboardingService.Models;

namespace CustomerOnboardingService.InterfaceRepositories.Interfaces
{
	public interface IOtp
	{
		string SendOtp(string otp);
		//Task<string> VerifyOtp(OtpValidatorDTO model);
		Task<string> InvalidateOtp(VerifyCustomerDto model);
		Task<string> SaveOtp(Customer model, string otpGenerated);
		Task<string> GenerateNewOtp(OtpValidatorDTO model);
	}
}
