namespace ShoppingPlanner.MVVM.Model
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string Store { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public string Frequency { get; set; } = "Monthly";
    }
}
