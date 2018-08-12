using Foundation;
using System;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class PickUpViewController : UIViewController
    {
        public Delivery Delivery;
        public string UserId { get; set; }

        public PickUpViewController (IntPtr handle) : base (handle)
        {
        }
    }
}