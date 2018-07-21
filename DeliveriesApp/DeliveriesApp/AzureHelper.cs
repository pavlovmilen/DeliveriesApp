using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace DeliveriesApp
{
    public class AzureHelper
    {
        public static  MobileServiceClient MobileService = new MobileServiceClient("https://jukadeliveriesapp.azurewebsites.net");

        public static async Task<bool> Login(string email, string password)
        {
            var result = false;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                result = false;
            }
            else
            {
                var user = (await MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

                if (user != null)
                {
                    result = user.Password == password;
                }
            }

            return result;
        }

        public static async Task<bool> Register(string email, string password, string confirmPassword)
        {
            var result = false;

            if(!string.IsNullOrEmpty(password) )
            {
                if (password == confirmPassword)
                {
                    var user = new User
                    {
                        Email = email,
                        Password = password
                    };

                    await AzureHelper.MobileService.GetTable<User>().InsertAsync(user);

                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
