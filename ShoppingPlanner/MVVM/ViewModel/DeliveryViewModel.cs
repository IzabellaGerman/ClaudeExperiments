using System;
using System.Collections.Generic;
using ShoppingPlanner.Core;

namespace ShoppingPlanner.MVVM.ViewModel
{
    public class DeliveryViewModel : ObservableObject
    {
        private readonly SharedState _state;

        public List<string> ScheduleOptions { get; } =
            ["Weekly", "Bi-weekly", "Monthly", "Every 2 months", "Every 3 months"];

        public List<string> DayOptions { get; } =
            ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];

        public bool IsAlfaBox
        {
            get => _state.IsAlfaBox;
            set { _state.IsAlfaBox = value; OnPropertyChanged(); }
        }

        public bool IsZasilkovna
        {
            get => _state.IsZasilkovna;
            set { _state.IsZasilkovna = value; OnPropertyChanged(); }
        }

        public string SelectedSchedule
        {
            get => _state.SelectedSchedule;
            set { _state.SelectedSchedule = value; OnPropertyChanged(); }
        }

        public string SelectedDay
        {
            get => _state.SelectedDay;
            set { _state.SelectedDay = value; OnPropertyChanged(); }
        }

        public DateTime NextPurchaseDate
        {
            get => _state.NextPurchaseDate;
            set { _state.NextPurchaseDate = value; OnPropertyChanged(); }
        }

        public DeliveryViewModel(SharedState state)
        {
            _state = state;
        }
    }
}
