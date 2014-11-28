using System.Collections.Generic;
using System.Linq;

namespace Bmbsqd.SaferPay
{
	public class SaferPayResponse : Dictionary<string, string>, ISaferPayResponse
	{
		public SaferPayResponse( IEnumerable<KeyValuePair<string, string>> values )
		{
			foreach( var x in values )
				this[x.Key] = x.Value;
		}

		public new string this[ string name ]
		{
			get
			{
				string value;
				return TryGetValue( name, out value )
					? value
					: null;
			}
			set { base[name] = value; }
		}

		public override string ToString()
		{
			return string.Join( ", ", this.Select( x => "{" + x.Key + ": " + x.Value + "}" ) );
		}
	}
}