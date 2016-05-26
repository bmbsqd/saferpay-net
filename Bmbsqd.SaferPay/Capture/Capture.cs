using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Restsharp;

namespace Bmbsqd.SaferPay
{
  public class CaptureRequest
  {
    public RequestHeader RequestHeader { get; set; }
    public TransactionReference TransactionReference { get; set; }
    public Amount Amount { get; set; }
  }

  public class CaptureResponse
  {
    public ResponseHeader ResponseHeader { get; set; }
    public TransactionId TransactionId { get; set; }
    public string OrderId { get; set; }
    public DateTime Date { get; set; }
  }

  public class CaptureApi : RequestApi
  {
    public class RestResponse<CaptureResponse> Send(CaptureRequest req)
    {
      RestRequest request = base.Request("/Payment/v1/Transaction/Capture")
      request.AddJsonBody(req);
      return client.Execute<CaptureResponse>(request);
    }
  }
}
