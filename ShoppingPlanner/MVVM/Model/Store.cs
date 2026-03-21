using ShoppingPlanner.Core;

namespace ShoppingPlanner.MVVM.Model
{
    public class Store : ObservableObject
    {
        public string Name { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public Store(string name)
        {
            Name = name;
        }
    }
}
