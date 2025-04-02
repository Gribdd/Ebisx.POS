using System.Transactions;
using Ebisx.POS.Presentation.Views.Manager;

namespace Ebisx.POS.Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            RegisterRoutes();
            InitializeComponent();
            var getuserSavedkey = Preferences.Get("UserAlreadyloggedIn", false);

            if (getuserSavedkey == true)
            {
                MyAppShell.CurrentItem = EmployeeHomePage;

            }
            else
            {
                MyAppShell.CurrentItem = LoginPage;

            }
        }

        private void RegisterRoutes()
        {
            //employee
            Routing.RegisterRoute("home/iteminventory", typeof(ItemInventoryPage));

            //mana
            Routing.RegisterRoute("employee", typeof(ManagerEmployeesPage));
            Routing.RegisterRoute("inventory", typeof(ManagerInventoryPage));
            Routing.RegisterRoute("inventory/additem", typeof(ManagerAddInventoryItem));

            Routing.RegisterRoute("sales", typeof(ManagerSalesPage));
            Routing.RegisterRoute("transaction", typeof(ManagerTransactionPage));
        }
    }
}
