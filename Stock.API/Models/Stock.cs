﻿namespace Stock.API.Models
{
	public class Stock
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public int Quntity { get; set; }
	}
}
