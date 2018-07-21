using System.Linq;
using System.Threading.Tasks;

namespace DeliveriesApp.Models
{
    public class DeliveryPerson
    {
        public string Id { get; set; }

        public static async Task<DeliveryPerson> GetDeliveryPerson(string id)
        {
            var deliveryPerson = (await AzureHelper.MobileService.GetTable<DeliveryPerson>().Where(d => d.Id == id).ToListAsync())
                .FirstOrDefault();

            return deliveryPerson;
        }
    }
}
