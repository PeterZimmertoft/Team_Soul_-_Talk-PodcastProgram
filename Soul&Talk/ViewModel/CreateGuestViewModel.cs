using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class CreateGuestViewModel : BaseViewModel
    {
        private IRepository<Guest> guestRepository;
        private IRepository<Citizen> citizenRepository;

        public Guest Guest { get; set; }
        public Citizen Citizen { get; set; }

        public ICommand SaveGuestCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private Action goBack;

        public CreateGuestViewModel(IRepository<Guest> guestRepo, IRepository<Citizen> citizenRepo, Action goBack)
        {
            guestRepository = guestRepo;
            citizenRepository = citizenRepo;
            this.goBack = goBack;

            Guest = new Guest();
            Citizen = new Citizen();

            SaveGuestCommand = new RelayCommand(_ => SaveGuest());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Cancel()
        {
            goBack?.Invoke();
        }

        public void SaveGuest() 
        {
        }
    }
}
