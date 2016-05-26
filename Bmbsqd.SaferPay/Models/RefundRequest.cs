namespace Bmbsqd.SaferPay.Models
{
	public class RefundRequest
	{
		public RequestHeader RequestHeader { get; set; }
		public Refund Refund { get; set; }
		public TransactionReference TransactionReference { get; set; }
	}
}