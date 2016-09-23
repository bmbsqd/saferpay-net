using System;

namespace SaferPay.Models
{
	public class CancelResponse : ResponseBase
	{
		public string TransactionId { get; set; }
		public string OrderId { get; set; }
		public DateTimeOffset Date { get; set; }
	}
}
