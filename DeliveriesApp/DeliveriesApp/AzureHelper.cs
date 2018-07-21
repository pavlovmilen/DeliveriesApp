using System;
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
            if (string.IsNullOrEmpty(password)) return false;
            if (password != confirmPassword) return false;

            var user = new User
            {
                Email = email,
                Password = password
            };

            await Insert(user);

            return true;
        }

        public static async Task<bool> Insert<T>(T objectToInsert)
        {
            try
            {
                await MobileService.GetTable<T>().InsertAsync(objectToInsert);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
