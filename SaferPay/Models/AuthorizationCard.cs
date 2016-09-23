namespace SaferPay.Models
{
	public class AuthorizationCard
	{
		public string MaskedNumber { get; set; }
		public int ExpYear { get; set; }
		public int ExpMonth { get; set; }
		public string HolderName { get; set; }
		public string CountryCode { get; set; }
		public string HashValue { get; set; }
	}
}