using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class GuestViewModel : BaseViewModel
    {
        private IGuestRepository guestRepository;

        public ObservableCollection<Guest> Guests { get; set; }
        public Guest SelectedGuest { get; set; }

        public ICommand LoadGuestsCommand { get; set; }
        public ICommand CreateGuestCommand { get; set; }
        public ICommand EditGuestCommand { get; set; }
        public ICommand DeleteGuestCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public GuestViewModel(IGuestRepository repository, Action showCreateGuestView)
        {
            guestRepository = repository;
            Guests = new ObservableCollection<Guest>();

            LoadGuestsCommand = new RelayCommand(_ => LoadGuests());
            CreateGuestCommand = new RelayCommand(_ => CreateGuest());
            EditGuestCommand = new RelayCommand(_ => EditGuest());
            DeleteGuestCommand = new RelayCommand(_ => DeleteGuest());
            BackCommand = new RelayCommand(_ => GoBack());
        }

        public void LoadGuests() { }
        public void CreateGuest() { }
        public void EditGuest() { }
        public void DeleteGuest() { }
        public void GoBack() { }
    }
}
