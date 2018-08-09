using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace DeliveriesApp
{
    public class AzureHelper
    {
        public static  MobileServiceClient MobileService = new MobileServiceClient("https://jukadeliveriesapp.azurewebsites.net");

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
