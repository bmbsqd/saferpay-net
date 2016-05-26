using System;

namespace Bmbsqd.SaferPay.Models
{
	public class Transaction
	{
		public string Type { get; set; }
		public string Status { get; set; }
		public string Id { get; set; }
		public DateTimeOffset Date { get; set; }
		public Amount Amount { get; set; }
		public string OrderId { get; set; }
		public string AcquireName { get; set; }
		public string AcquirerReference { get; set; }
		public DirectDebitResponse DirectDebit { get; set; }
	}
}