using Android.App;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Models;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "PickUpActivity")]
    public class PickUpActivity : Activity
    {
        private MapFragment _mapFragment;
        private Button _pickupButton;

        private double _lat;
        private double _lng;
        private string _userId;
        private string _deliveryId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.PickUp);

            _pickupButton = FindViewById<Button>(Resource.Id.pickUpButton);
            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.pickUpMapFragment);

            _pickupButton.Click += PickupButton_Click;

            
            _lat = Intent.GetDoubleExtra("latitude", 0);
            _lng = Intent.GetDoubleExtra("longitude", 0);
            _userId = Intent.GetStringExtra("userId");
            _deliveryId = Intent.GetStringExtra("deliveryId");
        }

        private async void PickupButton_Click(object sender, System.EventArgs e)
        {
            await Delivery.MarkAsPickerUp(_deliveryId, _userId);
        }
    }
}