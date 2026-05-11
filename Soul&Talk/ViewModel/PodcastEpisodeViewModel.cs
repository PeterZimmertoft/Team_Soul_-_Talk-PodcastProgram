using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class PodcastEpisodeViewModel : BaseViewModel
    {
        private readonly IPodcastEpisodeRepository podcastEpisodeRepository;
        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;

        public ObservableCollection<PodcastEpisode> PodcastEpisodes { get; set; }
        public ObservableCollection<Guest> SelectedPodcastEpisodeGuests { get; set; }

        private PodcastEpisode? selectedPodcastEpisode;
        public PodcastEpisode? SelectedPodcastEpisode
        {
            get { return selectedPodcastEpisode; }
            set
            {
                selectedPodcastEpisode = value;
                OnPropertyChanged(nameof(SelectedPodcastEpisode));
                LoadSelectedPodcastEpisodeGuests();
            }
        }

        public ICommand LoadPodcastEpisodesCommand { get; }
        public ICommand CreatePodcastEpisodeCommand { get; }
        public ICommand DeletePodcastEpisodeCommand { get; }
        public ICommand BackCommand { get; }

        public PodcastEpisodeViewModel(
            IPodcastEpisodeRepository repository,
            INavigationService navigationService,
            IMessageService messageService)
        {
            podcastEpisodeRepository = repository;
            this.navigationService = navigationService;
            this.messageService = messageService;

            PodcastEpisodes = new ObservableCollection<PodcastEpisode>();
            SelectedPodcastEpisodeGuests = new ObservableCollection<Guest>();

            LoadPodcastEpisodesCommand = new PodcastEpisodeLoadCommand(this);
            CreatePodcastEpisodeCommand = new PodcastEpisodeCreateNavigationCommand(this);
            DeletePodcastEpisodeCommand = new PodcastEpisodeDeleteCommand(this);
            BackCommand = new PodcastEpisodeBackNavigationCommand(this);

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

        public void CreatePodcastEpisode()
        {
            navigationService.NavigateToCreatePodcastEpisode();
        }

        public void DeletePodcastEpisode()
        {
            if (SelectedPodcastEpisode == null)
            {
                messageService.ShowInfo("Vælg en episode først.");
                return;
            }

            PodcastEpisode episodeToDelete = SelectedPodcastEpisode;

            bool confirmed = messageService.Confirm(
                $"Er du sikker på, at du vil slette podcast-episoden:\n\n{episodeToDelete.Title}",
                "Bekræft sletning");

            if (!confirmed)
            {
                return;
            }

            try
            {
                podcastEpisodeRepository.Delete(episodeToDelete.PodcastEpisodeID);

                PodcastEpisodes.Remove(episodeToDelete);
                SelectedPodcastEpisodeGuests.Clear();
                SelectedPodcastEpisode = null;

                messageService.ShowInfo("Podcast-episoden er slettet.");
            }
            catch (Exception ex)
            {
                messageService.ShowError("Podcast-episoden kunne ikke slettes: " + ex.Message);
            }
        }

        public void GoBack()
        {
            navigationService.NavigateToMain();
        }
    }
}