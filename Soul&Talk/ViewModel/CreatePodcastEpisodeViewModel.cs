using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class CreatePodcastEpisodeViewModel : BaseViewModel
    {
        private readonly IRepository<PodcastEpisode> podcastRepository;
        private readonly IRepository<Guest> guestRepository;
        private readonly PodcastEpisode _podcastEpisode;
        private readonly Action<Action<Guest>> _openGuestDialog;
        private readonly Action _goBack;

        public string Title
        {
            get => _podcastEpisode.Title;
            set { _podcastEpisode.Title = value; OnPropertyChanged(nameof(Title)); }
        }

        public DateTime Date
        {
            get => _podcastEpisode.Date;
            set { _podcastEpisode.Date = value; OnPropertyChanged(nameof(Date)); }
        }

        public int Duration
        {
            get => _podcastEpisode.Duration;
            set { _podcastEpisode.Duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public string Status
        {
            get => _podcastEpisode.Status;
            set { _podcastEpisode.Status = value; OnPropertyChanged(nameof(Status)); }
        }

        public string MeetingPlace
        {
            get => _podcastEpisode.MeetingPlace;
            set { _podcastEpisode.MeetingPlace = value; OnPropertyChanged(nameof(MeetingPlace)); }
        }

        public string Note
        {
            get => _podcastEpisode.Note;
            set { _podcastEpisode.Note = value; OnPropertyChanged(nameof(Note)); }
        }

        public string CaseOfficerName
        {
            get => _podcastEpisode.CaseOfficerName;
            set { _podcastEpisode.CaseOfficerName = value; OnPropertyChanged(nameof(CaseOfficerName)); }
        }

        public ObservableCollection<string> StatusOptions { get; set; }
        public ObservableCollection<Guest> SelectedGuests { get; set; }
        public Guest SelectedEpisodeGuest { get; set; }

        public ICommand AddGuestCommand { get; set; }
        public ICommand RemoveSelectedEpisodeGuestCommand { get; set; }
        public ICommand SavePodcastEpisodeCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public CreatePodcastEpisodeViewModel(
            IRepository<PodcastEpisode> podcastRepo,
            IRepository<Guest> guestRepo,
            Action goBack,
            Action<Action<Guest>> openGuestDialog)
        {
            podcastRepository = podcastRepo;
            guestRepository = guestRepo;
            _goBack = goBack;
            _openGuestDialog = openGuestDialog;

            _podcastEpisode = new PodcastEpisode();
            _podcastEpisode.Date = DateTime.Today;
            _podcastEpisode.CaseOfficerName = string.Empty;

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
            if (_openGuestDialog != null)
            {
                _openGuestDialog(AddGuestToEpisode);
            }
        }

        private void AddGuestToEpisode(Guest guest)
        {
            if (guest == null)
            {
                return;
            }

            if (SelectedGuests.Any(item => item.GuestId == guest.GuestId))
            {
                return;
            }

            SelectedGuests.Add(guest);
        }

        private void RemoveSelectedGuest()
        {
            if (SelectedEpisodeGuest != null)
            {
                SelectedGuests.Remove(SelectedEpisodeGuest);
            }
        }

        public void SavePodcastEpisode() { }

        public void Cancel()
        {
            _goBack?.Invoke();
        }
    }
}
