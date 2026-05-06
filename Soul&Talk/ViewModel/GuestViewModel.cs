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
        private IRepository<Guest> guestRepository;

        public ObservableCollection<Guest> Guests { get; set; }
        public Guest SelectedGuest { get; set; }

        public ICommand LoadGuestsCommand { get; set; }
        public ICommand CreateGuestCommand { get; set; }
        public ICommand EditGuestCommand { get; set; }
        public ICommand DeleteGuestCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private Action _goBackAction;

        public GuestViewModel(IRepository<Guest> repository, Action showCreateGuestView, Action goBack)
        {
            guestRepository = repository;
            _goBackAction = goBack;
            Guests = new ObservableCollection<Guest>();

            LoadGuestsCommand = new RelayCommand(_ => LoadGuests());
            CreateGuestCommand = new RelayCommand(_ => showCreateGuestView());
            EditGuestCommand = new RelayCommand(_ => EditGuest());
            DeleteGuestCommand = new RelayCommand(_ => DeleteGuest());
            BackCommand = new RelayCommand(_ => goBack());
        }

        public void LoadGuests() { }
        public void EditGuest() { }
        public void DeleteGuest() { }
    }
}
