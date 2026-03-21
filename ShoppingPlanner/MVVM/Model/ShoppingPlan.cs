using System.Collections.Generic;

namespace ShoppingPlanner.MVVM.Model
{
    public class ShoppingPlan
    {
        public List<string> SelectedStoreNames { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public string DeliveryMethod { get; set; } = "Alfa Box";
        public string Schedule { get; set; } = "Monthly";
        public string ScheduleDay { get; set; } = "Monday";
        public string NextPurchaseDate { get; set; } = string.Empty;
        public PaymentInfo Payment { get; set; } = new();
    }
}
