using System.Collections.ObjectModel;
using ShoppingPlanner.Core;
using ShoppingPlanner.MVVM.Model;

namespace ShoppingPlanner.MVVM.ViewModel
{
    public class StoresViewModel : ObservableObject
    {
        public ObservableCollection<Store> Stores { get; }

        public StoresViewModel(SharedState state)
        {
            Stores = state.Stores;
        }
    }
}
