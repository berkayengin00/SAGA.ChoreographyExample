using Microsoft.EntityFrameworkCore;

namespace Stock.API.Context
{
	public class SagaStockDBContext : DbContext
	{
		public SagaStockDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Models.Stock> Stocks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Models.Stock>().HasData(
				new Models.Stock()
				{
					Id = Guid.NewGuid(),
					ProductId = Guid.Parse("0bd39273-fcc6-4167-b1e9-88fccf9dfa2e"),
					Quntity = 5
				},
				new Models.Stock()
				{
					Id = Guid.NewGuid(),
					ProductId = Guid.Parse("25bba58e-c7c5-4dca-ab2f-6ca0fbc98f09"),
					Quntity = 14
				},
				new Models.Stock()
				{
					Id = Guid.NewGuid(),
					ProductId = Guid.Parse("7e8e9773-2670-4e06-9115-f1e4aa8acf22"),
					Quntity = 13
				}
			);
		}
	}
}
