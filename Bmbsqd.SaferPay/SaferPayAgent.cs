using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Bmbsqd.SaferPay
{
	public class SaferPayAgent : ISaferPayAgent
	{
		private readonly ISaferPayDispatcher _dispatcher;

		public SaferPayAgent( ISaferPayDispatcher dispatcher )
		{
			if( dispatcher == null ) throw new ArgumentNullException( "dispatcher" );
			_dispatcher = dispatcher;
		}

		private static string FilterCreditCardNumber( string text )
		{
			return new string( text.Where( c => c >= '0' && c <= '9' ).ToArray() );
		}

		private static string FilterName( string name )
		{
			return new string( name.Where( c => char.IsLetterOrDigit( c ) || char.IsSeparator( c ) ).ToArray() );
		}

		public async Task<SaferPayAuthorization> AuthorizeAsync( int amount, string currency, string creditCardNumber, CreditCardExpiration expires, string cvc,
			string creditCardName = null,
			string orderId = null,
			string ipAddress = null )
		{
			var request = new SaferPayRequest( SaferPayRequestType.Authorize ) {
				{"ORDERID", orderId},
				{"AMOUNT", amount.ToString( CultureInfo.InvariantCulture )},
				{"CURRENCY", currency},
				{"PAN", FilterCreditCardNumber(creditCardNumber)},
				{"EXP", expires.ToString()},
				{"CVC", cvc},
				{"NAME", FilterName(creditCardName)},
				{"IP", ipAddress},
			};

			var response = await _dispatcher.DispatchAsync( request );
			return new SaferPayAuthorization( response );
		}

		public async Task<SaferPayCredit> CreditAsync( int amount, string currency, string creditCardNumber, CreditCardExpiration expires, string cvc,
			string orderId = null )
		{
			var request = new SaferPayRequest( SaferPayRequestType.Credit ) {
				{"ACTION", "Credit"},
				{"ORDERID", orderId},
				{"AMOUNT", amount.ToString( CultureInfo.InvariantCulture )},
				{"CURRENCY", currency},
				{"PAN", FilterCreditCardNumber(creditCardNumber)},
				{"EXP", expires.ToString()},
				{"CVC", cvc}
			};

			var response = await _dispatcher.DispatchAsync( request );
			return new SaferPayCredit( response );
		}

		public async Task<SaferPaySettlement> SettleAsync( string authorizationId, string token, int amount = 0 )
		{
			var request = new SaferPayRequest( SaferPayRequestType.Settle ) {
				{"ID", authorizationId},
				{"TOKEN", token},
				{"ACTION", "Settlement"},
			};
			if( amount != 0 )
				request["AMOUNT"] = amount.ToString( CultureInfo.InvariantCulture );

			var response = await _dispatcher.DispatchAsync( request );
			return new SaferPaySettlement( response );
		}

		public async Task<SaferPaySettlement> CancelAuthorizationAsync( string authorizationId, string token )
		{
			var request = new SaferPayRequest( SaferPayRequestType.Cancel ) {
				{"ID", authorizationId},
				{"TOKEN", token},
				{"ACTION", "Cancel"},
			};

			var response = await _dispatcher.DispatchAsync( request );
			return new SaferPaySettlement( response );
		}
	}
}