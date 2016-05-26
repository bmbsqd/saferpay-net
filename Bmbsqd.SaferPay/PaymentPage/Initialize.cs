using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Restsharp;

namespace Bmbsqd.SaferPay
{
  public class PaymentPageApi : AuthorizationApi
  {
    public class Payment
    {
      public Amount Amount { get; set; }
      public string OrderId { get; set; }
      public string Description { get; set; }
      public string PayerNote { get; set; }
    }

    public class Payer
    {
      public string IpAddress { get; set; }
    }

    public class ReturnUrls
    {
      public string Success { get; set; }
      public string Fail { get; set; }
      public string Abort { get; set; }
    }

    public class Notification
    {
      public string MerchantEmail { get; set; }
      public string NotifyUrl { get; set; }
    }

    public class Redirect
    {
      public string RedirectUrl { get; set; }
      public bool PaymentMeansRequired { get; set; }
    }

    public class PaymentPageResponse
    {
      public ResponseHeader ResponseHeader { get; set; }
      public string Token { get; set; }
      public string Expiration { get; set; }
      public bool RedirectRequired { get; set; }
      public bool LiabilityShift { get; set; }
      public Redirect Redirect { get; set; }
      public string RedirectUrl { get; set; }
    }

    public class RestResponse<PaymentPageResponse> Send(PaymentPageRequest req)
    {
      RestRequest request = base.Request("/Payment/v1/Transaction/Initialize")
      request.AddJsonBody(req);
      return client.Execute<PaymentPageResponse>(request);
    }
  }


}
