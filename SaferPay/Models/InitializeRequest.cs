namespace SaferPay.Models
{
	public class InitializeRequest : RequestBase
	{
		public string TerminalId { get; set; }
		public InitializationPayment Payment { get; set; }
		public InitializationPaymentMeans PaymentMeans { get; set; }
		public Payer Payer { get; set; }
		public ReturnUrls ReturnUrls { get; set; }
	}
}