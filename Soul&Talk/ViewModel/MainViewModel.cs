using Microsoft.Extensions.Configuration;
using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.View;
using System.Windows.Input;


namespace Soul_Talk.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
       
        private readonly IRepository<Guest> _guestRepository;
        private readonly IRepository<Citizen> _citizenRepository;
        private readonly IRepository<PodcastEpisode> _podcastRepository;
        private readonly IRepository<CaseOfficer> _caseOfficerRepository;

        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
        }

        public ICommand ShowMainViewCommand { get; set; }
        public ICommand ShowGuestViewCommand { get; set; }
        public ICommand ShowPodcastEpisodeViewCommand { get; set; }
        public ICommand ShowCreateGuestViewCommand { get; set; }
        public ICommand ShowCreatePodcastEpisodeViewCommand { get; set; }

        public MainViewModel()
        {
        
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

  
            _guestRepository = new GuestRepository(connectionString);
            _citizenRepository = new CitizenRepository(connectionString);
            _podcastRepository = new PodcastEpisodeRepository(connectionString);
            _caseOfficerRepository = new CaseOfficerRepository(connectionString);

            ShowMainViewCommand = new RelayCommand(_ => ShowMainView());
            ShowGuestViewCommand = new RelayCommand(_ => ShowGuestView());
            ShowPodcastEpisodeViewCommand = new RelayCommand(_ => ShowPodcastEpisodeView());
            ShowCreateGuestViewCommand = new RelayCommand(_ => ShowCreateGuestView());
            ShowCreatePodcastEpisodeViewCommand = new RelayCommand(_ => ShowCreatePodcastEpisodeView());
        }

        public void ShowMainView() => CurrentViewModel = null;

        public void ShowGuestView()
        {
            CurrentViewModel = new GuestViewModel(_guestRepository, ShowCreateGuestView, ShowMainView);
        }

        public void ShowPodcastEpisodeView()
        {
            CurrentViewModel = new PodcastEpisodeViewModel(_podcastRepository, ShowMainView);
        }

        public void ShowCreateGuestView()
        {
            CurrentViewModel = new CreateGuestViewModel(_guestRepository, _citizenRepository, ShowGuestView);
        }

        public void ShowCreatePodcastEpisodeView()
        {
            CurrentViewModel = new CreatePodcastEpisodeViewModel(_podcastRepository, _guestRepository, _caseOfficerRepository, ShowPodcastEpisodeView);
        }
    }
}