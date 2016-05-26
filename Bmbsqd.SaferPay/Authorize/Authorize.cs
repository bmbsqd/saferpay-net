using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Restsharp;

namespace Bmbsqd.SaferPay
{
  public class RegisterAlias
  {
    public string IdGenerator { get; set; }
    public string Id { get; set; }
    public int Lifetime { get; set; }
  }

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

  public class ThreeDs
  {
    public bool Authenticated { get; set; }
    public bool LiabilityShift { get; set; }
    public string Xid { get; set; }
    public string VerificationValue { get; set; }
  }

  public class AuthorizationRequest
  {
    public RequestHeader RequestHeader { get; set; }
    public string Token { get; set; }
    public string VerificationCode { get; set; }
    public RegisterAlias RegisterAlias { get; set; }
  }

  public class AuthorizationResponse
  {
    public ResponseHeader ResponseHeader { get; set; }
    public Transaction Transaction { get; set; }
    public PaymentMeants PaymentMeans { get; set; }
    public RegistrationResult RegistrationResult { get; set; }
    public Payer Payer { get; set; }
    public ThreeDs ThreeDs { get; set; }
  }

  public class AuthorizationApi : RequestApi
  {
    public class RestResponse<AuthorizationResponse> Send(AuthorizationRequest req)
    {
      RestRequest request = base.Request("/Payment/v1/Transaction/Authorize")
      request.AddJsonBody(req);
      return client.Execute<AuthorizationResponse>(request);
    }
  }
}
