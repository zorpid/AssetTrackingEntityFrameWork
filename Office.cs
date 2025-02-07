using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingEntityFrameWork
{
    public class Office
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal ExchangeRate { get; set; }

        // Navigation Property for Assets
        public List<Asset> Assets { get; set; }
    }
}
