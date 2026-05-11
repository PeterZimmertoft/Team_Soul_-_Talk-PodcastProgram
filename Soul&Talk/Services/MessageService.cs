using System.Windows;

namespace Soul_Talk.Services
{
    public class MessageService : IMessageService
    {
        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool Confirm(string message, string title)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            return result == MessageBoxResult.Yes;
        }
    }
}
