namespace SaferPay.Models
{
	public class AuthorizationRequest : RequestBase
	{
		public string Terminal { get; set; }
		public string Token { get; set; }
		public string VerificationCode { get; set; }
		public AuthorizationPayment Payment { get; set; }
		public AuthorizationPaymentMeans PaymentMeans { get; set; }
		public RegisterAlias RegisterAlias { get; set; }
		public Payer Payer { get; set; }
	}
}
