namespace Ebisx.POS.Presentation.Common;

public static class AppRoutes
{
    // Employee Routes
    public const string Home = "///home";
    public const string ItemInventory = "//home/iteminventory";

    // Manager Routes
    public const string ManagerHome = "///managerhome";
    public const string ManagerEmployees = "//managerhome/employees";
    public const string ManagerInventory = "//managerhome/inventory";
    public const string ManagerAddInventoryItem = "//managerhome/inventory/additem";
    public const string ManagerSales = "//managerhome/sales";
    public const string ManagerTransaction = "//managerhome/transactions";

    public const string Login = "///mainpage";
}

