using System.Collections.Generic;

namespace Bmbsqd.SaferPay
{
	public interface ISaferPayRequest : IEnumerable<KeyValuePair<string, string>>
	{
		string this[string name] { get; set; }
		SaferPayRequestType RequestType { get; }
	}
}