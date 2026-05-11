using Soul_Talk.Services;

namespace Soul_Talk.Commands
{
    public class ShowMainViewNavigationCommand : BaseCommand
    {
        private readonly INavigationService navigationService;

        public ShowMainViewNavigationCommand(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            navigationService.NavigateToMain();
        }
    }

    public class ShowGuestViewNavigationCommand : BaseCommand
    {
        private readonly INavigationService navigationService;

        public ShowGuestViewNavigationCommand(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            navigationService.NavigateToGuest();
        }
    }

    public class ShowPodcastEpisodeViewNavigationCommand : BaseCommand
    {
        private readonly INavigationService navigationService;

        public ShowPodcastEpisodeViewNavigationCommand(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            navigationService.NavigateToPodcastEpisode();
        }
    }

    public class ShowCreateGuestViewNavigationCommand : BaseCommand
    {
        private readonly INavigationService navigationService;

        public ShowCreateGuestViewNavigationCommand(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            navigationService.NavigateToCreateGuest();
        }
    }

    public class ShowCreatePodcastEpisodeViewNavigationCommand : BaseCommand
    {
        private readonly INavigationService navigationService;

        public ShowCreatePodcastEpisodeViewNavigationCommand(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            navigationService.NavigateToCreatePodcastEpisode();
        }
    }
}