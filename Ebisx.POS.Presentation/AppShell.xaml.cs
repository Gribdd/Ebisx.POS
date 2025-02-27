namespace Ebisx.POS.Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("home/iteminventory", typeof(ItemInventoryPage));
        }
    }
}
