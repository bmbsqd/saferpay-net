using System;
using System.Net.Http;
using System.Threading.Tasks;
using SaferPay.Models;
using Xunit;
using Xunit.Abstractions;

namespace SaferPay.Tests {
	public class IntegrationTests {

		[Fact]
		public async Task FullTest1CHF( ITestOutputHelper output )
		{
			var settings = TestSettings.LoadSettings();
			var card = TestSettings.LoadCreditCard();

			using( var client = new SaferPayClient( new HttpClient(), settings ) ) {
				output.WriteLine( "Initialize" );
				var initializeResponse = await client.InitializeAsync( new InitializeRequest {
					TerminalId = settings.TerminalId,
					Payment = new InitializationPayment {
						Amount = new Amount {
							CurrencyCode = "CHF",
							Value = "100"
						},
					},
					PaymentMeans = new InitializationPaymentMeans {
						Card = card
					},
					ReturnUrls = new ReturnUrls {
						Success = new Uri( "http://localhost/success" ),
						Fail = new Uri( "http://localhost/fail" ),
						Abort = new Uri( "http://localhost/abort" )
					}
				} );

				Assert.NotNull( initializeResponse );
				Assert.NotNull( initializeResponse.Token );
				Assert.True( initializeResponse.Expiration > DateTimeOffset.UtcNow );






				output.WriteLine( "Authorize" );
				var authorizeResponse = await client.AuthorizeAsync( new AuthorizeRequest {
					Token = initializeResponse.Token
				} );

				Assert.NotNull( authorizeResponse );
				Assert.NotNull( authorizeResponse.Transaction );
				Assert.NotNull( authorizeResponse.Transaction.Id );



				output.WriteLine( "Capture" );
				var captureResponse = await client.CaptureAsync( new CaptureRequest {
					TransactionReference = new TransactionReference {
						TransactionId = authorizeResponse.Transaction.Id
					},
					Amount = new Amount {
						CurrencyCode = "CHF",
						Value = "100"
					},
				} );

				Assert.NotNull( captureResponse );
				Assert.NotNull( captureResponse.TransactionId );






				output.WriteLine( "Refund" );
				var refundResponse = await client.RefundAsync( new RefundRequest {
					Refund = new Refund {
						Amount = new Amount {
							CurrencyCode = "CHF",
							Value = "100"
						}
					},
					TransactionReference = new TransactionReference {
						TransactionId = captureResponse.TransactionId
					}
				} );

				Assert.NotNull( refundResponse );
				Assert.NotNull( refundResponse.Transaction );
			}
		}
	}
}