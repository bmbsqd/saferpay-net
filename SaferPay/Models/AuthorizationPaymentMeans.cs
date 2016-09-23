namespace SaferPay.Models
{
	public class AuthorizationPaymentMeans
	{
		public Brand Brand { get; set; }
		public string DisplayText { get; set; }
		public AuthorizationCard Card { get; set; }
	}
}