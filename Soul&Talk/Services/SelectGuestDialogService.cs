using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.View;
using Soul_Talk.ViewModel;
using System;
using System.Windows;

namespace Soul_Talk.Services
{
    public class SelectGuestDialogService : ISelectGuestDialogService
    {
        private readonly IGuestRepository guestRepository;

        public SelectGuestDialogService(IGuestRepository guestRepository)
        {
            this.guestRepository = guestRepository;
        }

        public void OpenSelectGuestDialog(Action<Guest> onGuestSelected)
        {
            SelectGuestDialog dialog = new SelectGuestDialog();
            dialog.Owner = Application.Current.MainWindow;
            dialog.DataContext = new SelectGuestViewModel(guestRepository, onGuestSelected, () => dialog.Close());
            dialog.ShowDialog();
        }
    }
}