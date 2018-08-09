using System.Linq;
using System.Threading.Tasks;

namespace DeliveriesApp.Models
{
    public class DeliveryPerson
    {
        public string Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public static async Task<DeliveryPerson> GetDeliveryPerson(string id)
        {
            var deliveryPerson = (await AzureHelper.MobileService.GetTable<DeliveryPerson>().Where(d => d.Id == id).ToListAsync())
                .FirstOrDefault();

            return deliveryPerson;
        }

        public static async Task<bool> Register(string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (password != confirmPassword) return false;

            var user = new DeliveryPerson
            {
                Email = email,
                Password = password
            };

            await AzureHelper.Insert(user);

            return true;
        }

        public static async Task<bool> Login(string email, string password)
        {
            var result = false;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
            }
            else
            {
                var user = (await AzureHelper.MobileService.GetTable<DeliveryPerson>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

                if (user != null)
                {
                    result = user.Password == password;
                }
            }

            return result;
        }
    }
}
