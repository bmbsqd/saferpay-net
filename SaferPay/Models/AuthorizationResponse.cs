namespace SaferPay.Models
{
	public class AuthorizationResponse : ResponseBase
	{
		public Transaction Transaction { get; set; }
		public AuthorizationPaymentMeans PaymentMeans { get; set; }
		public RegistrationResult RegistrationResult { get; set; }
		public Payer Payer { get; set; }
		public ThreeDs ThreeDs { get; set; }
	}
}
