using Order.API.Enum;

namespace Order.API.Models
{
	public class Order
	{
		public Guid Id { get; set; }
		public Guid BuyerId { get; set; }
		public OrderStatus OrderStatu { get; set; }
		public DateTime CreatedDate { get; set; }
		public decimal TotalPrice { get; set; }

		public virtual ICollection<OrderItem> OrderItems { get; set; }
	}
}
