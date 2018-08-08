using Android.App;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "PickUpActivity")]
    public class PickUpActivity : Activity
    {
        private MapFragment _mapFragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.PickUp);

            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.pickUpMapFragment);
        }
    }
}