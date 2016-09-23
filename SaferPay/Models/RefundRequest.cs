namespace SaferPay.Models
{
	public class RefundRequest : RequestBase
	{
		public Refund Refund { get; set; }
		public TransactionReference TransactionReference { get; set; }
	}
}