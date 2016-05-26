using System;

namespace Bmbsqd.SaferPay.Models
{
	public class SaferPaySettings
	{
		public Uri BaseUri { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string CustomerId { get; set; }
	}
}