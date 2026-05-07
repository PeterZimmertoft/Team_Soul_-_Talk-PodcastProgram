using Microsoft.Extensions.Configuration;
using System.Windows;
using Soul_Talk.View;
using Soul_Talk.ViewModel;


namespace Soul_Talk
{
    public partial class App : Application
    {

        //Denne metode bliver kaldt, når applikationen starter. Den indlæser konfigurationen fra appsettings.json, henter forbindelsesstrengen og opretter hovedvinduet (MainView) med det tilhørende ViewModel (MainViewModel), som får forbindelsesstrengen som parameter.
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string? connectionString = config.GetConnectionString("MyDBConnection");


            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'MyDBConnection' blev ikke fundet i appsettings.json.");
            }

            MainView mainView = new MainView();
            mainView.DataContext = new MainViewModel(connectionString);
            mainView.Show();
        }
    }

}
