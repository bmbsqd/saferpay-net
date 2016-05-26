namespace Bmbsqd.SaferPay.Models
{
	public class Refund
	{
		public string Description { get; set; }
		public Amount Amount { get; set; }
		public string OrderId { get; set; }
	}
}