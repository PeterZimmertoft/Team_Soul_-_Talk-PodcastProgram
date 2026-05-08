using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class CreateGuestViewModel : BaseViewModel
    {
        private readonly IGuestRepository _guestRepository;
        private readonly ICitizenRepository _citizenRepository;
        private readonly INavigationService _navigationService;

        private Guest _guestToEdit;
        private Citizen _citizenToEdit;
        private bool _isEditMode;

        public string PageTitle
        {
            get { return _isEditMode ? "Rediger gæsteprofil" : "Opret gæsteprofil"; }
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CprNumber { get; set; }
        public string WorkStatus { get; set; }
        public string WorkType { get; set; }
        public string ConsentStatus { get; set; }
        public string CurrentStatus { get; set; }
        public string SpecialConsiderations { get; set; }

        private bool _hasCitizenInfo;
        public bool HasCitizenInfo
        {
            get { return _hasCitizenInfo; }
            set
            {
                _hasCitizenInfo = value;
                OnPropertyChanged(nameof(HasCitizenInfo));
            }
        }

        public ICommand SaveGuestCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateGuestViewModel(IGuestRepository guestRepository, ICitizenRepository citizenRepository, INavigationService navigationService)
        {
            _guestRepository = guestRepository;
            _citizenRepository = citizenRepository;
            _navigationService = navigationService;

            _guestToEdit = null;
            _citizenToEdit = null;
            _isEditMode = false;

            Name = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            CprNumber = string.Empty;
            WorkStatus = string.Empty;
            WorkType = string.Empty;
            ConsentStatus = string.Empty;
            CurrentStatus = string.Empty;
            SpecialConsiderations = string.Empty;
            HasCitizenInfo = false;

            SaveGuestCommand = new RelayCommand(SaveGuest);
            CancelCommand = new RelayCommand(Cancel);
        }

        public CreateGuestViewModel(IGuestRepository guestRepository, ICitizenRepository citizenRepository, INavigationService navigationService, Guest guestToEdit)
            : this(guestRepository, citizenRepository, navigationService)
        {
            _guestToEdit = guestToEdit;
            _isEditMode = guestToEdit != null;

            if (_isEditMode)
            {
                Name = guestToEdit.Name;
                Phone = guestToEdit.Phone;
                Email = guestToEdit.Email;

                _citizenToEdit = _guestRepository.GetCitizenForGuest(guestToEdit.GuestId);

                if (_citizenToEdit != null)
                {
                    HasCitizenInfo = true;
                    CprNumber = _citizenToEdit.CprNumber;
                    WorkStatus = _citizenToEdit.WorkStatus;
                    WorkType = _citizenToEdit.WorkType;
                    ConsentStatus = _citizenToEdit.ConsentStatus;
                    CurrentStatus = _citizenToEdit.CurrentStatus;
                    SpecialConsiderations = _citizenToEdit.SpecialConsiderations;
                }

                OnPropertyChanged(nameof(PageTitle));
            }
        }

        private void Cancel()
        {
            _navigationService.NavigateToGuest();
        }

        private void SaveGuest()
        {
            if (!BasicGuestInformationIsValid())
            {
                return;
            }

            if (HasCitizenInfo && !CitizenInformationIsValid())
            {
                return;
            }

            if (ProfileAlreadyExists())
            {
                MessageBox.Show("Der findes allerede en anden gæst med samme navn, telefon og email.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                if (_isEditMode)
                {
                    UpdateExistingGuest();
                }
                else
                {
                    CreateNewGuest();
                }

                MessageBox.Show("Gæsten er gemt.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                _navigationService.NavigateToGuest();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gæsten kunne ikke gemmes: " + ex.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool BasicGuestInformationIsValid()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Udfyld venligst navn, telefon og email.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool CitizenInformationIsValid()
        {
            if (string.IsNullOrWhiteSpace(CprNumber)
                || string.IsNullOrWhiteSpace(WorkStatus)
                || string.IsNullOrWhiteSpace(WorkType)
                || string.IsNullOrWhiteSpace(ConsentStatus)
                || string.IsNullOrWhiteSpace(CurrentStatus))
            {
                MessageBox.Show("Udfyld venligst CPR, jobstatus, arbejdstype, samtykkestatus og nuværende status.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!IsValidCprNumber(CprNumber))
            {
                MessageBox.Show("Angiv venligst CPR-nummer i formatet DDMMYY-XXXX.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!IsValidConsentStatus(ConsentStatus))
            {
                MessageBox.Show("Samtykkestatus skal være Ja eller Nej.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            int currentCitizenId = _citizenToEdit == null ? 0 : _citizenToEdit.CitizenId;

            if (_citizenRepository.CprExistsForAnotherCitizen(CprNumber, currentCitizenId))
            {
                MessageBox.Show("Der findes allerede en anden borger med samme CPR-nummer.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private bool ProfileAlreadyExists()
        {
            if (_isEditMode)
            {
                return _guestRepository.ProfileExistsForAnotherGuest(Name, Phone, Email, _guestToEdit.GuestId);
            }

            return _guestRepository.ProfileExists(Name, Phone, Email);
        }

        private void CreateNewGuest()
        {
            int? citizenId = null;

            if (HasCitizenInfo)
            {
                Citizen newCitizen = BuildCitizen(0);
                citizenId = _citizenRepository.Add(newCitizen);
            }

            Guest newGuest = BuildGuest(0);
            _guestRepository.AddGuest(newGuest, citizenId);
        }

        private void UpdateExistingGuest()
        {
            int? citizenId = null;
            int? oldCitizenId = _guestRepository.GetCitizenIdForGuest(_guestToEdit.GuestId);

            if (HasCitizenInfo)
            {
                if (_citizenToEdit != null)
                {
                    Citizen updatedCitizen = BuildCitizen(_citizenToEdit.CitizenId);
                    _citizenRepository.Update(updatedCitizen);
                    citizenId = _citizenToEdit.CitizenId;
                }
                else
                {
                    Citizen newCitizen = BuildCitizen(0);
                    citizenId = _citizenRepository.Add(newCitizen);
                }
            }

            Guest updatedGuest = BuildGuest(_guestToEdit.GuestId);
            _guestRepository.UpdateGuest(updatedGuest, citizenId);

            if (!HasCitizenInfo && oldCitizenId.HasValue)
            {
                _citizenRepository.Delete(oldCitizenId.Value);
            }
        }

        private Guest BuildGuest(int guestId)
        {
            return new Guest
            {
                GuestId = guestId,
                Name = Name.Trim(),
                Phone = Phone.Trim(),
                Email = Email.Trim()
            };
        }

        private Citizen BuildCitizen(int citizenId)
        {
            return new Citizen(
                citizenId,
                Name.Trim(),
                Phone.Trim(),
                Email.Trim(),
                CprNumber.Trim(),
                WorkStatus.Trim(),
                WorkType.Trim(),
                ConsentStatus.Trim(),
                CurrentStatus.Trim(),
                SpecialConsiderations);
        }

        private bool IsValidCprNumber(string cprNumber)
        {
            if (string.IsNullOrWhiteSpace(cprNumber) || cprNumber.Length != 11 || cprNumber[6] != '-')
            {
                return false;
            }

            for (int i = 0; i < 6; i++)
            {
                if (!char.IsDigit(cprNumber[i]))
                {
                    return false;
                }
            }

            for (int i = 7; i < 11; i++)
            {
                if (!char.IsDigit(cprNumber[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidConsentStatus(string consentStatus)
        {
            if (string.IsNullOrWhiteSpace(consentStatus))
            {
                return false;
            }

            string value = consentStatus.Trim().ToLower();
            return value == "ja" || value == "nej" || value == "true" || value == "false" || value == "1" || value == "0";
        }
    }
}
