using System;

namespace Bmbsqd.SaferPay.Models
{
	public class InitializeResponse : ResponseBase
	{
		public string Token { get; set; }
		public DateTimeOffset Expiration { get; set; }
		public bool LiabilityShift { get; set; }
		public bool RedirectRequired { get; set; }
		public InitializationResponseRedirect Redirect { get; set; }
	}
}