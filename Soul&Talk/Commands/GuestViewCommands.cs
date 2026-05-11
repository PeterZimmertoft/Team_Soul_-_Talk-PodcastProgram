using Soul_Talk.ViewModel;

namespace Soul_Talk.Commands
{
    public class GuestLoadCommand : BaseCommand
    {
        private readonly GuestViewModel viewModel;

        public GuestLoadCommand(GuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.LoadGuests();
        }
    }

    public class GuestCreateNavigationCommand : BaseCommand
    {
        private readonly GuestViewModel viewModel;

        public GuestCreateNavigationCommand(GuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.CreateGuest();
        }
    }

    public class GuestEditCommand : BaseCommand
    {
        private readonly GuestViewModel viewModel;

        public GuestEditCommand(GuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.EditGuest();
        }
    }

    public class GuestDeleteCommand : BaseCommand
    {
        private readonly GuestViewModel viewModel;

        public GuestDeleteCommand(GuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.DeleteGuest();
        }
    }

    public class GuestBackNavigationCommand : BaseCommand
    {
        private readonly GuestViewModel viewModel;

        public GuestBackNavigationCommand(GuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.GoBack();
        }
    }
}