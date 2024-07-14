using CustomerOnboardingService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerOnboardingService.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Customer>customers { get; set; }
		public DbSet<StateList> stateLists { get; set; }
		public DbSet<OtpVerification> otpVerifications { get; set; }
	}
}
