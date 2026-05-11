using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class CreateGuestViewModel : BaseViewModel
    {
        private readonly IGuestRepository _guestRepository;
        private readonly ICitizenRepository _citizenRepository;
        private readonly ICaseOfficerRepository _caseOfficerRepository;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly IValidationService _validationService;

        private Guest _guestToEdit;
        private Citizen _citizenToEdit;
        private bool _isEditMode;
        private int? _caseOfficerId;

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
        public string CaseOfficerName { get; set; }

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

        public ICommand SaveGuestCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateGuestViewModel(
            IGuestRepository guestRepository,
            ICitizenRepository citizenRepository,
            ICaseOfficerRepository caseOfficerRepository,
            INavigationService navigationService,
            IMessageService messageService,
            IValidationService validationService)
        {
            _guestRepository = guestRepository;
            _citizenRepository = citizenRepository;
            _caseOfficerRepository = caseOfficerRepository;
            _navigationService = navigationService;
            _messageService = messageService;
            _validationService = validationService;

            _guestToEdit = null;
            _citizenToEdit = null;
            _isEditMode = false;
            _caseOfficerId = null;

            Name = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            CprNumber = string.Empty;
            WorkStatus = string.Empty;
            WorkType = string.Empty;
            ConsentStatus = string.Empty;
            CurrentStatus = string.Empty;
            SpecialConsiderations = string.Empty;
            CaseOfficerName = string.Empty;
            HasCitizenInfo = false;

            SaveGuestCommand = new CreateGuestSaveCommand(this);
            CancelCommand = new CreateGuestCancelNavigationCommand(this);
        }

        public CreateGuestViewModel(
            IGuestRepository guestRepository,
            ICitizenRepository citizenRepository,
            ICaseOfficerRepository caseOfficerRepository,
            INavigationService navigationService,
            IMessageService messageService,
            IValidationService validationService,
            Guest guestToEdit)
            : this(guestRepository, citizenRepository, caseOfficerRepository, navigationService, messageService, validationService)
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
                    CaseOfficerName = GetCaseOfficerName(_citizenToEdit.CaseOfficerId);
                }

                OnPropertyChanged(nameof(PageTitle));
            }
        }

        public void Cancel()
        {
            _navigationService.NavigateToGuest();
        }

        public void SaveGuest()
        {
            if (!BasicGuestInformationIsValid())
            {
                return;
            }

            if (HasCitizenInfo && !CitizenInformationIsValid())
            {
                return;
            }

            if (HasCitizenInfo && !TrySetCaseOfficerId())
            {
                return;
            }

            if (ProfileAlreadyExists())
            {
                _messageService.ShowInfo("Der findes allerede en anden gæst med samme navn, telefon og email.");
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

                _messageService.ShowInfo("Gæsten er gemt.");
                _navigationService.NavigateToGuest();
            }
            catch (Exception ex)
            {
                _messageService.ShowError("Gæsten kunne ikke gemmes: " + ex.Message);
            }
        }

        private bool BasicGuestInformationIsValid()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Email))
            {
                _messageService.ShowWarning("Udfyld venligst navn, telefon og email.");
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
                || string.IsNullOrWhiteSpace(CurrentStatus)
                || string.IsNullOrWhiteSpace(CaseOfficerName))
            {
                _messageService.ShowWarning("Udfyld venligst CPR, jobstatus, arbejdstype, samtykkestatus, nuværende status og sagsbehandler.");
                return false;
            }

            if (!_validationService.IsValidCprNumber(CprNumber))
            {
                _messageService.ShowWarning("Angiv venligst CPR-nummer i formatet DDMMYY-XXXX.");
                return false;
            }

            if (!_validationService.IsValidConsentStatus(ConsentStatus))
            {
                _messageService.ShowWarning("Samtykkestatus skal være Ja eller Nej.");
                return false;
            }

            int currentCitizenId = _citizenToEdit?.CitizenId ?? 0;

            bool cprBelongsToAnotherCitizen = _citizenRepository.CprExistsForAnotherCitizen(
                CprNumber.Trim(),
                currentCitizenId);

            if (cprBelongsToAnotherCitizen)
            {
                _messageService.ShowInfo("Der findes allerede en anden borger med samme CPR-nummer.");
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
            _guestRepository.Add(newGuest, citizenId);
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
            _guestRepository.Update(updatedGuest, citizenId);

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
                SpecialConsiderations,
                _caseOfficerId ?? 0);
        }

        private bool TrySetCaseOfficerId()
        {
            string caseOfficerName = CaseOfficerName.Trim();

            CaseOfficer caseOfficer = _caseOfficerRepository.GetOrCreateByName(caseOfficerName);

            _caseOfficerId = caseOfficer.CaseOfficerId;
            CaseOfficerName = caseOfficer.Name;
            OnPropertyChanged(nameof(CaseOfficerName));
            return true;
        }

        private string GetCaseOfficerName(int caseOfficerId)
        {
            if (caseOfficerId <= 0)
            {
                return string.Empty;
            }

            CaseOfficer caseOfficer = _caseOfficerRepository
                .GetAll()
                .FirstOrDefault(co => co.CaseOfficerId == caseOfficerId);

            return caseOfficer?.Name ?? string.Empty;
        }

    }
}
