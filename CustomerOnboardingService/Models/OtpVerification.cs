namespace CustomerOnboardingService.Models
{
	public class OtpVerification
	{
        public int Id { get; set; }
        public string Otp { get; set; }
        public string Email { get; set; }

        public bool IsValid { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ValidTill { get; set; }
    }
}
