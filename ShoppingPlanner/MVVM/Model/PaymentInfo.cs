namespace ShoppingPlanner.MVVM.Model
{
    public class PaymentInfo
    {
        public string CardholderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public bool AutoPaymentEnabled { get; set; } = true;
    }
}
