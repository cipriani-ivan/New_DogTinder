using NewDogTinder.EFDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace NewDogTinder.EFDataAccessLibrary.DataAccess
{
	public class NewDogTinderContext : DbContext
	{
		public NewDogTinderContext(DbContextOptions options) : base(options) 
		{ 
		}

		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Dog> Dogs { get; set; }
		public DbSet<Owner> Owners { get; set; }
		public DbSet<Place> Places { get; set; }
	}
}
