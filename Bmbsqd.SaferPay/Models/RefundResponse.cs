namespace Bmbsqd.SaferPay.Models
{
	public class RefundResponse : ResponseBase
	{
		public Transaction Transaction { get; set; }
		public AuthorizationPaymentMeans PaymentMeans { get; set; }
		public Dcc Dcc { get; set; }
	}
}