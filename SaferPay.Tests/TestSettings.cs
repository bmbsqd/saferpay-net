using System.IO;
using Newtonsoft.Json;
using SaferPay.Models;

namespace SaferPay.Tests {
	public static class TestSettings {
		private const string SecretsDirectory = "secrets";

		public static SaferPaySettings LoadTestSettings() => JsonConvert.DeserializeObject<SaferPaySettings>( File.ReadAllText( Path.Combine( SecretsDirectory, "saferpay-test.json" ) ) );

		public static SaferPaySettings LoadSettings() => JsonConvert.DeserializeObject<SaferPaySettings>( File.ReadAllText( Path.Combine( SecretsDirectory, "saferpay.json" ) ) );
		public static InitializationCard LoadCreditCard() => JsonConvert.DeserializeObject<InitializationCard>( File.ReadAllText( Path.Combine( SecretsDirectory, "saferpay-creditcard.json" ) ) );
	}
}