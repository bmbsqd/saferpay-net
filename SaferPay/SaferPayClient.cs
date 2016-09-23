using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SaferPay.Models;

namespace SaferPay {
	public interface ISaferPayClient : IDisposable {
		Task<TResponse> SendAsync<TResponse, TRequest>( string path, TRequest request )
			where TRequest : RequestBase
			where TResponse : ResponseBase;
	}

	public class SaferPayClient : ISaferPayClient {
		protected static readonly MediaTypeWithQualityHeaderValue _applicationJson = MediaTypeWithQualityHeaderValue.Parse( "application/json" );

		protected readonly HttpClient _httpClient;
		protected readonly SaferPaySettings _settings;

		public SaferPayClient( HttpClient httpClient, SaferPaySettings settings )
		{
			_httpClient = httpClient;
			_settings = settings;
		}

		protected virtual string GenerateRequestId() => Guid.NewGuid().ToString( "n" );

		protected virtual RequestHeader CreateRequestHeader() => new RequestHeader {
			CustomerId = _settings.CustomerId,
			SpecVersion = SaferPayApiConstants.Version,
			RequestId = GenerateRequestId(),
			RetryIndicator = 0
		};

		public virtual async Task<TResponse> SendAsync<TResponse, TRequest>( string path, TRequest request )
			where TRequest : RequestBase
			where TResponse : ResponseBase
		{
			if( request == null ) throw new ArgumentNullException( nameof( request ) );
			request.RequestHeader = CreateRequestHeader();

			var text = JsonConvert.SerializeObject( request );
			var uri = new Uri( _settings.BaseUri, path );

			var message = new HttpRequestMessage( HttpMethod.Post, uri ) {
				Content = new StringContent( text, Encoding.UTF8, _applicationJson.MediaType ),
				Headers = {
					Accept = {
						_applicationJson
					},
					Authorization = new AuthenticationHeaderValue( "Basic", Convert.ToBase64String( Encoding.ASCII.GetBytes( $"{_settings.Username}:{_settings.Password}" ) ) )
				}
			};

			var response = await _httpClient.SendAsync( message, HttpCompletionOption.ResponseContentRead );
			var responseText = await response.Content.ReadAsStringAsync();
			if( !response.IsSuccessStatusCode ) {
				var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>( responseText );
				throw new SaferPayException( response.StatusCode, errorResponse );
			}

			return JsonConvert.DeserializeObject<TResponse>( responseText );
		}

		public virtual void Dispose() => _httpClient.Dispose();
	}
}
