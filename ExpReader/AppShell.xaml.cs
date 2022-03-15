using ExpReader.Views;
using Xamarin.Forms;

namespace ExpReader
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
           Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            // Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            //Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            // Routing.RegisterRoute(nameof(UserLibPage), typeof(UserLibPage));
           // Routing.RegisterRoute("//LoginPage", typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }

    }
}
