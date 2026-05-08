using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.View;
using Soul_Talk.ViewModel;
using System;
using System.Windows;

namespace Soul_Talk.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IGuestRepository guestRepository;
        private readonly ICitizenRepository citizenRepository;
        private readonly IPodcastEpisodeRepository podcastEpisodeRepository;

        private BaseViewModel currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            private set
            {
                currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public event Action CurrentViewModelChanged;

        public NavigationService(string connectionString)
        {
            guestRepository = new GuestRepository(connectionString);
            citizenRepository = new CitizenRepository(connectionString);
            podcastEpisodeRepository = new PodcastEpisodeRepository(connectionString);
        }

        public void NavigateToMain()
        {
            CurrentViewModel = null;
        }

        public void NavigateToGuest()
        {
            CurrentViewModel = new GuestViewModel(guestRepository, this);
        }

        public void NavigateToCreateGuest()
        {
            CurrentViewModel = new CreateGuestViewModel(guestRepository, citizenRepository, this);
        }

        public void NavigateToEditGuest(Guest guest)
        {
            CurrentViewModel = new CreateGuestViewModel(guestRepository, citizenRepository, this, guest);
        }

        public void NavigateToPodcastEpisode()
        {
            CurrentViewModel = new PodcastEpisodeViewModel(podcastEpisodeRepository, this);
        }

        public void NavigateToCreatePodcastEpisode()
        {
            CurrentViewModel = new CreatePodcastEpisodeViewModel(podcastEpisodeRepository, this);
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
