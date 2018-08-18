using Foundation;
using System;
using CoreLocation;
using DeliveriesApp.Models;
using UIKit;
using MapKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliverViewController : UIViewController
    {
        public Delivery Delivery;
        private CLLocationManager _locationManager;

        public DeliverViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            DeliverBarButtonItem.Clicked += DeliverBarButtonItem_Clicked;
            navigateBarButtonItem.Clicked += NavigateBarButtonItem_Clicked; 

            PrepareMap();
        }

        private void NavigateBarButtonItem_Clicked(object sender, EventArgs e)
        {
            var coord = new CLLocationCoordinate2D(Delivery.DestinationLatitude, Delivery.DestinationLongitude);
            var mapItem = new MKMapItem(new MKPlacemark(coord)) {Name = "Deliver here"};
            mapItem.OpenInMaps();
        }

        private void PrepareMap()
        {
            _locationManager = new CLLocationManager();
            _locationManager.RequestWhenInUseAuthorization();
            DeliveryMapView.ShowsUserLocation = true;
            var span = new MKCoordinateSpan(0.15, 0.15);
            var coord = new CLLocationCoordinate2D(Delivery.DestinationLatitude, Delivery.DestinationLongitude);
            DeliveryMapView.Region = new MKCoordinateRegion(coord, span);
            DeliveryMapView.AddAnnotation(new MKPointAnnotation(){Coordinate =  coord, Title = "Deliver here"});

        }

        private async void DeliverBarButtonItem_Clicked(object sender, EventArgs e)
        {
            var haptic = new UINotificationFeedbackGenerator();
            haptic.Prepare();

            var delivered = await Delivery.MarkAsDelivered(Delivery.Id);

            UIAlertController alert = null;
            if (delivered)
            {
                haptic.NotificationOccurred(UINotificationFeedbackType.Success);
                alert = UIAlertController.Create("Success", "Delivery has been delivered", UIAlertControllerStyle.Alert);
            }
            else
            {
                haptic.NotificationOccurred(UINotificationFeedbackType.Error);
                alert = UIAlertController.Create("Failure", "Please try agail", UIAlertControllerStyle.Alert);
            }

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

            PresentViewController(alert, true, null);
        }
    }
}