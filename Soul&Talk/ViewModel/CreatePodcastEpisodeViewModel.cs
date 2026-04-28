using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class CreatePodcastEpisodeViewModel
    {
        private IPodcastEpisodeRepository podcastRepository;
        private IGuestRepository guestRepository;
        private ICaseOfficerRepository caseOfficerRepository;

        public PodcastEpisode PodcastEpisode { get; set; }

        public ObservableCollection<CaseOfficer> CaseOfficers { get; set; }
        public CaseOfficer SelectedCaseOfficer { get; set; }

        public ObservableCollection<Guest> SelectedGuests { get; set; }

        public ICommand AddGuestCommand { get; set; }
        public ICommand SavePodcastEpisodeCommand { get; set; }

        public CreatePodcastEpisodeViewModel(
            IPodcastEpisodeRepository podcastRepo,
            IGuestRepository guestRepo,
            ICaseOfficerRepository caseOfficerRepo)

        {
            podcastRepository = podcastRepo;
            guestRepository = guestRepo;
            caseOfficerRepository = caseOfficerRepo;

            PodcastEpisode = new PodcastEpisode();
            CaseOfficers = new ObservableCollection<CaseOfficer>();
            SelectedGuests = new ObservableCollection<Guest>();

            AddGuestCommand = new RelayCommand(g => AddGuest((Guest)g));
            SavePodcastEpisodeCommand = new RelayCommand(_ => SavePodcastEpisode());
        }

        public void AddGuest(Guest guest)
        {
            if (guest != null)
                SelectedGuests.Add(guest);
        }

        public void SavePodcastEpisode() { }
        public void Cancel() { }
    }
}
