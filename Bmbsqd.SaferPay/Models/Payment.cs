namespace Bmbsqd.SaferPay.Models
{
	public abstract class BasePayment
	{
		public Amount Amount { get; set; }
		public string OrderId { get; set; }
		public string Description { get; set; }
		public string PayerNote { get; set; }
		public string MandateId { get; set; }
	}

	public class AuthorizationPayment : BasePayment
	{
	}

	public class InitializationPayment : BasePayment
	{
		public PaymentOptions Options { get; set; }
		public RecurringOptions Recurring { get; set; }
	}
}