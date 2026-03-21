using System;
using System.Collections.ObjectModel;
using System.Linq;
using ShoppingPlanner.MVVM.Model;

namespace ShoppingPlanner.MVVM.ViewModel
{
    public class SharedState
    {
        public ObservableCollection<Store> Stores { get; } = new()
        {
            new Store("Albert"),
            new Store("Tesco"),
            new Store("Lidl"),
            new Store("Kaufland"),
            new Store("Billa"),
            new Store("Penny Market"),
            new Store("Globus"),
            new Store("Makro"),
            new Store("CZC.cz"),
            new Store("Alza.cz"),
            new Store("Rohlík.cz"),
            new Store("Košík.cz"),
        };

        public ObservableCollection<Product> Products { get; } = new();

        // Delivery
        public bool IsAlfaBox { get; set; } = true;
        public bool IsZasilkovna { get; set; }

        // Schedule
        public string SelectedSchedule { get; set; } = "Monthly";
        public string SelectedDay { get; set; } = "Monday";
        public DateTime NextPurchaseDate { get; set; } = DateTime.Today.AddDays(7);

        // Payment
        public string CardholderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public bool AutoPaymentEnabled { get; set; } = true;

        public string[] SelectedStoreNames =>
            Stores.Where(s => s.IsSelected).Select(s => s.Name).ToArray();
    }
}
