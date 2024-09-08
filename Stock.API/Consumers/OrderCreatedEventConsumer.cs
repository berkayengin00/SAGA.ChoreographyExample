using MassTransit;
using MassTransit.Transports;
using Shared;
using Shared.Events;
using Stock.API.Context;

namespace Stock.API.Consumers
{
	public class OrderCreatedEventConsumer(SagaStockDBContext _dbContext , ISendEndpointProvider _sendEndpointProvider,IPublishEndpoint _publishEndpoint) : IConsumer<OrderCreatedEvent>
	{

		public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
		{
			var dbStocks = _dbContext.Stocks.ToList().Where(x=> context.Message.OrderItemMessages.Any(y=>y.ProductId==x.ProductId)).ToList();

			var notExistProductCount =  dbStocks.Where(x=> context.Message.OrderItemMessages.Any(y=>y.ProductId==x.ProductId && y.Quantity>x.Quntity)).ToList();

			var willUpdateStockList = new List<Stock.API.Models.Stock>();

			if (notExistProductCount?.Count==0)
			{
				context.Message.OrderItemMessages.ForEach(x =>
				{
					var updatedStock = dbStocks.FirstOrDefault(y => y.ProductId == x.ProductId);

					if (updatedStock !=null)
					{
						updatedStock.Quntity -= x.Quantity;
						willUpdateStockList.Add(updatedStock);
					}

				});

				if (willUpdateStockList.Count>0)
				{
					_dbContext.UpdateRange(willUpdateStockList);
					await _dbContext.SaveChangesAsync();
				}
				var sendEndPoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.Payment_StockReservedEventQueue}"));

				var stockReservedEvent = new StockReservedEvent()
				{
					BuyerId = context.Message.BuyerId,
					OrderId = context.Message.OrdeId,
					TotalPrice = context.Message.TotalPrice,
					OrderItemMessages = context.Message.OrderItemMessages,
				};

				await sendEndPoint.Send<StockReservedEvent>(stockReservedEvent);

				

			}
			else
			{
				var stockReservedEvent = new StockNotReservedEvent()
				{
					OrderId = context.Message.OrdeId,
					Message = "Ürün Stoğu Yeterli Değil"
				};

				await _publishEndpoint.Publish(stockReservedEvent);
			}

		}
	}
}
