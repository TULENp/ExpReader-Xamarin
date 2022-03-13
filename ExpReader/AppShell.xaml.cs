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
        }

    }
}
