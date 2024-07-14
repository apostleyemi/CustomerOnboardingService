using System.Numerics;

namespace CustomerOnboardingService.Models
{
	public class Customer
	{
		//	phone Number, email, password,
		//state of residence, and LGA
		public int Id { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public bool IsVarified { get; set; } = false;

        public int LgaId { get; set; }

	}
}