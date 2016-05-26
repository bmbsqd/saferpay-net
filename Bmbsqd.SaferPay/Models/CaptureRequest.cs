namespace Bmbsqd.SaferPay.Models
{
	public class CaptureRequest
	{
		public RequestHeader RequestHeader { get; set; }
		public TransactionReference TransactionReference { get; set; }
		public Amount Amount { get; set; }
	}
}