using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class SelectGuestViewModel
    {
        private IGuestRepository guestRepository;

        public ObservableCollection<Guest> Guests { get; set; }
        public Guest SelectedGuest { get; set; }

        public ICommand LoadGuestsCommand { get; set; }
        public ICommand AddSelectedGuestCommand { get; set; }
        public ICommand CreateGuestCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public SelectGuestViewModel(IGuestRepository repository)
        {
            guestRepository = repository;

            Guests = new ObservableCollection<Guest>();

            LoadGuestsCommand = new RelayCommand(_ => LoadGuests());
            AddSelectedGuestCommand = new RelayCommand(_ => AddSelectedGuest());
            CreateGuestCommand = new RelayCommand(_ => { });
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public void LoadGuests() { }
        public void AddSelectedGuest() { }
        public void Cancel() { }
    }
}
