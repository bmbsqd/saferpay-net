namespace Bmbsqd.SaferPay.Models
{
	public class AuthorizeResponse : ResponseBase
	{
		public Transaction Transaction { get; set; }
		public AuthorizationPaymentMeans PaymentMeans { get; set; }
	}
}