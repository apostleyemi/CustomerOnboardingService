namespace CustomerOnboardingService.Models
{
	public class LacalGovernment
	{
		public int Id { get; set; }
		public string LgaName { get; set; }

		public int StateListId { get; set; }
		public StateList StateList { get; set; }
	}
}