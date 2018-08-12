using Foundation;
using System;
using System.Collections.Generic;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliveredTableViewController : UITableViewController
    {
        public string UserId;
        private List<Delivery> _deliveries;
        public DeliveredTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _deliveries = new List<Delivery>();
        }
    }
}