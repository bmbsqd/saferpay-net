using System;

namespace Bmbsqd.SaferPay.Models
{
	public class CaptureResponse
	{
		public ResponseHeader ResponseHeader { get; set; }
		public string TransactionId { get; set; }
		public string OrderId { get; set; }
		public DateTime Date { get; set; }
	}
}