using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Windows;
using System.Windows.Input;


namespace Soul_Talk.ViewModel
{
    public class CreateGuestViewModel : BaseViewModel
    {

        private IRepository<Guest> guestRepository;
        private IRepository<Citizen> citizenRepository;

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CprNumber { get; set; }
        public string WorkStatus { get; set; }
        public string WorkType { get; set; }
        public string ConsentStatus { get; set; }
        public string CurrentStatus { get; set; }
        public string SpecialConsiderations { get; set; }

        private readonly Action _goBack;

        public ICommand SaveGuestCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateGuestViewModel(IRepository<Guest> guestRepo, IRepository<Citizen> citizenRepo, Action goBack)
        {
            guestRepository = guestRepo;
            citizenRepository = citizenRepo;
            _goBack = goBack;

            Name = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            CprNumber = string.Empty;
            WorkStatus = string.Empty;
            WorkType = string.Empty;
            ConsentStatus = string.Empty;
            CurrentStatus = string.Empty;
            SpecialConsiderations = string.Empty;

            SaveGuestCommand = new RelayCommand(SaveGuest);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Cancel()
        {
            _goBack?.Invoke();
        }

        public void SaveGuest() 
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Udfyld venligst navn, telefon og email.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidCprNumber(CprNumber))
            {
                MessageBox.Show("Angiv venligst et korrekt CPR-nummer. Brug følgende format: \"DDMMYY-XXXX\"", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Gem logikken kommer her.
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
    }
}
