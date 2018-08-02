using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Models;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "NewDelivery")]
    public class NewDeliveryActivity : Activity, IOnMapReadyCallback, ILocationListener
    {
        readonly string [] PermissionsLocation = 
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };
 
        const int RequestLocationId = 0;

        private Button _saveButton;
        private EditText _packageNamEditText;
        private MapFragment _mapFragment;
        private MapFragment _destinationMapFragment;
        private double _latitude, _longitude;
        private LocationManager _locationManager;

        private View _layout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewDeivery2);
            _layout = FindViewById(Resource.Id.sample_main_layout);

            _saveButton = FindViewById<Button>(Resource.Id.saveButton);
            _packageNamEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            _mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragment);
            _destinationMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.destinationMapFragment);
  
            _saveButton.Click += OnSave_Clicked;
        }

        protected override async void OnResume()
        {
            base.OnResume();   

            GetLocationPermission();
        }


        private void RequestLocationUpdates()
        {
            _locationManager = GetSystemService(Context.LocationService) as LocationManager;
            var provider = LocationManager.GpsProvider;

            if (_locationManager.IsProviderEnabled(provider))
            {
                _locationManager.RequestLocationUpdates(provider, 5000, 1, this);
            }

            var location = _locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
            if (location != null)
            {
                _latitude = location.Latitude;
                _longitude = location.Longitude;
            }

            _mapFragment.GetMapAsync(this);
            _destinationMapFragment.GetMapAsync(this);
        }

        private void GetLocationPermission()
        {
            //Check to see if any permission in our group is available, if one, then all are
            const string permission = Manifest.Permission.AccessFineLocation;
            if (CheckSelfPermission(permission) == (int)Permission.Granted)
            {
                RequestLocationUpdates();
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                //Explain to the user why we need to read the contacts
                Snackbar.Make(_layout, "Location access is required to show coffee shops nearby.", Snackbar.LengthIndefinite)
                    .SetAction("OK", v => RequestPermissions(PermissionsLocation, RequestLocationId))
                    .Show();
                return;
            }
            //Finally request permissions with the list of permissions and Id
            RequestPermissions(PermissionsLocation, RequestLocationId);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                {
                    if (grantResults[0] == Permission.Granted)
                    {
                        //Permission granted
                        var snack = Snackbar.Make(_layout, "Location permission is available, getting lat/long.", Snackbar.LengthShort);
                        snack.Show();

                        RequestLocationUpdates();
                    }
                    else
                    {
                        //Permission Denied :(
                        //Disabling location functionality
                        var snack = Snackbar.Make(_layout, "Location permission is denied.", Snackbar.LengthShort);
                        snack.Show();
                    }
                }
                    break;
            }
        }

        protected override void OnPause()
        {
            _locationManager?.RemoveUpdates(this);

            base.OnPause();
        }

        private async void OnSave_Clicked(object sender, EventArgs e)
        {
            var origin = _mapFragment.Map.CameraPosition.Target;
            var destinationLocation = _destinationMapFragment.Map.CameraPosition.Target;

            var delivery = new Delivery
            {
                Name = _packageNamEditText.Text,
                Status = 0,
                OriginLatitude = origin.Latitude,
                OriginLongitude = origin.Longitude,
                DestinationLatitude = destinationLocation.Latitude,
                DestinationLongitude = destinationLocation.Longitude
            };

            await Delivery.InsertDelivery(delivery);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(_latitude, _longitude));
            markerOptions.SetTitle("Your location");
            googleMap.AddMarker(markerOptions);

            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(_latitude, _longitude), 10));
        }

        public void OnLocationChanged(Location location)
        {
            _latitude = location.Latitude;
            _longitude = location.Longitude;
            _mapFragment.GetMapAsync(this);
            _destinationMapFragment.GetMapAsync(this);
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