// See https://aka.ms/new-console-template for more information
using AssetTrackingEntityFrameWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
Console.WriteLine("Hello, World!");
Console.WriteLine("\nDisplaying Assets Table:\n");

{
    using var context = new MyDbContext();
    var assetService = new AssetService(context);

    while (true)
    {
        Console.WriteLine("\n=== Asset Tracking Menu ===");
        Console.WriteLine("1. Add Asset");
        Console.WriteLine("2. Update Asset");
        Console.WriteLine("3. Delete Asset");
        Console.WriteLine("4. Display Sorted Assets (Class & Date)");
        Console.WriteLine("5. Display Grouped Assets by Office");
        Console.WriteLine("6. Add Office");
        Console.WriteLine("7. Exit");
        Console.Write("Select an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddAsset(assetService);
                break;
            case "2":
                UpdateAsset(assetService);
                break;
            case "3":
                DeleteAsset(assetService);
                break;
            case "4":
                DisplaySortedAssets(assetService);
                break;
            case "5":
                DisplayGroupedAssets(assetService);
                break;
            case "6":
                AddOffice(assetService);
                break;
            case "7":
                Console.WriteLine("Exiting... Goodbye!");
                return;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}

static void AddAsset(AssetService assetService)
{
    Console.WriteLine("\n=== Add New Asset ===");

    // Display available offices
    Console.WriteLine("\nAvailable Offices:");
    var offices = assetService.GetAllOffices();
    foreach (var officeObj in offices)
    {
        Console.WriteLine($"- {officeObj.Name}");
    }

    // Get Office Name from User
    Console.Write("\nEnter Office Name: ");
    string officeName = Console.ReadLine();

    // Validate Office Name
    var office = offices.FirstOrDefault(o => o.Name.Equals(officeName, StringComparison.OrdinalIgnoreCase));
    if (office == null)
    {
        Console.WriteLine($"Office '{officeName}' does not exist. Please try again.");
        return;
    }

    // Get Asset Type
    Console.Write("Enter Asset Type (Laptop/Mobile): ");
    string type = Console.ReadLine();

    // Validate Asset Type and Gather Additional Input
    Asset asset;
    if (type.Equals("Laptop", StringComparison.OrdinalIgnoreCase))
    {
        asset = new Laptop();
    }
    else if (type.Equals("Mobile", StringComparison.OrdinalIgnoreCase))
    {
        asset = new Mobile();
    }
    else
    {
        Console.WriteLine("Invalid asset type. Returning to menu...");
        return;
    }

    // Gather Asset Details
    Console.Write("Enter Name: ");
    asset.Name = Console.ReadLine();

    Console.Write("Enter Model Name: ");
    asset.ModelName = Console.ReadLine();

    Console.Write("Enter Purchase Price (USD): ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
    {
        Console.WriteLine("Invalid price entered. Returning to menu...");
        return;
    }
    asset.PurchasePrice = price;

    Console.Write("Enter Purchase Date (yyyy-MM-dd): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime purchaseDate))
    {
        Console.WriteLine("Invalid date entered. Returning to menu...");
        return;
    }
    asset.PurchaseDate = purchaseDate;
    asset.EndOfLifeDate = purchaseDate.AddYears(3);

    // Assign OfficeId
    asset.OfficeId = office.Id;

    // Add Asset to Database
    assetService.AddAsset(asset);
    Console.WriteLine("Asset added successfully!");
}

static void UpdateAsset(AssetService assetService)
{
    Console.Write("Enter Asset ID to Update: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Enter New Name: ");
    string newName = Console.ReadLine();

    Console.Write("Enter New Model Name: ");
    string newModelName = Console.ReadLine();

    assetService.UpdateAsset(id, newName, newModelName);
    Console.WriteLine("Asset updated successfully!");
}

static void DeleteAsset(AssetService assetService)
{
    Console.Write("Enter Asset ID to Delete: ");
    int id = int.Parse(Console.ReadLine());

    assetService.DeleteAsset(id);
    Console.WriteLine("Asset deleted successfully!");
}

static void DisplaySortedAssets(AssetService assetService)
{
    var sortedAssets = assetService.GetAssetsSortedByClassAndDate();
    Console.WriteLine("\nAssets Sorted by Class and Purchase Date:");
    foreach (var asset in sortedAssets)
    {
        string status = assetService.GetStatus(asset);
        Console.WriteLine($"Name: {asset.Name}, Model: {asset.ModelName}, Purchase Date: {asset.PurchaseDate.ToShortDateString()}, Status: {status}");
    }
}

static void DisplayGroupedAssets(AssetService assetService)
{
    var groupedAssets = assetService.GetAssetsGroupedByOffice();
    Console.WriteLine("\nAssets Grouped by Office:");
    foreach (var office in groupedAssets)
    {
        Console.WriteLine($"\nOffice: {office.Key.Name} ({office.Key.Currency})");
        foreach (var asset in office.Value)
        {
            string status = assetService.GetStatus(asset);
            decimal localPrice = asset.PurchasePrice * office.Key.ExchangeRate;
            Console.WriteLine($"Name: {asset.Name}, Model: {asset.ModelName}, Price: {localPrice:F2} {office.Key.Currency}, Status: {status}");
        }
    }
}

static void AddOffice(AssetService assetService)
{
    Console.Write("Enter Office Name: ");
    string name = Console.ReadLine();

    Console.Write("Enter Currency: ");
    string currency = Console.ReadLine();

    Console.Write("Enter Exchange Rate: ");
    decimal exchangeRate = decimal.Parse(Console.ReadLine());

    var office = new Office
    {
        Name = name,
        Currency = currency,
        ExchangeRate = exchangeRate
    };

    assetService.AddOffice(office);
    Console.WriteLine("Office added successfully!");
}
