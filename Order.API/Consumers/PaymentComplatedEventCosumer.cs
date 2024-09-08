using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Context;
using Shared.Events;

namespace Order.API.Consumers
{
	public class PaymentComplatedEventCosumer(SagaOrderDBContext _context) : IConsumer<PaymentComplatedEvent>
	{
		public async Task Consume(ConsumeContext<PaymentComplatedEvent> context)
		{
			var order =await _context.Orders.FirstOrDefaultAsync(x=>x.Id==context.Message.OrderId);
			if (order!=null)
			{
				order.OrderStatu = Enum.OrderStatus.Complated;
				await _context.SaveChangesAsync();	
			}
		}
	}
}
