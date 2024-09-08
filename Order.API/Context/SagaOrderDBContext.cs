﻿using Microsoft.EntityFrameworkCore;

namespace Order.API.Context
{
	public class SagaOrderDBContext : DbContext
	{
		public SagaOrderDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Order.API.Models.Order> Orders { get; set; }
		public DbSet<Order.API.Models.OrderItem> OrderItems { get; set; }
	}
}
