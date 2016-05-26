using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Restsharp;

namespace Bmbsqd.SaferPay
{
  public class RequestApi
  {
    public RestClient client;

    public RequestApi()
    {
      client = new RestClient("https://www.saferpay.com/api");
      client.AddDefaultHeader("Content-Type", "application/json");
    }

    public class RestRequest Request(string url)
    {
      var request = new RestRequest(url, Method.POST)
      request.RequestFormat = DataFormat.Json;
      request.JsonSerializer = new NewtonsoftJsonSerializer();
      request.JsonSerializer.ContentType = "application/json; charset=utf-8";
      return request;
    }
  }


}
