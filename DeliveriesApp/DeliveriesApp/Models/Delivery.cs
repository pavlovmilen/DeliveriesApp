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

        public static async Task<List<Delivery>> GetActiveDeliveries()
        {
            var deliveries = await AzureHelper.MobileService.GetTable<Delivery>()
                .Where(d => d.Status != 2)
                .ToListAsync();

            return deliveries;
        }

        public static async Task<List<Delivery>> GetCompletedDeliveries()
        {
            var deliveries = await AzureHelper.MobileService.GetTable<Delivery>()
                .Where(d => d.Status == 2)
                .ToListAsync();

            return deliveries;
        }

        public static async Task<bool> InsertDelivery(Delivery delivery)
        {
            var result = await AzureHelper.Insert(delivery);

            return result;
        }

        public string GetStatusForDelivery(int status)
        {
            string toReturn;
            switch (status)
            {
                case 0:
                    toReturn = "Waiting for delivery person.";
                    break;
                case 1:
                    toReturn = "Out for delivery";
                    break;
                default:
                    toReturn = "Delivered";
                    break;
            }

            return toReturn;
        }

        public override string ToString()
        {
            return $"{Name} - {Status}";
        }
    }
}
