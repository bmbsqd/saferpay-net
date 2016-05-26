namespace Bmbsqd.SaferPay.Models
{
	public class AuthorizeRequest : RequestBase
	{
		public string Token { get; set; }
		public string VerificationCode { get; set; }
		public RegisterAlias RegisterAlias { get; set; }
	}
}