using MauiAppCoach.View;

namespace MauiAppCoach
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(HistoPage), typeof(HistoPage));
        }
    }
}
