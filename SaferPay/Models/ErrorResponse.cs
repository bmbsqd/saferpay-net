using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SaferPay.Models
{
	public class ErrorResponse : ResponseBase
	{
		public string Behavior { get; set; }
		public string ErrorName { get; set; }
		public string ErrorMessage { get; set; }
		public string TransactionId { get; set; }
		public string ProcessorName { get; set; }
		public string ProcessorResult { get; set; }
		public string ProcessorMessage { get; set; }
		public string[] ErrorDetail { get; set; }

		[JsonExtensionData]
		public IDictionary<string, JToken> Unknown { get; set; }

	}
}