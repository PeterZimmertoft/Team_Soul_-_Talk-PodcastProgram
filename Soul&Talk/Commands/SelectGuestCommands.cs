using Soul_Talk.ViewModel;

namespace Soul_Talk.Commands
{
    public class SelectGuestLoadCommand : BaseCommand
    {
        private readonly SelectGuestViewModel viewModel;

        public SelectGuestLoadCommand(SelectGuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.LoadGuests();
        }
    }

    public class SelectGuestConfirmCommand : BaseCommand
    {
        private readonly SelectGuestViewModel viewModel;

        public SelectGuestConfirmCommand(SelectGuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.ConfirmGuestSelection();
        }
    }

    public class SelectGuestCancelCommand : BaseCommand
    {
        private readonly SelectGuestViewModel viewModel;

        public SelectGuestCancelCommand(SelectGuestViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.Cancel();
        }
    }
}