using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Context;
using Shared.Events;

namespace Order.API.Consumers
{
	public class PaymentFailedEventConsumer(SagaOrderDBContext _context) : IConsumer<PaymentFailedEvent>
	{
		public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
		{
			var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == context.Message.OrderId);
			if (order != null)
			{
				order.OrderStatu = Enum.OrderStatus.Fail;
				await _context.SaveChangesAsync();
			}
		}
	}
}
