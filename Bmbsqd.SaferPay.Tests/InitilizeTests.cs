using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bmbsqd.SaferPay.Models;
using NUnit.Framework;

namespace Bmbsqd.SaferPay.Tests
{
	[TestFixture]
	public class InitializeTests
	{
		private static SaferPaySettings CreateTestDefaultSettings()
			=> new SaferPaySettings
			{
				BaseUri = new Uri("https://test.saferpay.com/api/"),
				CustomerId = "404621",
				Username = "API_404621_78913645",
				Password = "JsonApiPwd1_h5jGCcmN"
			};

		private static SaferPayClient CreateTestClient()
			=> new SaferPayClient(new HttpClient(), CreateTestDefaultSettings());




		[Test]
		public async Task InitializeTest()
		{
			var client = CreateTestClient();

			var response = await client.InitializeAsync(new InitializeRequest
			{
				TerminalId = "17812438",
				Payment = new InitializationPayment
				{
					Amount = new Amount
					{
						CurrencyCode = "EUR",
						Value = "100"
					},
				},
				PaymentMeans = new InitializationPaymentMeans
				{
					Card = new InitializationCard
					{
						Number = "9070101052000009",
						ExpYear = 2031,
						ExpMonth = 2,
						HolderName = "DURANGO",
						VerificationCode = "1111"
					}
				},
				ReturnUrls = new ReturnUrls
				{
					Success = new Uri("http://localhost/success"),
					Fail = new Uri("http://localhost/fail"),
					Abort = new Uri("http://localhost/abort")
				}
			});

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Token, Is.Not.Null);
			Assert.That(response.Expiration, Is.GreaterThan(DateTimeOffset.UtcNow));

			TestContext.WriteLine(response);
		}


		private Task<InitializeResponse> CreateInitialization(ISaferPayClient client)
		{
			return client.InitializeAsync(new InitializeRequest
			{
				TerminalId = "17812438",
				Payment = new InitializationPayment
				{
					Amount = new Amount
					{
						CurrencyCode = "EUR",
						Value = "100"
					},
				},
				PaymentMeans = new InitializationPaymentMeans
				{
					Card = new InitializationCard
					{
						Number = "9070101052000009",
						ExpYear = 2031,
						ExpMonth = 2,
						HolderName = "DURANGO",
						VerificationCode = "1111"
					}
				},
				ReturnUrls = new ReturnUrls
				{
					Success = new Uri("http://localhost/success"),
					Fail = new Uri("http://localhost/fail"),
					Abort = new Uri("http://localhost/abort")
				}
			});
		}

		[Test]
		public async Task AuthorizeTest()
		{
			var client = CreateTestClient();

			var initialization = await CreateInitialization(client);

			var response = await client.AuthorizeAsync(new AuthorizeRequest
			{
				Token = initialization.Token
			});

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Transaction, Is.Not.Null);
			Assert.That(response.Transaction.Id, Is.Not.Null);
		}

		[Test]
		public async Task CaptureTest()
		{
			var client = CreateTestClient();

			var initialization = await CreateInitialization(client);

			var response = await client.AuthorizeAsync(new AuthorizeRequest
			{
				Token = initialization.Token
			});

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Transaction, Is.Not.Null);
			Assert.That(response.Transaction.Id, Is.Not.Null);

			var capture = await client.CaptureAsync(new CaptureRequest
			{
				TransactionReference = new TransactionReference
				{
					TransactionId = response.Transaction.Id
				},
				Amount = new Amount
				{
					CurrencyCode = "EUR",
					Value = "100"
				},
			});

			Assert.That(capture, Is.Not.Null);
			Assert.That(capture.TransactionId, Is.Not.Null);
		}

		[Test]
		public async Task CancelTest()
		{
			var client = CreateTestClient();

			var initialization = await CreateInitialization(client);

			var response = await client.AuthorizeAsync(new AuthorizeRequest
			{
				Token = initialization.Token
			});

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Transaction, Is.Not.Null);
			Assert.That(response.Transaction.Id, Is.Not.Null);

			var cancel = await client.CancelAsync(new CancelRequest
			{
				TransactionReference = new TransactionReference
				{
					TransactionId = response.Transaction.Id
				}
			});

			TestContext.WriteLine(cancel);
			Assert.That(cancel, Is.Not.Null);
			Assert.That(cancel.TransactionId, Is.Not.Null);
		}

		[Test]
		public async Task RefundTest()
		{
			var client = CreateTestClient();

			var initialization = await CreateInitialization(client);

			var response = await client.AuthorizeAsync(new AuthorizeRequest
			{
				Token = initialization.Token
			});

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Transaction, Is.Not.Null);
			Assert.That(response.Transaction.Id, Is.Not.Null);

			var capture = await client.CaptureAsync(new CaptureRequest
			{
				TransactionReference = new TransactionReference
				{
					TransactionId = response.Transaction.Id
				},
				Amount = new Amount
				{
					CurrencyCode = "EUR",
					Value = "100"
				},
			});

			Assert.That(capture, Is.Not.Null);
			Assert.That(capture.TransactionId, Is.Not.Null);

			var refund = await client.RefundAsync(new RefundRequest
			{
				Refund = new Refund
				{
					Amount = new Amount
					{
						Value = "100",
						CurrencyCode = "EUR"
					}
				},
				TransactionReference = new TransactionReference
				{
					TransactionId = response.Transaction.Id
				}
			});

			Assert.That(refund, Is.Not.Null);
			Assert.That(refund.Transaction, Is.Not.Null);
		}
	}
}