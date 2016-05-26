
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Restsharp;

namespace Bmbsqd.SaferPay
{

  public class Alias
  {
    public string Id { get; set; }
    public string Lifetime { get; set; }
  }

  public class RegistrationResult
  {
    public bool Success { get; set; }
    public Alias Alias { get; set; }
  }

  public class AuthorizationRequest
  {
    public RequestHeader RequestHeader { get; set; }
    public string Terminal { get; set; }
    public Payment Payment { get; set; }
    public PaymentMeans PaymentMeans { get; set; }
    public RegisterAlias RegisterAlias { get; set; }
    public Payer Payer { get; get; }
  }

  public class AuthorizeDirectResponse
  {
    public ResponseHeader ResponseHeader { get; set; }
    public Transaction Transaction { get; set; }
    public PaymentMeants PaymentMeans { get; set; }
    public RegistrationResult RegistrationResult { get; set; }
    public Payer Payer { get; set; }
  }

  public class AuthorizeDirectApi : RequestApi
  {
    public class RestResponse<AuthorizeDirectResponse> Send(AuthorizeDirectRequest req)
    {
      RestRequest request = base.Request("/Payment/v1/Transaction/Authorize")
      request.AddJsonBody(req);
      return client.Execute<AuthorizeDirectResponse>(request);
    }
  }
}
