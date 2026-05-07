using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Soul_Talk.ViewModel
{
    public class SelectGuestViewModel : BaseViewModel
    {
        private readonly IGuestRepository guestRepository;
        private readonly System.Action<Guest> _confirmSelection;
        private readonly System.Action _closeDialog;

        public ObservableCollection<Guest> Guests { get; set; }
        private Guest _selectedGuest;
        public Guest SelectedGuest
        {
            get => _selectedGuest;
            set { _selectedGuest = value; OnPropertyChanged(nameof(SelectedGuest)); }
        }

        public ICommand LoadGuestsCommand { get; set; }
        public ICommand ConfirmGuestSelectionCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public SelectGuestViewModel(IGuestRepository repository, System.Action<Guest> confirmSelection, System.Action closeDialog)
        {
            guestRepository = repository;
            _confirmSelection = confirmSelection;
            _closeDialog = closeDialog;

            Guests = new ObservableCollection<Guest>();

            LoadGuestsCommand = new RelayCommand(() => LoadGuests());
            ConfirmGuestSelectionCommand = new RelayCommand(() => ConfirmGuestSelection());
            CancelCommand = new RelayCommand(() => Cancel());

            LoadGuests();
        }

        private void OnGuestSelected(Guest guest)
        {
            SelectedGuest = guest;
        }

        public void LoadGuests() 
        {
            var guests = guestRepository.GetAll();
            foreach (var g in guests)
                Guests.Add(g);
        }


        public void ConfirmGuestSelection() 
        {
                if (SelectedGuest != null)
                {
                    _confirmSelection?.Invoke(SelectedGuest);
                    _closeDialog?.Invoke();
            }
        }

        public void Cancel() 
        { 
            _closeDialog?.Invoke();
        }
    }
}
