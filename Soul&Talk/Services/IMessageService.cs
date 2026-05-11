namespace Soul_Talk.Services
{
    public interface IMessageService
    {
        void ShowInfo(string message);
        void ShowWarning(string message);
        void ShowError(string message);
        bool Confirm(string message, string title);
    }
}
