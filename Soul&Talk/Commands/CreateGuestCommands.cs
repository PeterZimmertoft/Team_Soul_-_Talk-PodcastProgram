using Soul_Talk.ViewModel;

namespace Soul_Talk.Commands
{
    public class CreateGuestSaveCommand : BaseCommand
    {
        private readonly CreateGuestViewModel viewModel;

        public CreateGuestSaveCommand(CreateGuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.SaveGuest();
        }
    }

    public class CreateGuestCancelNavigationCommand : BaseCommand
    {
        private readonly CreateGuestViewModel viewModel;

        public CreateGuestCancelNavigationCommand(CreateGuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.Cancel();
        }
    }
}