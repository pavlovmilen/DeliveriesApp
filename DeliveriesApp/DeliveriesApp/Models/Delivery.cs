using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace DeliveriesApp.Models
{
    public class Delivery
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double OriginLatitude { get; set; }
        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }

        /// <summary>
        /// 0 = waining for delivery person
        /// 1 = being delivered
        /// 2 = delivered
        /// </summary>
        public int Status { get; set; }

        public static async Task<List<Delivery>> GetDeliveries()
        {
            var deliveries = await AzureHelper.MobileService.GetTable<Delivery>().ToListAsync();

            return deliveries;
        }

        public static async Task<bool> InsertDelivery(Delivery delivery)
        {
            var result = await AzureHelper.Insert(delivery);

            return result;
        }
    }
}
