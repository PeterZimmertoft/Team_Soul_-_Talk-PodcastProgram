using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class GuestViewModel : BaseViewModel
    {
        private readonly IRepository<Guest> guestRepository;

        public ObservableCollection<Guest> Guests { get; set; }
        public Guest SelectedGuest { get; set; }

        public ICommand LoadGuestsCommand { get; set; }
        public ICommand CreateGuestCommand { get; set; }
        public ICommand EditGuestCommand { get; set; }
        public ICommand DeleteGuestCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public GuestViewModel(IRepository<Guest> repository, Action showCreateGuestView, Action goBack)
        {
            guestRepository = repository;
            Guests = new ObservableCollection<Guest>();

            LoadGuestsCommand = new RelayCommand(LoadGuests);
            CreateGuestCommand = new RelayCommand(showCreateGuestView);
            EditGuestCommand = new RelayCommand(EditGuest);
            DeleteGuestCommand = new RelayCommand(DeleteGuest);
            BackCommand = new RelayCommand(goBack);

            LoadGuests();
        }

        public void LoadGuests()
        {
            Guests.Clear();

            foreach (var guest in guestRepository.GetAll())
            {
                Guests.Add(guest);
            }
        }

        public void EditGuest() { }
        public void DeleteGuest() { }
    }
}
