using Foundation;
using System;
using System.Collections.Generic;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliverTableViewController : UITableViewController
    {
        private List<Delivery> _deliveries;

        public DeliverTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _deliveries = new List<Delivery>();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "DeliverSegue")
            {
                var selectedRow = TableView.IndexPathForSelectedRow;
                var destinationViewController = segue.DestinationViewController as DeliverViewController;
                destinationViewController.Delivery = _deliveries[selectedRow.Row];

            }

            base.PrepareForSegue(segue, sender);
        }
    }
}