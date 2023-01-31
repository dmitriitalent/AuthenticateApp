using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
	public class Database : DbContext
	{
		public DbSet<User> Users { get; set; } = null!;
		public Database(DbContextOptions<Database> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
