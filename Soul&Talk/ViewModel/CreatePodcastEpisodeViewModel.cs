using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class CreatePodcastEpisodeViewModel : BaseViewModel
    {
        private readonly IPodcastEpisodeRepository podcastRepository;
        private readonly INavigationService navigationService;
        private readonly PodcastEpisode podcastEpisode;

        public string Title
        {
            get { return podcastEpisode.Title; }
            set { podcastEpisode.Title = value; OnPropertyChanged(nameof(Title)); }
        }

        public DateTime Date
        {
            get { return podcastEpisode.Date; }
            set { podcastEpisode.Date = value; OnPropertyChanged(nameof(Date)); }
        }

        public int Duration
        {
            get { return podcastEpisode.Duration; }
            set { podcastEpisode.Duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public string Status
        {
            get { return podcastEpisode.Status; }
            set { podcastEpisode.Status = value; OnPropertyChanged(nameof(Status)); }
        }

        public string MeetingPlace
        {
            get { return podcastEpisode.MeetingPlace; }
            set { podcastEpisode.MeetingPlace = value; OnPropertyChanged(nameof(MeetingPlace)); }
        }

        public string CaseOfficerName
        {
            get { return podcastEpisode.CaseOfficerName; }
            set { podcastEpisode.CaseOfficerName = value; OnPropertyChanged(nameof(CaseOfficerName)); }
        }

        public string Note
        {
            get { return podcastEpisode.Note; }
            set { podcastEpisode.Note = value; OnPropertyChanged(nameof(Note)); }
        }

        public ObservableCollection<string> StatusOptions { get; set; }
        public ObservableCollection<Guest> SelectedGuests { get; set; }

        private Guest selectedEpisodeGuest;
        public Guest SelectedEpisodeGuest
        {
            get { return selectedEpisodeGuest; }
            set
            {
                selectedEpisodeGuest = value;
                OnPropertyChanged(nameof(SelectedEpisodeGuest));
            }
        }

        public ICommand AddGuestCommand { get; set; }
        public ICommand RemoveSelectedEpisodeGuestCommand { get; set; }
        public ICommand SavePodcastEpisodeCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreatePodcastEpisodeViewModel(IPodcastEpisodeRepository podcastRepository, INavigationService navigationService)
        {
            this.podcastRepository = podcastRepository;
            this.navigationService = navigationService;

            podcastEpisode = new PodcastEpisode();
            podcastEpisode.Date = DateTime.Today;
            podcastEpisode.Status = "Planlagt";
            podcastEpisode.CaseOfficerName = string.Empty;

            StatusOptions = new ObservableCollection<string>
            {
                "Planlagt",
                "Afholdt",
                "Aflyst"
            };

            SelectedGuests = new ObservableCollection<Guest>();

            AddGuestCommand = new RelayCommand(AddSelectedGuest);
            RemoveSelectedEpisodeGuestCommand = new RelayCommand(RemoveSelectedGuest);
            SavePodcastEpisodeCommand = new RelayCommand(SavePodcastEpisode);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void AddSelectedGuest()
        {
            navigationService.OpenSelectGuestDialog(AddGuestToEpisode);
        }

        private void AddGuestToEpisode(Guest guest)
        {
            if (guest == null)
            {
                return;
            }

            if (SelectedGuests.Any(g => g.GuestId == guest.GuestId))
            {
                return;
            }

            SelectedGuests.Add(guest);
        }

        private void RemoveSelectedGuest()
        {
            if (SelectedEpisodeGuest == null)
            {
                return;
            }

            SelectedGuests.Remove(SelectedEpisodeGuest);
            SelectedEpisodeGuest = null;
        }

        private void SavePodcastEpisode()
        {
            if (!InputIsValid())
            {
                return;
            }

            int podcastEpisodeId = podcastRepository.Add(podcastEpisode);

            foreach (Guest guest in SelectedGuests)
            {
                podcastRepository.AddGuestToPodcastEpisode(podcastEpisodeId, guest.GuestId);
            }

            MessageBox.Show("Podcast-episoden er gemt.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            navigationService.NavigateToPodcastEpisode();
        }

        private bool InputIsValid()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageBox.Show("Udfyld venligst titel.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (Duration <= 0)
            {
                MessageBox.Show("Varighed skal være større end 0.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Status))
            {
                MessageBox.Show("Vælg venligst status.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(MeetingPlace))
            {
                MessageBox.Show("Udfyld venligst mødested.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(CaseOfficerName))
            {
                MessageBox.Show("Udfyld venligst sagsbehandler.", "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void Cancel()
        {
            navigationService.NavigateToPodcastEpisode();
        }
    }
}
