namespace Bmbsqd.SaferPay.Models
{
	public class Payer
	{
		public string LanguageCode { get; set; }
		public string IpAddress { get; set; }
		public string IpLocation { get; set; }
		public PayerAddress DeliveryAddress { get; set; }
		public PayerAddress BillingAddress { get; set; }
	}
}