using ShoppingPlanner.Core;

namespace ShoppingPlanner.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly SharedState _state = new();

        public RelayCommand StoresViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand DeliveryViewCommand { get; set; }
        public RelayCommand PaymentViewCommand { get; set; }
        public RelayCommand SummaryViewCommand { get; set; }

        public StoresViewModel StoresVm { get; set; }
        public ProductsViewModel ProductsVm { get; set; }
        public DeliveryViewModel DeliveryVm { get; set; }
        public PaymentViewModel PaymentVm { get; set; }
        public SummaryViewModel SummaryVm { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            StoresVm = new StoresViewModel(_state);
            ProductsVm = new ProductsViewModel(_state);
            DeliveryVm = new DeliveryViewModel(_state);
            PaymentVm = new PaymentViewModel(_state);
            SummaryVm = new SummaryViewModel(_state);

            CurrentView = StoresVm;

            StoresViewCommand = new RelayCommand(_ => CurrentView = StoresVm);
            ProductsViewCommand = new RelayCommand(_ =>
            {
                ProductsVm.RefreshStores();
                CurrentView = ProductsVm;
            });
            DeliveryViewCommand = new RelayCommand(_ => CurrentView = DeliveryVm);
            PaymentViewCommand = new RelayCommand(_ => CurrentView = PaymentVm);
            SummaryViewCommand = new RelayCommand(_ =>
            {
                SummaryVm.Refresh();
                CurrentView = SummaryVm;
            });
        }
    }
}
