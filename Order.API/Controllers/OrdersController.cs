using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Context;
using Order.API.Models;
using Order.API.ViewModels;
using Shared.Events;
using Shared.Messages;

namespace Order.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly SagaOrderDBContext _context;
		private readonly IPublishEndpoint _publishEndpoint;

		public OrdersController(SagaOrderDBContext context, IPublishEndpoint publishEndpoint)
		{
			_context = context;
			_publishEndpoint = publishEndpoint;
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder(CreateOrderVM model)
		{
			var order = new Order.API.Models.Order()
			{
				Id = Guid.NewGuid(),
				BuyerId = model.BuyerId,
				CreatedDate = DateTime.Now,
				OrderStatu = Enum.OrderStatus.Suspend,
				TotalPrice = model.CreateOrderItems.Sum(x => x.Price * x.Quantity),
				OrderItems = model.CreateOrderItems.Select(x => new OrderItem()
				{
					Id = Guid.NewGuid(),
					Price = x.Price,
					ProductId = x.ProductId,
					Quantity = x.Quantity
				}).ToList()
			};

			await _context.AddAsync(order);
			await _context.SaveChangesAsync();

			OrderCreatedEvent orderCreatedEvent = new()
			{
				BuyerId = order.BuyerId,
				OrdeId = order.Id,
				TotalPrice = order.TotalPrice,
				OrderItemMessages = order.OrderItems.Select(x=> new OrderItemMessage()
				{
					Price = x.Price,
					ProductId = x.ProductId,
					Quantity = x.Quantity
				}).ToList()
			};

			await _publishEndpoint.Publish(orderCreatedEvent);

			return Ok();
		}
	}
}
