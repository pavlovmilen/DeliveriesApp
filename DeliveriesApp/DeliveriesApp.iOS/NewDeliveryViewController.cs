using Foundation;
using System;
using CoreLocation;
using DeliveriesApp.Models;
using MapKit;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class NewDeliveryViewController : UIViewController
    {
        private CLLocationManager _locationManager;

        public NewDeliveryViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _locationManager = new CLLocationManager();
            _locationManager.RequestWhenInUseAuthorization();
            SourceMapView.ShowsUserLocation = true;
            DestinationMapView.ShowsUserLocation = true;

            SourceMapView.DidUpdateUserLocation += SourceMapView_DidUpdateUserLocation;
            DestinationMapView.DidUpdateUserLocation += DestinationMapView_DidUpdateUserLocation;
            SaveBarButtonItem.Clicked += SaveBarButtonItem_Clicked;
        }

        private void DestinationMapView_DidUpdateUserLocation(object sender, MapKit.MKUserLocationEventArgs e)
        {
            if (DestinationMapView.UserLocation == null) return;

            var coordinates = DestinationMapView.UserLocation.Coordinate;
            var span = new MKCoordinateSpan(0.15, 0.15);
            DestinationMapView.Region = new MKCoordinateRegion(coordinates, span);

            DestinationMapView.RemoveAnnotations();
            DestinationMapView.AddAnnotation(new MKPointAnnotation
            {
                Coordinate = coordinates,
                Title = "Your location"
            });
        }

        private void SourceMapView_DidUpdateUserLocation(object sender, MapKit.MKUserLocationEventArgs e)
        {
            if (SourceMapView.UserLocation == null) return;

            var coordinates = SourceMapView.UserLocation.Coordinate;
            var span = new MKCoordinateSpan(0.15, 0.15);
            SourceMapView.Region = new MKCoordinateRegion(coordinates, span);

            SourceMapView.RemoveAnnotations();
            SourceMapView.AddAnnotation(new MKPointAnnotation
            {
                Coordinate = coordinates,
                Title = "Your location"
            });
        }

        private async void SaveBarButtonItem_Clicked(object sender, EventArgs e)
        {
            var delivery = new Delivery
            {
                Name = PackageNameTextField.Text,
                Status = 0
            };

            await Delivery.InsertDelivery(delivery);
        }
    }
}