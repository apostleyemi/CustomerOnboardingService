namespace CustomerOnboardingService.DTOs
{
	public class CustomerDTO
	{
        public int Id { get; set; }
        public string Phone { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public bool IsVarified { get; set; } = false;


		public int LgaId { get; set; }

	}

	public class OtpValidatorDTO
	{
        public string Otp { get; set; }
        public string Email { get; set; }
    }
}
