using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AssetTrackingEntityFrameWork
{
    public class AssetService
    {
        private readonly MyDbContext _context;

        public AssetService(MyDbContext context)
        {
            _context = context;
        }

        // Add Asset
        public void AddAsset(Asset asset)
        {
            _context.Assets.Add(asset);
            _context.SaveChanges();
        }

        // Update Asset
        public void UpdateAsset(int id, string newName, string newModelName)
        {
            var asset = _context.Assets.SingleOrDefault(a => a.Id == id);
            if (asset != null)
            {
                asset.Name = newName;
                asset.ModelName = newModelName;
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Asset not found.");
            }
        }

        // Delete Asset
        public void DeleteAsset(int id)
        {
            var asset = _context.Assets.SingleOrDefault(a => a.Id == id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Asset not found.");
            }
        }

        // Add Office
        public void AddOffice(Office office)
        {
            _context.Offices.Add(office);
            _context.SaveChanges();
        }

        // Retrieve All Assets Sorted by Class and Purchase Date
        public List<Asset> GetAssetsSortedByClassAndDate()
        {
            return _context.Assets
                .Include(a => a.Office)
                .AsEnumerable()
                .OrderBy(a => a.GetType().Name) // Sort by class (Laptop first, then Mobile)
                .ThenBy(a => a.PurchaseDate)  // Then by purchase date
                .ToList();
        }

        // Retrieve Assets Grouped by Office
        public Dictionary<Office, List<Asset>> GetAssetsGroupedByOffice()
        {
            return _context.Assets
                .Include(a => a.Office)
                .ToList()
                .GroupBy(a => a.Office)
                .OrderBy(g => g.Key.Name)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Retrieve All Offices
        public List<Office> GetAllOffices()
        {
            return _context.Offices.ToList();
        }

        // Get Status of Asset
        public string GetStatus(Asset asset)
        {
            var monthsRemaining = (asset.EndOfLifeDate - DateTime.Now).TotalDays / 30;
            if (monthsRemaining < 3) return "RED";
            if (monthsRemaining < 6) return "YELLOW";
            return "NORMAL";
        }
    }
}
