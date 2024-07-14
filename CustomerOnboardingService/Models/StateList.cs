namespace CustomerOnboardingService.Models
{
	public class StateList
	{
		public int Id { get; set; }
		public string StateName { get; set; }

		public ICollection<LacalGovernment> lacalGovernments { get; set; }

	}
}