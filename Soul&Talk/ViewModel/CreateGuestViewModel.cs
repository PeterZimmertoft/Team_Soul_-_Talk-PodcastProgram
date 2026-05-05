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
        private IGuestRepository guestRepository;
        private ICitizenRepository citizenRepository;

        public Guest Guest { get; set; }
        public Citizen Citizen { get; set; }

        public ICommand SaveGuestCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreateGuestViewModel(IGuestRepository guestRepo, ICitizenRepository citizenRepo)
        {
            guestRepository = guestRepo;
            citizenRepository = citizenRepo;

            Guest = new Guest();
            Citizen = new Citizen();

            SaveGuestCommand = new RelayCommand(_ => SaveGuest());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public void SaveGuest() { }
        public void Cancel() { }
    }
}
