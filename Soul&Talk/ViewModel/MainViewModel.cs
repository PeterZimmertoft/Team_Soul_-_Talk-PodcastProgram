using System.Windows.Input;
using Soul_Talk.Commands;
using Soul_Talk.View;


namespace Soul_Talk.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand ShowMainViewCommand { get; set; }
        public ICommand ShowGuestViewCommand { get; set; }
        public ICommand ShowPodcastEpisodeViewCommand { get; set; }
        public ICommand ShowCreateGuestViewCommand { get; set; }
        public ICommand ShowCreatePodcastEpisodeViewCommand { get; set; }

        public MainViewModel()
        {
            ShowMainViewCommand = new RelayCommand(_ => ShowMainView());
            ShowGuestViewCommand = new RelayCommand(_ => ShowGuestView());
            ShowPodcastEpisodeViewCommand = new RelayCommand(_ => ShowPodcastEpisodeView());
            ShowCreateGuestViewCommand = new RelayCommand(_ => ShowCreateGuestView());
            ShowCreatePodcastEpisodeViewCommand = new RelayCommand(_ => ShowCreatePodcastEpisodeView());
        }

        public void ShowMainView()
        {
            CurrentViewModel = null;
        }

        public void ShowGuestView()
        {
            CurrentViewModel = new GuestViewModel(ShowMainView, ShowCreateGuestView);
        }

        public void ShowPodcastEpisodeView()
        {
            CurrentViewModel = new PodcastEpisodeViewModel(ShowMainView, ShowCreatePodcastEpisodeView);
        }

        public void ShowCreateGuestView()
        {
            CurrentViewModel = new CreateGuestViewModel(ShowGuestView);
        }

        public void ShowCreatePodcastEpisodeView()
        {
            CurrentViewModel = new CreatePodcastEpisodeViewModel(ShowPodcastEpisodeView);
        }
    }
}