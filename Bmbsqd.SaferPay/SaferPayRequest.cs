using System.Collections.Generic;

namespace Bmbsqd.SaferPay
{
	public class SaferPayRequest : Dictionary<string, string>, ISaferPayRequest
	{
	    public SaferPayRequest( SaferPayRequestType requestType )
		{
			RequestType = requestType;
		}

		public SaferPayRequestType RequestType { get; }
	}
}