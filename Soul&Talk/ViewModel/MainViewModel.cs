using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Soul_Talk.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewmodel {  get; set; }
         
        public ICommand ShowMainViewCommand { get; set; }
        public ICommand ShowGuestViewCommand { get; set; }
        public ICommand ShowPodcastEpisodeViewCommand { get; set; }
        public ICommand ShowCreateGuestViewCommand { get; set; }
        public ICommand ShowCreatePodcastEpisodeViewCommand { get; set; }

        
        public MainViewModel()
        {
            ShowMainViewCommand = new RelayCommand(_ => ShowMainView());
            ShowGuestViewCommand = new RelayCommand(_ => ShowGuestView());
            ShowPodcastEpisodeViewCommand = new RelayCommand(_ => ShowPodcastEpisodeView());
            ShowCreateGuestViewCommand = new RelayCommand(_ => ShowCreateGuestView());
            ShowCreatePodcastEpisodeViewCommand = new RelayCommand(_ => ShowCreatePodcastEpisodeView());
        }
        public void ShowMainView()
        {
            CurrentViewModel = null;
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void ShowGuestView()
        {
            CurrentViewModel = new GuestViewModel(null);
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void ShowPodcastEpisodeView()
        {
            CurrentViewModel = new PodcastEpisodeViewModel(null);
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void ShowCreateGuestView()
        {
            CurrentViewModel = new CreateGuestViewModel(null, null);
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void ShowCreatePodcastEpisodeView()
        {
            CurrentViewModel = new CreatePodcastEpisodeViewModel(null, null, null);
            OnPropertyChanged(nameof(CurrentViewModel));
        }


    }
}
