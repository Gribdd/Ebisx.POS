namespace Ebisx.POS.Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("home/iteminventory", typeof(ItemInventoryPage));
        }
    }
}
