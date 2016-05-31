using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Bmbsqd.SaferPay.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Bmbsqd.SaferPay.Tests
{
	[TestFixture]
	public class IntegrationTests
	{
		private static SaferPaySettings LoadSettings() => JsonConvert.DeserializeObject<SaferPaySettings>( File.ReadAllText( Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Desktop ), "saferpay.json" ) ) );
		private static InitializationCard LoadCreditCard() => JsonConvert.DeserializeObject<InitializationCard>( File.ReadAllText( Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.Desktop ), "saferpay-creditcard.json" ) ) );

		[Test]
		public async Task FullTest1CHF()
		{
			var settings = LoadSettings();
			var card = LoadCreditCard();

			using( var client = new SaferPayClient( new HttpClient(), settings ) ) {


				TestContext.WriteLine( "Initialize" );
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

				Assert.That( initializeResponse, Is.Not.Null );
				Assert.That( initializeResponse.Token, Is.Not.Null );
				Assert.That( initializeResponse.Expiration, Is.GreaterThan( DateTimeOffset.UtcNow ) );






				TestContext.WriteLine( "Authorize" );
				var authorizeResponse = await client.AuthorizeAsync( new AuthorizeRequest {
					Token = initializeResponse.Token
				} );

				Assert.That( authorizeResponse, Is.Not.Null );
				Assert.That( authorizeResponse.Transaction, Is.Not.Null );
				Assert.That( authorizeResponse.Transaction.Id, Is.Not.Null );



				TestContext.WriteLine( "Capture" );
				var captureResponse = await client.CaptureAsync( new CaptureRequest {
					TransactionReference = new TransactionReference {
						TransactionId = authorizeResponse.Transaction.Id
					},
					Amount = new Amount {
						CurrencyCode = "CHF",
						Value = "100"
					},
				} );

				Assert.That( captureResponse, Is.Not.Null );
				Assert.That( captureResponse.TransactionId, Is.Not.Null );






				TestContext.WriteLine( "Refund" );
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

				Assert.That( refundResponse, Is.Not.Null );
				Assert.That( refundResponse.Transaction, Is.Not.Null );
			}
		}
	}
}