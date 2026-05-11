using Soul_Talk.ViewModel;

namespace Soul_Talk.Commands
{
    public class CreatePodcastEpisodeAddGuestCommand : BaseCommand
    {
        private readonly CreatePodcastEpisodeViewModel viewModel;

        public CreatePodcastEpisodeAddGuestCommand(CreatePodcastEpisodeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.AddSelectedGuest();
        }
    }

    public class CreatePodcastEpisodeRemoveSelectedGuestCommand : BaseCommand
    {
        private readonly CreatePodcastEpisodeViewModel viewModel;

        public CreatePodcastEpisodeRemoveSelectedGuestCommand(CreatePodcastEpisodeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.RemoveSelectedGuest();
        }
    }

    public class CreatePodcastEpisodeSaveCommand : BaseCommand
    {
        private readonly CreatePodcastEpisodeViewModel viewModel;

        public CreatePodcastEpisodeSaveCommand(CreatePodcastEpisodeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.SavePodcastEpisode();
        }
    }

    public class CreatePodcastEpisodeCancelNavigationCommand : BaseCommand
    {
        private readonly CreatePodcastEpisodeViewModel viewModel;

        public CreatePodcastEpisodeCancelNavigationCommand(CreatePodcastEpisodeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.Cancel();
        }
    }
}