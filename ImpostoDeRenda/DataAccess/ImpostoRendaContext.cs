using ImpostoDeRenda.Model;
using Microsoft.EntityFrameworkCore;


namespace ImpostoDeRenda.DataAccess
{
	public class ImpostoRendaContext : DbContext
	{
		public DbSet<Contribuinte> Contribuintes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"data source=localhost\SQLEXPRESS; initial catalog=Sudden;persist security info=True;user id=sa;password=teste");
		}

	}
}