using Soul_Talk.Commands;
using Soul_Talk.Services;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public BaseViewModel? CurrentViewModel
        {
            get { return _navigationService.CurrentViewModel; }
        }

        public ICommand ShowMainViewCommand { get; }
        public ICommand ShowGuestViewCommand { get; }
        public ICommand ShowPodcastEpisodeViewCommand { get; }
        public ICommand ShowCreateGuestViewCommand { get; }
        public ICommand ShowCreatePodcastEpisodeViewCommand { get; }

        public MainViewModel(string connectionString)
        {
            _navigationService = new NavigationService(connectionString);
            _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ShowMainViewCommand = new ShowMainViewNavigationCommand(_navigationService);
            ShowGuestViewCommand = new ShowGuestViewNavigationCommand(_navigationService);
            ShowPodcastEpisodeViewCommand = new ShowPodcastEpisodeViewNavigationCommand(_navigationService);
            ShowCreateGuestViewCommand = new ShowCreateGuestViewNavigationCommand(_navigationService);
            ShowCreatePodcastEpisodeViewCommand = new ShowCreatePodcastEpisodeViewNavigationCommand(_navigationService);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
