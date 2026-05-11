using Soul_Talk.ViewModel;
using System;

namespace Soul_Talk.Stores
{
    public class NavigationStore
    {
        private BaseViewModel? currentViewModel;

        public BaseViewModel? CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public event Action? CurrentViewModelChanged;
    }
}