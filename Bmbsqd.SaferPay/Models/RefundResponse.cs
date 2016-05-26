namespace Bmbsqd.SaferPay.Models
{
	public class RefundResponse
	{
		public ResponseHeader ResponseHeader { get; set; }
		public Transaction Transaction { get; set; }
		public AuthorizationPaymentMeans PaymentMeans { get; set; }
		public Dcc Dcc { get; set; }
	}
}