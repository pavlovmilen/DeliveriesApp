using Foundation;
using System;
using System.Collections.Generic;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class WaitingTableViewController : UITableViewController
    {
        private List<Delivery> _deliveries;

        public WaitingTableViewController (IntPtr handle) : base (handle)
        {
        }
   
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _deliveries = new List<Delivery>();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "PickupSegue")
            {
                var selectedRow = TableView.IndexPathForSelectedRow;
                var destinationViewController = segue.DestinationViewController as PickUpViewController;
                destinationViewController.Delivery = _deliveries[selectedRow.Row];

            }

            base.PrepareForSegue(segue, sender);
        }
    }
}