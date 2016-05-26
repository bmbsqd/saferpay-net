namespace Bmbsqd.SaferPay.Models
{
	public class InitializationCard
	{
		public string Number { get; set; }
		public int ExpYear { get; set; }
		public int ExpMonth { get; set; }
		public string HolderName { get; set; }
		public string VerificationCode { get; set; }
	}
}