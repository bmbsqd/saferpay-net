using System;
using System.Net.Http;
using System.Threading.Tasks;
using SaferPay.Models;
using Xunit;

namespace SaferPay.Tests {
	public class ApiTests {
		private static SaferPayClient CreateTestClient()
			=> new SaferPayClient( new HttpClient(), TestSettings.LoadTestSettings() );



		[Fact]
		public async Task InitializeTest()
		{
			var client = CreateTestClient();

			var response = await client.InitializeAsync( new InitializeRequest {
				TerminalId = "17812438",
				Payment = new InitializationPayment {
					Amount = new Amount {
						CurrencyCode = "EUR",
						Value = "100"
					},
				},
				PaymentMeans = new InitializationPaymentMeans {
					Card = new InitializationCard {
						Number = "9070101052000009",
						ExpYear = 2031,
						ExpMonth = 2,
						HolderName = "DURANGO",
						VerificationCode = "1111"
					}
				},
				ReturnUrls = new ReturnUrls {
					Success = new Uri( "http://localhost/success" ),
					Fail = new Uri( "http://localhost/fail" ),
					Abort = new Uri( "http://localhost/abort" )
				}
			} );

			Assert.NotNull( response );
			Assert.NotNull( response.Token );
			Assert.True( response.Expiration > DateTimeOffset.UtcNow );
		}

		private Task<InitializeResponse> CreateInitialization( ISaferPayClient client )
		{
			return client.InitializeAsync( new InitializeRequest {
				TerminalId = "17812438",
				Payment = new InitializationPayment {
					Amount = new Amount {
						CurrencyCode = "EUR",
						Value = "100"
					},
				},
				PaymentMeans = new InitializationPaymentMeans {
					Card = new InitializationCard {
						Number = "9070101052000009",
						ExpYear = 2031,
						ExpMonth = 2,
						HolderName = "DURANGO",
						VerificationCode = "1111"
					}
				},
				ReturnUrls = new ReturnUrls {
					Success = new Uri( "http://localhost/success" ),
					Fail = new Uri( "http://localhost/fail" ),
					Abort = new Uri( "http://localhost/abort" )
				}
			} );
		}

		[Fact]
		public async Task AuthorizeTest()
		{
			var client = CreateTestClient();

			var initialization = await CreateInitialization( client );

			var response = await client.AuthorizeAsync( new AuthorizeRequest {
				Token = initialization.Token
			} );

			Assert.NotNull( response );
			Assert.NotNull( response.Transaction );
			Assert.NotNull( response.Transaction.Id );
		}

		[Fact]
		public async Task CaptureTest()
		{
			var client = CreateTestClient();

			var initialization = await CreateInitialization( client );

			var response = await client.AuthorizeAsync( new AuthorizeRequest {
				Token = initialization.Token
			} );

			Assert.NotNull( response );
			Assert.NotNull( response.Transaction );
			Assert.NotNull( response.Transaction.Id );

			var capture = await client.CaptureAsync( new CaptureRequest {
				TransactionReference = new TransactionReference {
					TransactionId = response.Transaction.Id
				},
				Amount = new Amount {
					CurrencyCode = "EUR",
					Value = "100"
				},
			} );

			Assert.NotNull( capture );
			Assert.NotNull( capture.TransactionId );
		}

		[Fact]
		public async Task CancelTest()
		{
			var client = CreateTestClient();

			var initialization = await CreateInitialization( client );

			var response = await client.AuthorizeAsync( new AuthorizeRequest {
				Token = initialization.Token
			} );

			Assert.NotNull( response );
			Assert.NotNull( response.Transaction );
			Assert.NotNull( response.Transaction.Id );

			var cancel = await client.CancelAsync( new CancelRequest {
				TransactionReference = new TransactionReference {
					TransactionId = response.Transaction.Id
				}
			} );

			Assert.NotNull( cancel );
			Assert.NotNull( cancel.TransactionId );
		}

		[Fact]
		public async Task RefundTest()
		{
			var client = CreateTestClient();

			var initialization = await CreateInitialization( client );

			var response = await client.AuthorizeAsync( new AuthorizeRequest {
				Token = initialization.Token
			} );

			Assert.NotNull( response );
			Assert.NotNull( response.Transaction );
			Assert.NotNull( response.Transaction.Id );

			var capture = await client.CaptureAsync( new CaptureRequest {
				TransactionReference = new TransactionReference {
					TransactionId = response.Transaction.Id
				},
				Amount = new Amount {
					CurrencyCode = "EUR",
					Value = "100"
				},
			} );

			Assert.NotNull( capture );
			Assert.NotNull( capture.TransactionId );

			var refund = await client.RefundAsync( new RefundRequest {
				Refund = new Refund {
					Amount = new Amount {
						Value = "100",
						CurrencyCode = "EUR"
					}
				},
				TransactionReference = new TransactionReference {
					TransactionId = response.Transaction.Id
				}
			} );

			Assert.NotNull( refund );
			Assert.NotNull( refund.Transaction );
		}
	}
}
