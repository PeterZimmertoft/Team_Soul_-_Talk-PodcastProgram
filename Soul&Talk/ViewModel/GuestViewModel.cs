using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class GuestViewModel : BaseViewModel
    {
        private readonly IGuestRepository _guestRepository;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Guest> Guests { get; set; }

        private Guest _selectedGuest;
        public Guest SelectedGuest
        {
            get { return _selectedGuest; }
            set
            {
                _selectedGuest = value;
                OnPropertyChanged(nameof(SelectedGuest));
                LoadSelectedCitizen();
            }
        }

        private Citizen _selectedCitizen;
        public Citizen SelectedCitizen
        {
            get { return _selectedCitizen; }
            set
            {
                _selectedCitizen = value;
                OnPropertyChanged(nameof(SelectedCitizen));
            }
        }

        public ICommand LoadGuestsCommand { get; set; }
        public ICommand CreateGuestCommand { get; set; }
        public ICommand EditGuestCommand { get; set; }
        public ICommand DeleteGuestCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public GuestViewModel(IGuestRepository guestRepository, INavigationService navigationService)
        {
            _guestRepository = guestRepository;
            _navigationService = navigationService;

            Guests = new ObservableCollection<Guest>();

            LoadGuestsCommand = new RelayCommand(LoadGuests);
            CreateGuestCommand = new RelayCommand(CreateGuest);
            EditGuestCommand = new RelayCommand(EditGuest);
            DeleteGuestCommand = new RelayCommand(DeleteGuest);
            BackCommand = new RelayCommand(GoBack);

            LoadGuests();
        }

        public void LoadGuests()
        {
            Guests.Clear();

            foreach (Guest guest in _guestRepository.GetAll())
            {
                Guests.Add(guest);
            }

            SelectedGuest = null;
        }

        private void LoadSelectedCitizen()
        {
            if (SelectedGuest == null)
            {
                SelectedCitizen = null;
                return;
            }

            SelectedCitizen = _guestRepository.GetCitizenForGuest(SelectedGuest.GuestId);
        }

        private void CreateGuest()
        {
            _navigationService.NavigateToCreateGuest();
        }

        private void EditGuest()
        {
            if (SelectedGuest == null)
            {
                MessageBox.Show("Vælg først en gæst fra listen.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _navigationService.NavigateToEditGuest(SelectedGuest);
        }

        private void DeleteGuest()
        {
            if (SelectedGuest == null)
            {
                MessageBox.Show("Vælg først en gæst fra listen.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Er du sikker på, at du vil slette " + SelectedGuest.Name + "?",
                "Bekræft sletning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                Guest guestToRemove = SelectedGuest;

                _guestRepository.Delete(guestToRemove.GuestId);

                Guests.Remove(guestToRemove);
                SelectedGuest = null;
                SelectedCitizen = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gæsten kunne ikke slettes: " + ex.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoBack()
        {
            _navigationService.NavigateToMain();
        }
    }
}
