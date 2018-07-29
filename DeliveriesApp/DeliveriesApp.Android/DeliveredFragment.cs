using Android.OS;
using Android.Widget;
using DeliveriesApp.Droid.Adapters;
using DeliveriesApp.Models;

namespace DeliveriesApp.Droid
{
    public class DeliveredFragment : Android.Support.V4.App.ListFragment
    {
        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var delivered = await Delivery.GetCompletedDeliveries();

            ListAdapter = new DeliveryAdapter(Activity, delivered);
        }
    }
}