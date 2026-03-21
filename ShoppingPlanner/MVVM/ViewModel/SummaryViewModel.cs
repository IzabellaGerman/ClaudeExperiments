using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using ShoppingPlanner.Core;
using ShoppingPlanner.MVVM.Model;

namespace ShoppingPlanner.MVVM.ViewModel
{
    public class SummaryViewModel : ObservableObject
    {
        private readonly SharedState _state;

        private static readonly string SavePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "ShoppingPlanner", "plan.json");

        private string _summaryStores = string.Empty;
        public string SummaryStores
        {
            get => _summaryStores;
            set { _summaryStores = value; OnPropertyChanged(); }
        }

        private string _summaryProducts = string.Empty;
        public string SummaryProducts
        {
            get => _summaryProducts;
            set { _summaryProducts = value; OnPropertyChanged(); }
        }

        private string _summaryDelivery = string.Empty;
        public string SummaryDelivery
        {
            get => _summaryDelivery;
            set { _summaryDelivery = value; OnPropertyChanged(); }
        }

        private string _summaryPayment = string.Empty;
        public string SummaryPayment
        {
            get => _summaryPayment;
            set { _summaryPayment = value; OnPropertyChanged(); }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand LoadCommand { get; }

        public SummaryViewModel(SharedState state)
        {
            _state = state;
            SaveCommand = new RelayCommand(_ => Save());
            LoadCommand = new RelayCommand(_ => Load());
        }

        public void Refresh()
        {
            var storeNames = _state.SelectedStoreNames;
            SummaryStores = storeNames.Length > 0
                ? string.Join(", ", storeNames)
                : "(no stores selected)";

            if (_state.Products.Count == 0)
            {
                SummaryProducts = "(no products added)";
            }
            else
            {
                var lines = _state.Products.Select(p =>
                    $"• {p.Name} x{p.Quantity} from {p.Store} ({p.Frequency})");
                SummaryProducts = string.Join("\n", lines);
            }

            var delivery = _state.IsAlfaBox ? "Alfa Box" : "Zásilkovna";
            SummaryDelivery = $"Delivery: {delivery}\n" +
                              $"Schedule: {_state.SelectedSchedule}, on {_state.SelectedDay}s\n" +
                              $"Next purchase: {_state.NextPurchaseDate:d MMMM yyyy}";

            var card = _state.CardNumber;
            var masked = card.Length >= 4
                ? $"**** **** **** {card[^4..]}"
                : "(not entered)";
            var autoPay = _state.AutoPaymentEnabled ? "Enabled" : "Disabled";
            SummaryPayment = $"Card: {masked}\n" +
                             $"Name: {_state.CardholderName}\n" +
                             $"Auto-payment: {autoPay}";
        }

        private void Save()
        {
            var plan = new ShoppingPlan
            {
                SelectedStoreNames = _state.SelectedStoreNames.ToList(),
                Products = _state.Products.ToList(),
                DeliveryMethod = _state.IsAlfaBox ? "Alfa Box" : "Zásilkovna",
                Schedule = _state.SelectedSchedule,
                ScheduleDay = _state.SelectedDay,
                NextPurchaseDate = _state.NextPurchaseDate.ToString("yyyy-MM-dd"),
                Payment = new PaymentInfo
                {
                    CardholderName = _state.CardholderName,
                    CardNumber = _state.CardNumber,
                    ExpiryDate = _state.ExpiryDate,
                    CVV = _state.CVV,
                    AutoPaymentEnabled = _state.AutoPaymentEnabled
                }
            };

            try
            {
                var dir = Path.GetDirectoryName(SavePath)!;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var json = JsonSerializer.Serialize(plan, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SavePath, json);

                MessageBox.Show($"Plan saved to:\n{SavePath}", "Saved",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Load()
        {
            if (!File.Exists(SavePath))
            {
                MessageBox.Show("No saved plan found.", "Not Found",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var json = File.ReadAllText(SavePath);
                var plan = JsonSerializer.Deserialize<ShoppingPlan>(json);
                if (plan == null) return;

                // Restore stores
                var names = plan.SelectedStoreNames.ToHashSet();
                foreach (var store in _state.Stores)
                    store.IsSelected = names.Contains(store.Name);

                // Restore products
                _state.Products.Clear();
                foreach (var p in plan.Products)
                    _state.Products.Add(p);

                // Restore delivery
                _state.IsAlfaBox = plan.DeliveryMethod == "Alfa Box";
                _state.IsZasilkovna = plan.DeliveryMethod != "Alfa Box";
                _state.SelectedSchedule = plan.Schedule;
                _state.SelectedDay = plan.ScheduleDay;

                if (DateTime.TryParse(plan.NextPurchaseDate, out var date))
                    _state.NextPurchaseDate = date;

                // Restore payment
                _state.CardholderName = plan.Payment.CardholderName;
                _state.CardNumber = plan.Payment.CardNumber;
                _state.ExpiryDate = plan.Payment.ExpiryDate;
                _state.CVV = plan.Payment.CVV;
                _state.AutoPaymentEnabled = plan.Payment.AutoPaymentEnabled;

                Refresh();
                MessageBox.Show("Plan loaded successfully.", "Loaded",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
