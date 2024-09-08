using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Consumers;
using Order.API.Context;
using Shared;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(conf =>
{
	conf.AddConsumer<PaymentComplatedEventCosumer>();
	conf.AddConsumer<PaymentFailedEventConsumer>();
	conf.AddConsumer<StockNotReservedEventConsumer>();
	conf.UsingRabbitMq((context, _configure) =>
	{
		_configure.Host(builder.Configuration["RabbitMQUrl"]);
		_configure.ReceiveEndpoint(RabbitMQSettings.Order_PaymentComplatedEventQueue, e => e.ConfigureConsumer<PaymentComplatedEventCosumer>(context));
		_configure.ReceiveEndpoint(RabbitMQSettings.Order_PaymentFailedEventQueue, e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));
		_configure.ReceiveEndpoint(RabbitMQSettings.Order_StockNotReservedEventQueue, e => e.ConfigureConsumer<StockNotReservedEventConsumer>(context));
	});
});

builder.Services.AddDbContext<SagaOrderDBContext>(x=> x.UseSqlServer(builder.Configuration.GetConnectionString("MssqlConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
