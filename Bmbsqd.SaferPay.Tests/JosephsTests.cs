using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bmbsqd.SaferPay.Tests
{
	[TestFixture]
	public class JosephsTests
	{
		private const string Account = "99867-94913159";
		private const string Password = "XAjc3Kna";


		[Test]
		public async Task HelloAgent()
		{
			var d = new SaferPayDispatcher( Account, Password );
			var a = new SaferPayAgent( d );
			var result = await a.AuthorizeAsync( 100, "CHF", "9451123100000004", new CreditCardExpiration( 2014, 12 ), "123", "Hello World", "1234567890-123" );

			Console.WriteLine( result );
			Assert.That( result.Result , Is.EqualTo( SaferPayResult.Success ));
		}

		[Test]
		public async Task InvalidCC()
		{
			var d = new SaferPayDispatcher( Account, Password );

			var auth = new SaferPayRequest( SaferPayRequestType.Authorize ) {
				{"ORDERID", "123456789-001"},
				{"AMOUNT", "1000"},
				{"CURRENCY", "EUR"},
				{"PAN", "9451123100000400"},
				{"EXP", "1214"},
				{"CVC", "123"},
			};

			var response = await d.DispatchAsync( auth );
			Console.WriteLine( response );
		}

		[Test]
		public async Task Hello()
		{
			var d = new SaferPayDispatcher( Account, Password );

			var auth = new SaferPayRequest( SaferPayRequestType.Authorize ) {
				{"ORDERID", "123456789-001"},
				{"AMOUNT", "1000"},
				{"CURRENCY", "EUR"},
				{"PAN", "9451123100000004"},
				{"EXP", "1214"},
				{"CVC", "123"},
			};

			var response = await d.DispatchAsync( auth );
			Console.WriteLine( response );
		}

		[Test]
		public async Task TestAgentAuth()
		{
			var d = new SaferPayDispatcher( Account, Password );
			var agent = new SaferPayAgent( d );

			var authorization = await agent.AuthorizeAsync( 1000, "EUR", "9451123100000004", new CreditCardExpiration( 2014, 12 ), "123", "Hello World", "o-123", "193.15.14.128" );
			Console.WriteLine( authorization );

			var settlement = await agent.SettleAsync( authorization.Id, authorization.Token );
			Console.WriteLine( settlement );
		}

		[Test]
		public async Task TestAgentCancel()
		{
			var d = new SaferPayDispatcher( Account, Password );
			var agent = new SaferPayAgent( d );

			var authorization = await agent.AuthorizeAsync( 1000, "EUR", "9451123100000004", new CreditCardExpiration( 2014, 12 ), "123", "Hello World", "o-123", "193.15.14.128" );
			Console.WriteLine( authorization );

			var cancellation = await agent.CancelAuthorizationAsync( authorization.Id, authorization.Token );
			Console.WriteLine( cancellation );
		}
	}
}
