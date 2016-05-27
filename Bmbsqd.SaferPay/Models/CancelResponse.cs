using System;

namespace Bmbsqd.SaferPay.Models
{
	public class CancelResponse : ResponseBase
	{
		public string TransactionId { get; set; }
		public string OrderId { get; set; }
		public DateTime Date { get; set; }
	}
}