using System.Threading.Tasks;

namespace Bmbsqd.SaferPay
{
	public interface ISaferPayAgent
	{
		Task<SaferPayAuthorization> AuthorizeAsync( int amount, string currency, string creditCardNumber, CreditCardExpiration expires, string cvc,
			string creditCardName = null,
			string orderId = null,
			string ipAddress = null );

		Task<SaferPayCredit> CreditAsync( int amount, string currency, string creditCardNumber, CreditCardExpiration expires, string cvc,
			string orderId = null );

		Task<SaferPaySettlement> SettleAsync( string authorizationId, string token, int amount = 0 );
		Task<SaferPaySettlement> CancelAuthorizationAsync( string authorizationId, string token );
	}
}