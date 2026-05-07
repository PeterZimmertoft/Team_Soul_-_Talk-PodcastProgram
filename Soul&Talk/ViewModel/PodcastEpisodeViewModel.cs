using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class PodcastEpisodeViewModel : BaseViewModel
    {
        private readonly IRepository<PodcastEpisode> podcastEpisodeRepository;
        private readonly Action _goBackAction;
        private readonly Action _createPodcastEpisodeAction;

        public ObservableCollection<PodcastEpisode> PodcastEpisodes { get; set; }
        public PodcastEpisode SelectedPodcastEpisode { get; set; }

        public ICommand LoadPodcastEpisodesCommand { get; set; }
        public ICommand CreatePodcastEpisodeCommand { get; set; }
        public ICommand EditPodcastEpisodeCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public PodcastEpisodeViewModel(IRepository<PodcastEpisode> repository, Action goBack, Action createPodcastEpisode)
        {
            podcastEpisodeRepository = repository;
            _goBackAction = goBack;
            _createPodcastEpisodeAction = createPodcastEpisode;

            PodcastEpisodes = new ObservableCollection<PodcastEpisode>();

            LoadPodcastEpisodesCommand = new RelayCommand(LoadPodcastEpisodes);
            CreatePodcastEpisodeCommand = new RelayCommand(CreatePodcastEpisode);
            EditPodcastEpisodeCommand = new RelayCommand(EditPodcastEpisode);
            BackCommand = new RelayCommand(GoBack);

            LoadPodcastEpisodes();
        }

        public void LoadPodcastEpisodes()
        {
            PodcastEpisodes.Clear();

            foreach (var episode in podcastEpisodeRepository.GetAll())
            {
                PodcastEpisodes.Add(episode);
            }
        }

        public void CreatePodcastEpisode()
        {
            _createPodcastEpisodeAction?.Invoke();
        }
        public void EditPodcastEpisode() { }
        public void GoBack()
        {
            _goBackAction?.Invoke();
        }
    }
}
