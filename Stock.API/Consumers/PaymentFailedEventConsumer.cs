using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Stock.API.Context;

namespace Stock.API.Consumers
{
	public class PaymentFailedEventConsumer(SagaStockDBContext _context) : IConsumer<PaymentFailedEvent>
	{
		public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
		{
			var stocks = (await _context.Stocks.ToListAsync()).Where(x => context.Message.OrderItemMessages.Any(y => y.ProductId == x.ProductId)).ToList() ;

			if (stocks!=null)
			{
				stocks.ForEach(x=>
				{
					var orderItemMessage = context.Message.OrderItemMessages.FirstOrDefault(y=>y.ProductId==x.ProductId);

					if (orderItemMessage!=null)
					{
						x.Quntity += orderItemMessage.Quantity;
					}
				});
				await _context.SaveChangesAsync();
			}
		}
	}
}
