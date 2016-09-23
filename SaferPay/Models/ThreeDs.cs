namespace SaferPay.Models
{
	public class ThreeDs
	{
		public bool Authenticated { get; set; }
		public bool LiabilityShift { get; set; }
		public string Xid { get; set; }
		public string VerificationValue { get; set; }
	}
}