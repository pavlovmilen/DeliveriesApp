using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Widget;
using DeliveriesApp.Models;


namespace DeliveryPersonApp.Android
{
    [Activity(Label = "DeliverActivity")]
    public class DeliverActivity : Activity, IOnMapReadyCallback
    {
        private MapFragment _mapFragment;
        private    Button _deliverButton;
        private double _lat;
        private double _lng;
        private string _deliveryId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Deliver);

            _deliverButton = FindViewById<Button>(Resource.Id.deliverButton);
            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.deliverMapFragment);

            _deliverButton.Click += DeliverButton_Click;

            _lat = Intent.GetDoubleExtra("lattitude", 0);
            _lng = Intent.GetDoubleExtra("longitude", 0);
            _deliveryId = Intent.GetStringExtra("deliveryId");

            _mapFragment.GetMapAsync(this);
        }

        private async void DeliverButton_Click(object sender, System.EventArgs e)
        {
            await Delivery.MarkAsDelivered(_deliveryId);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(_lat, _lng));
            markerOptions.SetTitle("Deliver here");
            googleMap.AddMarker(markerOptions);
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(_lat, _lng), 12));
        }
    }
}