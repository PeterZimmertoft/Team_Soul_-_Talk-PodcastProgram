using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class PodcastEpisodeViewModel
    {
        private IPodcastEpisodeRepository podcastEpisodeRepository;

        public ObservableCollection<PodcastEpisode> PodcastEpisodes { get; set; }
        public PodcastEpisode SelectedPodcastEpisode { get; set; }

        public ICommand LoadPodcastEpisodesCommand { get; set; }
        public ICommand CreatePodcastEpisodeCommand { get; set; }
        public ICommand EditPodcastEpisodeCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public PodcastEpisodeViewModel(IPodcastEpisodeRepository repository)
        {
            podcastEpisodeRepository = repository;

            PodcastEpisodes = new ObservableCollection<PodcastEpisode>();

            LoadPodcastEpisodesCommand = new RelayCommand(_ => LoadPodcastEpisodes());
            CreatePodcastEpisodeCommand = new RelayCommand(_ => CreatePodcastEpisode());
            EditPodcastEpisodeCommand = new RelayCommand(_ => EditPodcastEpisode());
            BackCommand = new RelayCommand(_ => GoBack());
        }

        public void LoadPodcastEpisodes() { }
        public void CreatePodcastEpisode() { }
        public void EditPodcastEpisode() { }
        public void GoBack() { }
    }
}
