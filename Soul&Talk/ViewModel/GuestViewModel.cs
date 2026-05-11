using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class GuestViewModel : BaseViewModel
    {
        private readonly IGuestRepository _guestRepository;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;

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

        public ICommand LoadGuestsCommand { get; }
        public ICommand CreateGuestCommand { get; }
        public ICommand EditGuestCommand { get; }
        public ICommand DeleteGuestCommand { get; }
        public ICommand BackCommand { get; }

        public GuestViewModel(IGuestRepository guestRepository, INavigationService navigationService, IMessageService messageService)
        {
            _guestRepository = guestRepository;
            _navigationService = navigationService;
            _messageService = messageService;

            Guests = new ObservableCollection<Guest>();

            LoadGuestsCommand = new GuestLoadCommand(this);
            CreateGuestCommand = new GuestCreateNavigationCommand(this);
            EditGuestCommand = new GuestEditCommand(this);
            DeleteGuestCommand = new GuestDeleteCommand(this);
            BackCommand = new GuestBackNavigationCommand(this);

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

        public void CreateGuest()
        {
            _navigationService.NavigateToCreateGuest();
        }

        public void EditGuest()
        {
            if (SelectedGuest == null)
            {
                _messageService.ShowInfo("Vælg først en gæst fra listen.");
                return;
            }

            _navigationService.NavigateToEditGuest(SelectedGuest);
        }

        public void DeleteGuest()
        {
            if (SelectedGuest == null)
            {
                _messageService.ShowInfo("Vælg først en gæst fra listen.");
                return;
            }

            bool confirmed = _messageService.Confirm(
                "Er du sikker på, at du vil slette " + SelectedGuest.Name + "?",
                "Bekræft sletning");

            if (!confirmed)
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
                _messageService.ShowError("Gæsten kunne ikke slettes: " + ex.Message);
            }
        }

        public void GoBack()
        {
            _navigationService.NavigateToMain();
        }
    }
}
