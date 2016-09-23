namespace SaferPay.Models
{
    public partial class CancelRequest : RequestBase
    {
        public TransactionReference TransactionReference { get; set; }
    }
}