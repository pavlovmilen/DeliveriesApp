// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    [Register ("DeliverViewController")]
    partial class DeliverViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem DeliverBarButtonItem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView DeliveryMapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem navigateBarButtonItem { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DeliverBarButtonItem != null) {
                DeliverBarButtonItem.Dispose ();
                DeliverBarButtonItem = null;
            }

            if (DeliveryMapView != null) {
                DeliveryMapView.Dispose ();
                DeliveryMapView = null;
            }

            if (navigateBarButtonItem != null) {
                navigateBarButtonItem.Dispose ();
                navigateBarButtonItem = null;
            }
        }
    }
}