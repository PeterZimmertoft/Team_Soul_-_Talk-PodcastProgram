using Soul_Talk.Model;
using Soul_Talk.Persistence__Repositories_;
using Soul_Talk.Stores;
using Soul_Talk.ViewModel;
using System;

namespace Soul_Talk.Services
{
    public class NavigationService : INavigationService
    {
        private readonly NavigationStore navigationStore;
        private readonly IGuestRepository guestRepository;
        private readonly ICitizenRepository citizenRepository;
        private readonly IPodcastEpisodeRepository podcastEpisodeRepository;
        private readonly ICaseOfficerRepository caseOfficerRepository;
        private readonly ISelectGuestDialogService selectGuestDialogService;
        private readonly IMessageService messageService;
        private readonly IValidationService validationService;

        public BaseViewModel? CurrentViewModel => navigationStore.CurrentViewModel;

        public event Action CurrentViewModelChanged
        {
            add => navigationStore.CurrentViewModelChanged += value;
            remove => navigationStore.CurrentViewModelChanged -= value;
        }

        public NavigationService(string connectionString)
        {
            navigationStore = new NavigationStore();
            guestRepository = new GuestRepository(connectionString);
            citizenRepository = new CitizenRepository(connectionString);
            podcastEpisodeRepository = new PodcastEpisodeRepository(connectionString);
            caseOfficerRepository = new CaseOfficerRepository(connectionString);
            selectGuestDialogService = new SelectGuestDialogService(guestRepository);
            messageService = new MessageService();
            validationService = new ValidationService();
        }

        public void NavigateToMain()
        {
            navigationStore.CurrentViewModel = null;
        }

        public void NavigateToGuest()
        {
            navigationStore.CurrentViewModel = new GuestViewModel(guestRepository, this, messageService);
        }

        public void NavigateToCreateGuest()
        {
            navigationStore.CurrentViewModel = new CreateGuestViewModel(
                guestRepository,
                citizenRepository,
                caseOfficerRepository,
                this,
                messageService,
                validationService);
        }

        public void NavigateToEditGuest(Guest guest)
        {
            navigationStore.CurrentViewModel = new CreateGuestViewModel(
                guestRepository,
                citizenRepository,
                caseOfficerRepository,
                this,
                messageService,
                validationService,
                guest);
        }

        public void NavigateToPodcastEpisode()
        {
            navigationStore.CurrentViewModel = new PodcastEpisodeViewModel(podcastEpisodeRepository, this, messageService);
        }

        public void NavigateToCreatePodcastEpisode()
        {
            navigationStore.CurrentViewModel = new CreatePodcastEpisodeViewModel(
                podcastEpisodeRepository,
                caseOfficerRepository,
                selectGuestDialogService,
                this,
                messageService);
        }
    }
}
