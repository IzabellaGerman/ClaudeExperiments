using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ShoppingPlanner.Core;
using ShoppingPlanner.MVVM.Model;

namespace ShoppingPlanner.MVVM.ViewModel
{
    public class ProductsViewModel : ObservableObject
    {
        private readonly SharedState _state;

        public ObservableCollection<Product> Products { get; }

        public ObservableCollection<string> SelectedStoreNames { get; } = new();

        public string[] FrequencyOptions { get; } =
            ["Weekly", "Bi-weekly", "Monthly", "Every 2 months", "Every 3 months"];

        private string _newProductName = string.Empty;
        public string NewProductName
        {
            get => _newProductName;
            set { _newProductName = value; OnPropertyChanged(); }
        }

        private string? _newProductStore;
        public string? NewProductStore
        {
            get => _newProductStore;
            set { _newProductStore = value; OnPropertyChanged(); }
        }

        private string _newProductQty = "1";
        public string NewProductQty
        {
            get => _newProductQty;
            set { _newProductQty = value; OnPropertyChanged(); }
        }

        private string _newProductFrequency = "Monthly";
        public string NewProductFrequency
        {
            get => _newProductFrequency;
            set { _newProductFrequency = value; OnPropertyChanged(); }
        }

        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public RelayCommand AddProductCommand { get; }
        public RelayCommand RemoveProductCommand { get; }

        public ProductsViewModel(SharedState state)
        {
            _state = state;
            Products = state.Products;

            AddProductCommand = new RelayCommand(_ => AddProduct());
            RemoveProductCommand = new RelayCommand(_ => RemoveProduct(), _ => SelectedProduct != null);
        }

        public void RefreshStores()
        {
            SelectedStoreNames.Clear();
            foreach (var name in _state.SelectedStoreNames)
                SelectedStoreNames.Add(name);

            if (SelectedStoreNames.Count > 0 && NewProductStore == null)
                NewProductStore = SelectedStoreNames.First();
        }

        private void AddProduct()
        {
            var name = NewProductName.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a product name.", "Missing Name",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(NewProductQty.Trim(), out int qty) || qty < 1)
                qty = 1;

            Products.Add(new Product
            {
                Name = name,
                Store = NewProductStore ?? "",
                Quantity = qty,
                Frequency = NewProductFrequency
            });

            NewProductName = "";
            NewProductQty = "1";
        }

        private void RemoveProduct()
        {
            if (SelectedProduct != null)
                Products.Remove(SelectedProduct);
        }
    }
}
