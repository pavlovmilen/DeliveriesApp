using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace DeliveriesApp.Models
{
    public class Delivery
    {
        public string Id { get; set; }

        public static async Task<List<Delivery>> GetDeliveries()
        {
            var deliveries = await AzureHelper.MobileService.GetTable<Delivery>().ToListAsync();

            return deliveries;
        }
    }
}
