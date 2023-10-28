using Microsoft.EntityFrameworkCore;
using AppSurveyTrustspot.Model;

namespace AppSurveyTrustspot.Model
{
	public class TrustspotContext : DbContext
	{
		public TrustspotContext(DbContextOptions<TrustspotContext> options) : base(options)
		{

		}
		public DbSet<App> Apps { get; set; }
		public DbSet<Display> Displays { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
