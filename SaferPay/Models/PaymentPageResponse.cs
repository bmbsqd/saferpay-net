namespace SaferPay.Models
{
	public class PaymentPageResponse
	{
		public ResponseHeader ResponseHeader { get; set; }
		public string Token { get; set; }
		public string Expiration { get; set; }
		public bool RedirectRequired { get; set; }
		public bool LiabilityShift { get; set; }
		public Redirect Redirect { get; set; }
		public string RedirectUrl { get; set; }
	}
}