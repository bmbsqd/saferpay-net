namespace Bmbsqd.SaferPay.Models
{
	public class CaptureRequest : RequestBase
	{
		public TransactionReference TransactionReference { get; set; }
		public Amount Amount { get; set; }
	}
}