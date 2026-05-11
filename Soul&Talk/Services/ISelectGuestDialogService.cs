using Soul_Talk.Model;
using System;

namespace Soul_Talk.Services
{
    public interface ISelectGuestDialogService
    {
        void OpenSelectGuestDialog(Action<Guest> onGuestSelected);
    }
}