using Foundation;
using System;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliverViewController : UIViewController
    {
        public Delivery Delivery;

        public DeliverViewController (IntPtr handle) : base (handle)
        {
        }
    }
}