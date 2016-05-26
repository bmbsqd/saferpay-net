using System;
using System.Collections.Generic;

namespace Bmbsqd.SaferPay
{
  public class TransactionReference
  {
    public string TransactionId { get; set; }
  }

  public class Amount
  {
    public string Value { get; set; }
    public string CurrenyCode { get; set; }
  }

  public class Payment
  {
    public Amount Amount { get; set; }
    public string OrderId { get; set; }
    public string Description { get; set; }
    public string PayerNote { get; set; }
  }

  public class Brand
  {
    public int PaymentMethod { get; set; }
    public string Name { get; set; }
  }

  public class Card
  {
    public string MaskedNumber { get; set; }
    public int ExpYear { get; set; }
    public int ExpMonth { get; set; }
    public string HolderName { get; set; }
    public string CountryCode { get; set; }
  }

  public class PaymentMeans
  {
    public Brand Brand { get; set; }
    public string DisplayText { get; set; }
    public Card Card { get; set; }
  }

  public class Payer
  {
    public string IpAddress { get; set; }
    public string IpLocation { get; set; }
  }

  public class Transaction
  {
    public string Type { get; set; }
    public string Status { get; set; }
    public string Id { get; set; }
    public Amount Amount { get; set; }
    public string OrderId { get; set; }
    public string AcquireName { get; set; }
    public string AcquirerReference { get; set; }
  }

  public class ClientInfo
  {
    public string ShopInfo { get; set; }
    public string ApplicationInfo { get; set; }
    public string OsInfo { get; set; }
  }

  public class RequestHeader
  {
    public string SpecVersion { get; set; }
    public string CustomerId { get; set; }
    public string RequestId { get; set; }
    public ClientInfo ClientInfo { get; set; }
  }

  public class ResponseHeader {
    public string SpecVersion { get; set; }
    public string RequestId { get; set; }
  }

  public class Redirect
  {
    public string RedirectUrl { get; set; }
    public bool PaymentMeansRequired { get; set; }
  }
}
