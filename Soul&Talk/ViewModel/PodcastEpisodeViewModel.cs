using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class PodcastEpisodeViewModel : BaseViewModel
    {
        private readonly IPodcastEpisodeRepository podcastEpisodeRepository;
        private readonly INavigationService navigationService;

        public ObservableCollection<PodcastEpisode> PodcastEpisodes { get; set; }
        public ObservableCollection<Guest> SelectedPodcastEpisodeGuests { get; set; }

        private PodcastEpisode selectedPodcastEpisode;
        public PodcastEpisode SelectedPodcastEpisode
        {
            get { return selectedPodcastEpisode; }
            set
            {
                selectedPodcastEpisode = value;
                OnPropertyChanged(nameof(SelectedPodcastEpisode));
                LoadSelectedPodcastEpisodeGuests();
            }
        }

        public ICommand LoadPodcastEpisodesCommand { get; set; }
        public ICommand CreatePodcastEpisodeCommand { get; set; }
        public ICommand DeletePodcastEpisodeCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public PodcastEpisodeViewModel(IPodcastEpisodeRepository repository, INavigationService navigationService)
        {
            podcastEpisodeRepository = repository;
            this.navigationService = navigationService;

            PodcastEpisodes = new ObservableCollection<PodcastEpisode>();
            SelectedPodcastEpisodeGuests = new ObservableCollection<Guest>();

            LoadPodcastEpisodesCommand = new RelayCommand(LoadPodcastEpisodes);
            CreatePodcastEpisodeCommand = new RelayCommand(CreatePodcastEpisode);
            DeletePodcastEpisodeCommand = new RelayCommand(DeletePodcastEpisode);
            BackCommand = new RelayCommand(GoBack);

            LoadPodcastEpisodes();
        }

        public void LoadPodcastEpisodes()
        {
            PodcastEpisodes.Clear();

            foreach (PodcastEpisode episode in podcastEpisodeRepository.GetAll())
            {
                PodcastEpisodes.Add(episode);
            }
        }

        private void LoadSelectedPodcastEpisodeGuests()
        {
            SelectedPodcastEpisodeGuests.Clear();

            if (SelectedPodcastEpisode == null)
            {
                return;
            }

            foreach (Guest guest in podcastEpisodeRepository.GetGuestsForPodcastEpisode(SelectedPodcastEpisode.PodcastEpisodeID))
            {
                SelectedPodcastEpisodeGuests.Add(guest);
            }
        }

        private void CreatePodcastEpisode()
        {
            navigationService.NavigateToCreatePodcastEpisode();
        }

        private void DeletePodcastEpisode()
        {
            if (SelectedPodcastEpisode == null)
            {
                MessageBox.Show("Vælg en episode først.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            PodcastEpisode episodeToDelete = SelectedPodcastEpisode;
            podcastEpisodeRepository.Delete(episodeToDelete.PodcastEpisodeID);
            PodcastEpisodes.Remove(episodeToDelete);
            SelectedPodcastEpisode = null;
        }

        private void GoBack()
        {
            navigationService.NavigateToMain();
        }
    }
}
