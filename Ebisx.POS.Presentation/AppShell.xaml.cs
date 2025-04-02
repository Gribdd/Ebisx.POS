using Ebisx.POS.Presentation.Common;
using Ebisx.POS.Presentation.Services.Interface;
using Ebisx.POS.Presentation.Views.Manager;

namespace Ebisx.POS.Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            RegisterRoutes();
            InitializeComponent();
            
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(AppRoutes.Home, typeof(HomePage));
            Routing.RegisterRoute(AppRoutes.Login, typeof(MainPage));
            Routing.RegisterRoute(AppRoutes.ManagerHome, typeof(ManagerHomePage));
            Routing.RegisterRoute(AppRoutes.ItemInventory, typeof(ItemInventoryPage));
            Routing.RegisterRoute(AppRoutes.ManagerEmployees, typeof(ManagerEmployeesPage));
            Routing.RegisterRoute(AppRoutes.ManagerInventory, typeof(ManagerInventoryPage));
            Routing.RegisterRoute(AppRoutes.ManagerAddInventoryItem, typeof(ManagerAddInventoryItem));
            Routing.RegisterRoute(AppRoutes.ManagerSales, typeof(ManagerSalesPage));
            Routing.RegisterRoute(AppRoutes.ManagerTransaction, typeof(ManagerTransactionPage));
        }

    }
}
