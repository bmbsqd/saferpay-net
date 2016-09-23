namespace SaferPay.Models
{
	public class AuthorizeResponse : ResponseBase
	{
		public Transaction Transaction { get; set; }
		public AuthorizationPaymentMeans PaymentMeans { get; set; }
		public Payer Payer { get; set; }
		public ThreeDs ThreeDs { get; set; }
	}
}