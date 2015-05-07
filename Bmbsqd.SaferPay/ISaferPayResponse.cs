using System.Collections.Generic;

namespace Bmbsqd.SaferPay
{
	public interface ISaferPayResponse : IEnumerable<KeyValuePair<string, string>>
	{
		string this[string name] { get; }
	}
}