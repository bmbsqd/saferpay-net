using System.Collections.Generic;

namespace Bmbsqd.SaferPay
{
	public class SaferPayRequest : Dictionary<string, string>, ISaferPayRequest
	{
		private readonly SaferPayRequestType _requestType;

		public SaferPayRequest( SaferPayRequestType requestType )
		{
			_requestType = requestType;
		}

		public SaferPayRequestType RequestType
		{
			get { return _requestType; }
		}
	}
}