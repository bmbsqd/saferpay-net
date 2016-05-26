using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bmbsqd.SaferPay.Models;
using Newtonsoft.Json;

namespace Bmbsqd.SaferPay
{
	public interface ISaferPayClient
	{
		Task<TResponse> SendAsync<TResponse, TRequest>( string path, TRequest request )
			where TRequest : RequestBase
			where TResponse : ResponseBase;
	}

	public class SaferPayClient : ISaferPayClient
	{
		private readonly HttpClient _httpClient;
		private readonly SaferPaySettings _settings;

		public SaferPayClient( HttpClient httpClient, SaferPaySettings settings )
		{
			_httpClient = httpClient;
			_settings = settings;
		}

		private RequestHeader CreateRequestHeader()
		{
			var header = new RequestHeader {
				CustomerId = _settings.CustomerId,
				SpecVersion = "1.3",
				RequestId = Guid.NewGuid().ToString( "n" ),
				RetryIndicator = 0
			};

			return header;
		}

		public async Task<TResponse> SendAsync<TResponse, TRequest>( string path, TRequest request )
			where TRequest : RequestBase
			where TResponse : ResponseBase
		{
			if( request == null ) throw new ArgumentNullException( nameof( request ) );
			request.RequestHeader = CreateRequestHeader();

			var text = JsonConvert.SerializeObject( request );
			var uri = new Uri( _settings.BaseUri, path );

			var message = new HttpRequestMessage( HttpMethod.Post, uri ) {
				Content = new StringContent( text, Encoding.UTF8, "application/json" ),
				Headers = {
					Accept = {
						MediaTypeWithQualityHeaderValue.Parse( "application/json" )
					},
					Authorization = new AuthenticationHeaderValue( "Basic", Convert.ToBase64String( Encoding.Default.GetBytes( $"{_settings.Username}:{_settings.Password}" ) ) )
				}
			};

			var response = await _httpClient.SendAsync( message, HttpCompletionOption.ResponseContentRead );
			response.EnsureSuccessStatusCode();

			var responseText = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<TResponse>( responseText );
		}
	}


	public static class SaferPayClientExtensions
	{
		public static Task<InitializeResponse> InitializeAsync( this ISaferPayClient client, InitializeRequest request )
			=> client.SendAsync<InitializeResponse, InitializeRequest>( "Payment/v1/Transaction/Initialize", request );

		public static Task<AuthorizeResponse> AuthorizeAsync( this ISaferPayClient client, AuthorizeRequest request )
			=> client.SendAsync<AuthorizeResponse, AuthorizeRequest>( "Payment/v1/Transaction/Authorize", request );
	}
}