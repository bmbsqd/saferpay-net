using System;

namespace Bmbsqd.SaferPay.Models
{
	public class InitializationResponseRedirect
	{
		public Uri RedirectUrl { get; set; }
		public bool PaymentMeansRequired { get; set; }
	}
}