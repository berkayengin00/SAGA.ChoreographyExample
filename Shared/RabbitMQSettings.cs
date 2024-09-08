﻿namespace Shared
{
	public static class RabbitMQSettings
	{
		public const string Stock_OrderCreatedEventQueue = "stock-order-created-event-queue";
		public const string Payment_StockReservedEventQueue = "payment-stock-reserved-event-queue";
		public const string Order_PaymentComplatedEventQueue = "order-payment-complated-event-queue";
		public const string Order_PaymentFailedEventQueue = "order-payment-failed-event-queue";
		public const string Stock_PaymentFailedEventQueue = "stock-payment-failed-event-queue";
		public const string Order_StockNotReservedEventQueue = "order-stock-not-reserved-event-queue";
	}
}
