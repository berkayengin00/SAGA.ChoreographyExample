namespace Order.API.ViewModels
{
	public class CreateOrderVM
	{
		public Guid BuyerId { get; set; }
		public List<CreateOrderItemVM> CreateOrderItems { get; set; }
	}
	public class CreateOrderItemVM
	{
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
