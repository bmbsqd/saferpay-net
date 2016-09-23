using System.Threading.Tasks;
using SaferPay.Models;

namespace SaferPay {
	public static class SaferPayClientExtensions {
		public static Task<InitializeResponse> InitializeAsync( this ISaferPayClient client, InitializeRequest request )
			=> client.SendAsync<InitializeResponse, InitializeRequest>( SaferPayEndpointConstants.TransactionEndpoint + "/Initialize", request );

		public static Task<AuthorizeResponse> AuthorizeAsync( this ISaferPayClient client, AuthorizeRequest request )
			=> client.SendAsync<AuthorizeResponse, AuthorizeRequest>( SaferPayEndpointConstants.TransactionEndpoint + "/Authorize", request );

		public static Task<CaptureResponse> CaptureAsync( this ISaferPayClient client, CaptureRequest request )
			=> client.SendAsync<CaptureResponse, CaptureRequest>( SaferPayEndpointConstants.TransactionEndpoint + "/Capture", request );

		public static Task<CancelResponse> CancelAsync( this ISaferPayClient client, CancelRequest request )
			=> client.SendAsync<CancelResponse, CancelRequest>( SaferPayEndpointConstants.TransactionEndpoint + "/Cancel", request );

		public static Task<RefundResponse> RefundAsync( this ISaferPayClient client, RefundRequest request )
			=> client.SendAsync<RefundResponse, RefundRequest>( SaferPayEndpointConstants.TransactionEndpoint + "/Refund", request );
	}
}