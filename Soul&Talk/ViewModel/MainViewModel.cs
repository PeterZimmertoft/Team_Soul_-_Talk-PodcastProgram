using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.View;
using System;
using System.Windows;
using System.Windows.Input;


namespace Soul_Talk.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        // Repositories
        private readonly IRepository<Guest> _guestRepository;
        private readonly IRepository<Citizen> _citizenRepository;
        private readonly IRepository<PodcastEpisode> _podcastRepository;
        private readonly IRepository<CaseOfficer> _caseOfficerRepository;

        // Current ViewModel
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
        }

        // Commands
        public ICommand ShowMainViewCommand { get; set; }
        public ICommand ShowGuestViewCommand { get; set; }
        public ICommand ShowPodcastEpisodeViewCommand { get; set; }
        public ICommand ShowCreateGuestViewCommand { get; set; }
        public ICommand ShowCreatePodcastEpisodeViewCommand { get; set; }

        // Constructor
        public MainViewModel(string connectionString)
        {
            _guestRepository = new GuestRepository(connectionString);
            _citizenRepository = new CitizenRepository(connectionString);
            _podcastRepository = new PodcastEpisodeRepository(connectionString);
            _caseOfficerRepository = new CaseOfficerRepository(connectionString);

            ShowMainViewCommand = new RelayCommand(ShowMainView);
            ShowGuestViewCommand = new RelayCommand(ShowGuestView);
            ShowPodcastEpisodeViewCommand = new RelayCommand(ShowPodcastEpisodeView);
            ShowCreateGuestViewCommand = new RelayCommand(ShowCreateGuestView);
            ShowCreatePodcastEpisodeViewCommand = new RelayCommand(ShowCreatePodcastEpisodeView);
        }

        // Metoder til at skifte mellem views
        public void ShowMainView() => CurrentViewModel = null;

        public void ShowGuestView()
        {
            CurrentViewModel = new GuestViewModel(_guestRepository, ShowCreateGuestView, ShowMainView);
        }

        public void ShowPodcastEpisodeView()
        {
            CurrentViewModel = new PodcastEpisodeViewModel(_podcastRepository, ShowMainView, ShowCreatePodcastEpisodeView);
        }

        public void ShowCreateGuestView()
        {
            CurrentViewModel = new CreateGuestViewModel(_guestRepository, _citizenRepository, ShowGuestView);
        }

        public void ShowCreatePodcastEpisodeView()
        {
            CurrentViewModel = new CreatePodcastEpisodeViewModel(_podcastRepository, _guestRepository, ShowPodcastEpisodeView, OpenSelectGuestDialog);
        }

        private void OpenSelectGuestDialog(Action<Guest> onGuestSelected)
        {
            SelectGuestDialog dialog = new SelectGuestDialog();
            dialog.Owner = Application.Current.MainWindow;
            dialog.DataContext = new SelectGuestViewModel((IGuestRepository)_guestRepository, onGuestSelected, () => dialog.Close());
            dialog.ShowDialog();
        }
    }
}