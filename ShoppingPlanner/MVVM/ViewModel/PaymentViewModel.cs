using ShoppingPlanner.Core;

namespace ShoppingPlanner.MVVM.ViewModel
{
    public class PaymentViewModel : ObservableObject
    {
        private readonly SharedState _state;

        public string CardholderName
        {
            get => _state.CardholderName;
            set { _state.CardholderName = value; OnPropertyChanged(); }
        }

        public string CardNumber
        {
            get => _state.CardNumber;
            set { _state.CardNumber = value; OnPropertyChanged(); }
        }

        public string ExpiryDate
        {
            get => _state.ExpiryDate;
            set { _state.ExpiryDate = value; OnPropertyChanged(); }
        }

        public string CVV
        {
            get => _state.CVV;
            set { _state.CVV = value; OnPropertyChanged(); }
        }

        public bool AutoPaymentEnabled
        {
            get => _state.AutoPaymentEnabled;
            set { _state.AutoPaymentEnabled = value; OnPropertyChanged(); }
        }

        public PaymentViewModel(SharedState state)
        {
            _state = state;
        }
    }
}
