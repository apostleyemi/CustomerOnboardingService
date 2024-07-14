namespace CustomerOnboardingService.DTOs
{
	public class BankDto
	{
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }
	public class BankResponse
	{
		public List<BankDto> Result { get; set; }
	}
}
