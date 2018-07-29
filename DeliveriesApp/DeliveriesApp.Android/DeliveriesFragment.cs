using Android.OS;
using Android.Widget;
using DeliveriesApp.Droid.Adapters;
using DeliveriesApp.Models;

namespace DeliveriesApp.Droid
{
    public class DeliveriesFragment : Android.Support.V4.App.ListFragment
    {
        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var deliveries = await Delivery.GetActiveDeliveries();

            ListAdapter = new DeliveryAdapter(Activity, deliveries);
        }
    }
}