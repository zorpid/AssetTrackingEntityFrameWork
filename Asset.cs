using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingEntityFrameWork
{
    public abstract class Asset
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string ModelName { get; set; } 
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; } 
        public DateTime EndOfLifeDate { get; set; } 

        // Foreign Key and Navigation Property for Office
        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
    
}
