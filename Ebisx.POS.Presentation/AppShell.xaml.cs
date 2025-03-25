using System.Transactions;
using Ebisx.POS.Presentation.Views.Manager;

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
            //employee
            Routing.RegisterRoute("home/iteminventory", typeof(ItemInventoryPage));

            //mana
            Routing.RegisterRoute("managerhome/employee", typeof(ManagerEmployeesPage));
            Routing.RegisterRoute("managerhome/inventory", typeof(ManagerInventoryPage));
            Routing.RegisterRoute("managerhome/sales", typeof(ManagerSalesPage));
            Routing.RegisterRoute("managerhome/transaction", typeof(ManagerTransactionPage));
        }
    }
}
