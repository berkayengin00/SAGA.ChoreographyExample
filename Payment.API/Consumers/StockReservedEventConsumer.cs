using MassTransit;
using Shared.Events;

namespace Payment.API.Consumers
{
	public class StockReservedEventConsumer(IPublishEndpoint _publishEndpoint) : IConsumer<StockReservedEvent>
	{
		public async Task Consume(ConsumeContext<StockReservedEvent> context)
		{
			if (false)
			{
				await _publishEndpoint.Publish(new PaymentComplatedEvent()
				{
					OrderId = context.Message.OrderId
				});
			}
			else
			{
				var paymentFailedEvent = new PaymentFailedEvent()
				{
					OrderId = context.Message.OrderId,
					OrderItemMessages = context.Message.OrderItemMessages,
					Message = "Ödeme Alınırken Bir Hatayla Karşılaşıldı"
				};

				await _publishEndpoint.Publish(paymentFailedEvent);
			}
		}
	}
}
