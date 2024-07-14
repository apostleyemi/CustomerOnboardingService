namespace CustomerOnboardingService.DTOs
{
	public class StateListDTO
	{
		public int Id { get; set; }
		public string StateName { get; set; }

		public ICollection<LocalgovernmentDto> LocalgovernmentList { get; set; }
	}
}
