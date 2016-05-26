using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Restsharp;

namespace Bmbsqd.SaferPay
{
  public class Refund
  {
    public string Description { get; set; }
    public Amount Amount { get; set; }
    public string OrderId { get; set; }
  }

  public class Dcc
  {
    public Amount PaymentAmount { get; set; }
  }

  public class RefundRequest
  {
    public RequestHeader RequestHeader { get; set; }
    public Refund Refund { get; set; }
    public TransactionReference TransactionReference { get; set; }
  }

  public class RefundResponse
  {
    public ResponseHeader ResponseHeader { get; set; }
    public Transaction Transaction { get; set; }
    public PaymentMeans PaymentMeans { get; set; }
    public Dcc Dcc { get; set; }
  }

  public class RefundApi : RequestApi
  {
    public class RestResponse<RefundResponse> Send(RefundRequest req)
    {
      RestRequest request = base.Request("/Payment/v1/Transaction/Refund")
      request.AddJsonBody(req);
      return client.Execute<RefundResponse>(request);
    }
  }
}
