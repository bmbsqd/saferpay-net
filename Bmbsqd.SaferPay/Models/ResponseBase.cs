using Newtonsoft.Json;

namespace Bmbsqd.SaferPay.Models
{
	public abstract class ResponseBase
	{
		public ResponseHeader ResponseHeader { get; set; }

		public string ErrorName { get; set; }
		public string ErrorDescription { get; set; }
		public string ErrorMessage { get; set; }

		public override string ToString() => JsonConvert.SerializeObject( this, Formatting.Indented );
	}
}
