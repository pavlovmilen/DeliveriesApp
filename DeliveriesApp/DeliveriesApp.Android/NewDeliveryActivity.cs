using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Models;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "NewDelivery")]
    public class NewDeliveryActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        private Button _saveButton;
        private EditText _packageNamEditText;
        private MapFragment _mapFragment;
        private double _latitude, _longitude;
        private LocationManager _locationManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewDeivery2);

            _saveButton = FindViewById<Button>(Resource.Id.saveButton);
            _packageNamEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragment);
  
            _saveButton.Click += OnSave_Clicked;
        }

        protected override void OnResume()
        {
            base.OnResume();

            _locationManager = GetSystemService(Context.LocationService) as LocationManager;
            var provider = LocationManager.GpsProvider;
            if (_locationManager.IsProviderEnabled(provider))
            {
                _locationManager.RequestLocationUpdates(provider, 5000, 1000, this);
            }

        }

        protected override void OnPause()
        {
            _locationManager.RemoveUpdates(this);
            base.OnPause();
        }

        private async void OnSave_Clicked(object sender, EventArgs e)
        {
            var delivery = new Delivery
            {
                Name = _packageNamEditText.Text,
                Status = 0
            };

            await Delivery.InsertDelivery(delivery);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(_latitude, _longitude));
            markerOptions.SetTitle("Your location");
            googleMap.AddMarker(markerOptions);
        }

        public void OnLocationChanged(Location location)
        {
            _latitude = location.Latitude;
            _longitude = location.Longitude;
            _mapFragment.GetMapAsync(this);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }
    }
}