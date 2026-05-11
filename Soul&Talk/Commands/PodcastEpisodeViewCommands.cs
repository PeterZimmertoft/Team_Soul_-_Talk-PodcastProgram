using Soul_Talk.ViewModel;

namespace Soul_Talk.Commands
{
    public class PodcastEpisodeLoadCommand : BaseCommand
    {
        private readonly PodcastEpisodeViewModel viewModel;

        public PodcastEpisodeLoadCommand(PodcastEpisodeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.LoadPodcastEpisodes();
        }
    }

    public class PodcastEpisodeCreateNavigationCommand : BaseCommand
    {
        private readonly PodcastEpisodeViewModel viewModel;

        public PodcastEpisodeCreateNavigationCommand(PodcastEpisodeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.CreatePodcastEpisode();
        }
    }

    public class PodcastEpisodeDeleteCommand : BaseCommand
    {
        private readonly PodcastEpisodeViewModel viewModel;

        public PodcastEpisodeDeleteCommand(PodcastEpisodeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.DeletePodcastEpisode();
        }
    }

    public class PodcastEpisodeBackNavigationCommand : BaseCommand
    {
        private readonly PodcastEpisodeViewModel viewModel;

        public PodcastEpisodeBackNavigationCommand(PodcastEpisodeViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.GoBack();
        }
    }
}