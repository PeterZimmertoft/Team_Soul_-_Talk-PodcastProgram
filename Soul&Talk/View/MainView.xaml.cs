using System.Windows;
using Soul_Talk.ViewModel;

namespace Soul_Talk.View
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}