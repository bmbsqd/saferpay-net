using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Bmbsqd.SaferPay
{

	public enum SaferPayResult
	{
		Success = 0,
		AccessDenied = 5,
		InvalidCard = 61,
		InvalidDate = 62,
		CardExpired = 63,
		UnknownCard = 64, 
		AuthorizationDeclined = 66,
		NoContractAvailable = 67,
		GeoIpNotWhitelisted = 70,
		InvalidCurrency = 83,
		InvalidAmount = 84,
		NoCreditsAvailable = 85,
		InvalidFunction =102,
		BlacklistedCard = 104,
		CardCountryNotWhitelisted =105,
		InvalidCVC = 113,
		CVCMandatory = 114,
	}

	public abstract class SaferPayAgentResponse : ISaferPayResponse
	{
		private readonly ISaferPayResponse _response;

		protected SaferPayAgentResponse( ISaferPayResponse response )
		{
			if( response == null ) throw new ArgumentNullException( "response" );
			_response = response;
		}

		public string this[string name]
		{
			get { return _response[name]; }
		}

		public string Id { get { return this["ID"]; } }
		public SaferPayResult Result { get { return (SaferPayResult)int.Parse( this["RESULT"] ); } }

		public bool IsSuccess
		{
			get { return Result == 0; }
		}

		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return _response.GetEnumerator();
		}

		public override string ToString()
		{
			return _response.ToString();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class SaferPayCredit : SaferPayAgentResponse
	{
		public SaferPayCredit( ISaferPayResponse response )
			: base( response )
		{
		}
	}

	public class SaferPayAuthorization : SaferPayAgentResponse
	{
		public SaferPayAuthorization( ISaferPayResponse response ) : base( response ) { }

		public DateTime AuthorizationDate { get { return DateTime.ParseExact( this["AUTHDATE"], "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal ); } }
		public string AuthorizationCode { get { return this["AUTHCODE"]; } }
		public int AuthorizationResult { get { return int.Parse( this["AUTHRESULT"] ); } }
		public string AuthorizationMessage { get { return this["AUTHMESSAGE"]; } }
		public string Token { get { return this["TOKEN"]; } }
		public int ProviderId { get { return int.Parse( this["PROVIDERID"] ); } }
		public string ProviderName { get { return this["PROVIDERNAME"]; } }
		public string CreditCardNumber { get { return this["PAN"]; } }
		public CreditCardExpiration CreditCardExpires { get { return CreditCardExpiration.Parse( this["EXP"] ); } }
		public string CreditCardCountry { get { return this["CCCOUNTRY"]; } }
		public string IpAddressCountry { get { return this["IPCOUNTRY"]; } }
	}

	public class SaferPaySettlement : SaferPayAgentResponse
	{
		public SaferPaySettlement( ISaferPayResponse response ) : base( response ) { }
		public string Message { get { return this["MESSAGE"]; } }
		public string AuthorizationMessage { get { return this["AUTHMESSAGE"]; } }
	}
}