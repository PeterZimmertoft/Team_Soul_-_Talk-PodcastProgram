using Soul_Talk.Model;
using Soul_Talk.ViewModel;
using System;

namespace Soul_Talk.Services
{
    public interface INavigationService
    {
        BaseViewModel CurrentViewModel { get; }

        event Action CurrentViewModelChanged;

        void NavigateToMain();
        void NavigateToGuest();
        void NavigateToCreateGuest();
        void NavigateToEditGuest(Guest guest);
        void NavigateToPodcastEpisode();
        void NavigateToCreatePodcastEpisode();

        void OpenSelectGuestDialog(Action<Guest> onGuestSelected);
    }
}
