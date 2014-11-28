using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bmbsqd.SaferPay
{
	public class SaferPayDispatcher : ISaferPayDispatcher, IDisposable
	{
		private static readonly Regex _responseParser = new Regex( @"\b(?<key>\w+)=""(?<value>[^""]+)""",
			RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled );
		private static readonly Regex _responseOk = new Regex( @"^OK:<IDP.*?/>",
			RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled );

		private readonly Uri _baseUri;
		private readonly string _accountId;
		private readonly string _password;
		private readonly HttpClient _client;
		private readonly IReadOnlyDictionary<SaferPayRequestType, string> _requestUrlMap;

		public SaferPayDispatcher( string accountId, string password ) : this( new Uri( "https://www.saferpay.com/" ), accountId, password ) { }

		public SaferPayDispatcher( Uri baseUri, string accountId, string password )
		{
			_baseUri = baseUri;
			_accountId = accountId;
			_password = password;
			_client = new HttpClient( new HttpClientHandler {
				AllowAutoRedirect = false
			} );
			_requestUrlMap = new Dictionary<SaferPayRequestType, string> {
				{SaferPayRequestType.Authorize, "hosting/execute.asp"},
				{SaferPayRequestType.Credit, "hosting/execute.asp"},
				{SaferPayRequestType.Settle, "hosting/paycompletev2.asp"},
				{SaferPayRequestType.Cancel, "hosting/paycompletev2.asp"},
			};
		}

		private string FormatRequestParameters( IEnumerable<KeyValuePair<string, string>> request )
		{
			var values = new[] {
				"ACCOUNTID=" + _accountId,
				"spPassword=" + _password
			}.Concat( request.Where( x => x.Value != null ).Select( FormatQueryStringValue ) );

			return string.Join( "&", values );
		}

		private static string FormatQueryStringValue( KeyValuePair<string, string> x )
		{
			return x.Key + "=" + Uri.EscapeDataString( x.Value );
		}

		public async Task<ISaferPayResponse> DispatchAsync( ISaferPayRequest request )
		{
			var requestEndpoint = _requestUrlMap[request.RequestType];
			var url = new Uri( _baseUri, requestEndpoint + "?" + FormatRequestParameters( request ) );
			var response = await _client.GetAsync( url, HttpCompletionOption.ResponseContentRead );
			response.EnsureSuccessStatusCode();

			var text = await response.Content.ReadAsStringAsync();
			if( !_responseOk.IsMatch( text ) )
				throw new Exception( "Invalid response: [" + text + "]" );

			var values = _responseParser
				.Matches( text )
				.Cast<Match>()
				.Select( m => new KeyValuePair<string, string>( m.Groups["key"].Value, m.Groups["value"].Value ) );

			return new SaferPayResponse( values );
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}
