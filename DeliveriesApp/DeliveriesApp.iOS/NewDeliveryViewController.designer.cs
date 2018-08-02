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

namespace DeliveriesApp.iOS
{
    [Register ("NewDeliveryViewController")]
    partial class NewDeliveryViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView DestinationMapView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PackageNameTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem SaveBarButtonItem { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView SourceMapView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DestinationMapView != null) {
                DestinationMapView.Dispose ();
                DestinationMapView = null;
            }

            if (PackageNameTextField != null) {
                PackageNameTextField.Dispose ();
                PackageNameTextField = null;
            }

            if (SaveBarButtonItem != null) {
                SaveBarButtonItem.Dispose ();
                SaveBarButtonItem = null;
            }

            if (SourceMapView != null) {
                SourceMapView.Dispose ();
                SourceMapView = null;
            }
        }
    }
}