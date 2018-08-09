using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveriesApp
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public static async Task<bool> Register(string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(password)) return false;
            if (password != confirmPassword) return false;

            var user = new User
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

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
            }
            else
            {
                var user = (await AzureHelper.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

                if (user != null)
                {
                    result = user.Password == password;
                }
            }

            return result;
        }
    }
}
