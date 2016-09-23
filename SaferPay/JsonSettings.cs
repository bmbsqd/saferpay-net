using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SaferPay {
	public static class JsonSettings {
		public static JsonSerializerSettings Default { get; } = CreateDefaultSettings();

		public static JsonSerializerSettings CreateDefaultSettings()
		{
			return new JsonSerializerSettings {
				ContractResolver = new DefaultContractResolver {
					// No CamelCase for json 9.x
					NamingStrategy = new DefaultNamingStrategy()
				}
			};
		}
	}
}