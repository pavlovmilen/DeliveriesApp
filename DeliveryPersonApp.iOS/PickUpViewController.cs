﻿using Foundation;
using System;
using CoreLocation;
using DeliveriesApp.Models;
using MapKit;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class PickUpViewController : UIViewController
    {
        public Delivery Delivery;
        public string UserId { get; set; }
        private CLLocationManager _locationManager;
        public PickUpViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            PuckUpBarButtonItem.Clicked += PuckUpBarButtonItem_Clicked;
            navigateBarButtonItem.Clicked += NavigateBarButtonItem_Clicked;  
            PrepareMap();
        }

        private void NavigateBarButtonItem_Clicked(object sender, EventArgs e)
        {
            var coord = new CLLocationCoordinate2D(Delivery.OriginLatitude, Delivery.OriginLongitude);
            var mapItem = new MKMapItem(new MKPlacemark(coord)) {Name = "Pick up here"};
            mapItem.OpenInMaps();
        }

        private void PrepareMap()
        {
            _locationManager = new CLLocationManager();
            _locationManager.RequestWhenInUseAuthorization();
            PickUpMapView.ShowsUserLocation = true;
            var span = new MKCoordinateSpan(0.15, 0.15);
            var coord = new CLLocationCoordinate2D(Delivery.OriginLatitude, Delivery.OriginLongitude);
            PickUpMapView.Region = new MKCoordinateRegion(coord, span);
            PickUpMapView.AddAnnotation(new MKPointAnnotation(){Coordinate =  coord, Title = "Pick up here"});

        }

        private async void PuckUpBarButtonItem_Clicked(object sender, EventArgs e)
        {
            var haptic = new UINotificationFeedbackGenerator();
            haptic.Prepare();

            var pickedUp = await Delivery.MarkAsPickedUp(Delivery, UserId);

            UIAlertController alert = null;
            if (pickedUp)
            {
                haptic.NotificationOccurred(UINotificationFeedbackType.Success);
                alert = UIAlertController.Create("Success", "Delivery set as picked up", UIAlertControllerStyle.Alert);
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