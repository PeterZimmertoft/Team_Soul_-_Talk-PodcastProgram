using Soul_Talk.Commands;
using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class CreatePodcastEpisodeViewModel : BaseViewModel
    {
        private readonly IPodcastEpisodeRepository podcastRepository;
        private readonly ICaseOfficerRepository caseOfficerRepository;
        private readonly ISelectGuestDialogService selectGuestDialogService;
        private readonly INavigationService navigationService;
        private readonly IMessageService messageService;
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

        private int durationHours;
        public int DurationHours
        {
            get { return durationHours; }
            set
            {
                durationHours = value;
                podcastEpisode.Duration = TimeSpan.FromHours(durationHours);
                OnPropertyChanged(nameof(DurationHours));
            }
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

        public ICommand AddGuestCommand { get; }
        public ICommand RemoveSelectedEpisodeGuestCommand { get; }
        public ICommand SavePodcastEpisodeCommand { get; }
        public ICommand CancelCommand { get; }

        public CreatePodcastEpisodeViewModel(
            IPodcastEpisodeRepository podcastRepository,
            ICaseOfficerRepository caseOfficerRepository,
            ISelectGuestDialogService selectGuestDialogService,
            INavigationService navigationService,
            IMessageService messageService)
        {
            this.podcastRepository = podcastRepository;
            this.caseOfficerRepository = caseOfficerRepository;
            this.selectGuestDialogService = selectGuestDialogService;
            this.navigationService = navigationService;
            this.messageService = messageService;

            podcastEpisode = new PodcastEpisode();
            podcastEpisode.Date = DateTime.Today;
            podcastEpisode.Status = "Planlagt";
            podcastEpisode.CaseOfficerName = string.Empty;
            DurationHours = 1;

            StatusOptions = new ObservableCollection<string>
            {
                "Planlagt",
                "Afholdt",
                "Aflyst"
            };

            SelectedGuests = new ObservableCollection<Guest>();

            AddGuestCommand = new CreatePodcastEpisodeAddGuestCommand(this);
            RemoveSelectedEpisodeGuestCommand = new CreatePodcastEpisodeRemoveSelectedGuestCommand(this);
            SavePodcastEpisodeCommand = new CreatePodcastEpisodeSaveCommand(this);
            CancelCommand = new CreatePodcastEpisodeCancelNavigationCommand(this);
        }

        public void AddSelectedGuest()
        {
            selectGuestDialogService.OpenSelectGuestDialog(AddGuestToEpisode);
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

        public void RemoveSelectedGuest()
        {
            if (SelectedEpisodeGuest == null)
            {
                return;
            }

            SelectedGuests.Remove(SelectedEpisodeGuest);
            SelectedEpisodeGuest = null;
        }

        public void SavePodcastEpisode()
        {
            if (!InputIsValid())
            {
                return;
            }

            if (!TrySetCaseOfficerId())
            {
                return;
            }

            int podcastEpisodeId = podcastRepository.Add(podcastEpisode);

            foreach (Guest guest in SelectedGuests)
            {
                podcastRepository.AddGuestToPodcastEpisode(podcastEpisodeId, guest.GuestId);
            }

            messageService.ShowInfo("Podcast-episoden er gemt.");
            navigationService.NavigateToPodcastEpisode();
        }

        private bool InputIsValid()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                messageService.ShowWarning("Udfyld venligst titel.");
                return false;
            }

            if (DurationHours <= 0)
            {
                messageService.ShowWarning("Varighed skal være større end 0.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Status))
            {
                messageService.ShowWarning("Vælg venligst status.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(MeetingPlace))
            {
                messageService.ShowWarning("Udfyld venligst mødested.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(CaseOfficerName))
            {
                messageService.ShowWarning("Udfyld venligst sagsbehandler.");
                return false;
            }

            return true;
        }

        private bool TrySetCaseOfficerId()
        {
            string caseOfficerName = CaseOfficerName.Trim();

            CaseOfficer caseOfficer = caseOfficerRepository.GetOrCreateByName(caseOfficerName);

            podcastEpisode.CaseOfficerId = caseOfficer.CaseOfficerId;
            podcastEpisode.CaseOfficerName = caseOfficer.Name;
            return true;
        }

        public void Cancel()
        {
            navigationService.NavigateToPodcastEpisode();
        }
    }
}
