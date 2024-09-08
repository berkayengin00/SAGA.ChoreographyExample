namespace Shared.Events
{
	public class StockNotReservedEvent
	{
		public Guid OrderId { get; set; }
		public string Message { get; set; }
	}
}
