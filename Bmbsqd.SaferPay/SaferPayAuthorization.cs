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
        InvalidFunction = 102,
        BlacklistedCard = 104,
        CardCountryNotWhitelisted = 105,
        InvalidCVC = 113,
        CVCMandatory = 114,
    }

    public abstract class SaferPayAgentResponse : ISaferPayResponse
    {
        private readonly ISaferPayResponse _response;

        protected SaferPayAgentResponse(ISaferPayResponse response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            _response = response;
        }

        public string this[string name] => _response[name];
        public string Id => this["ID"];
        public SaferPayResult Result => (SaferPayResult)int.Parse(this["RESULT"]);
        public bool IsSuccess => Result == SaferPayResult.Success;

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
        public SaferPayCredit(ISaferPayResponse response)
            : base(response)
        {
        }
    }

    public class SaferPayAuthorization : SaferPayAgentResponse
    {
        public SaferPayAuthorization(ISaferPayResponse response) : base(response) { }

        public DateTime AuthorizationDate => DateTime.ParseExact(this["AUTHDATE"], "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        public string AuthorizationCode => this["AUTHCODE"];
        public int AuthorizationResult => int.Parse(this["AUTHRESULT"]);
        public string AuthorizationMessage => this["AUTHMESSAGE"];
        public string Token => this["TOKEN"];
        public int ProviderId => int.Parse(this["PROVIDERID"]);
        public string ProviderName => this["PROVIDERNAME"];
        public string CreditCardNumber => this["PAN"];
        public CreditCardExpiration CreditCardExpires => CreditCardExpiration.Parse(this["EXP"]);
        public string CreditCardCountry => this["CCCOUNTRY"];
        public string IpAddressCountry => this["IPCOUNTRY"];
    }

    public class SaferPaySettlement : SaferPayAgentResponse
    {
        public SaferPaySettlement(ISaferPayResponse response) : base(response) { }
        public string Message => this["MESSAGE"];
        public string AuthorizationMessage => this["AUTHMESSAGE"];
    }
}
