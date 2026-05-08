using Soul_Talk.Commands;
using Soul_Talk.Services;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public BaseViewModel CurrentViewModel
        {
            get { return _navigationService.CurrentViewModel; }
        }

        public ICommand ShowMainViewCommand { get; set; }
        public ICommand ShowGuestViewCommand { get; set; }
        public ICommand ShowPodcastEpisodeViewCommand { get; set; }
        public ICommand ShowCreateGuestViewCommand { get; set; }
        public ICommand ShowCreatePodcastEpisodeViewCommand { get; set; }

        public MainViewModel(string connectionString)
        {
            _navigationService = new NavigationService(connectionString);
            _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ShowMainViewCommand = new RelayCommand(ShowMainView);
            ShowGuestViewCommand = new RelayCommand(ShowGuestView);
            ShowPodcastEpisodeViewCommand = new RelayCommand(ShowPodcastEpisodeView);
            ShowCreateGuestViewCommand = new RelayCommand(ShowCreateGuestView);
            ShowCreatePodcastEpisodeViewCommand = new RelayCommand(ShowCreatePodcastEpisodeView);
        }

        private void ShowMainView()
        {
            _navigationService.NavigateToMain();
        }

        private void ShowGuestView()
        {
            _navigationService.NavigateToGuest();
        }

        private void ShowPodcastEpisodeView()
        {
            _navigationService.NavigateToPodcastEpisode();
        }

        private void ShowCreateGuestView()
        {
            _navigationService.NavigateToCreateGuest();
        }

        private void ShowCreatePodcastEpisodeView()
        {
            _navigationService.NavigateToCreatePodcastEpisode();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
