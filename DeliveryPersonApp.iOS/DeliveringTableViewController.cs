using Foundation;
using System;
using System.Collections.Generic;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class DeliveringTableViewController : UITableViewController
    {
        private List<Delivery> _deliveries;
        public string UserId;

        public DeliveringTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _deliveries = new List<Delivery>();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _deliveries.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("deliveringCell");
            var delivery = _deliveries[indexPath.Row];

            cell.TextLabel.Text = delivery.Name;
            cell.DetailTextLabel.Text = $"{delivery.DestinationLatitude}, {delivery.DestinationLongitude}";

            return cell;
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            _deliveries = await Delivery.GetBeingDelivered(UserId);
            TableView.ReloadData();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "DeliverSegue")
            {
                var selectedRow = TableView.IndexPathForSelectedRow;
                if (segue.DestinationViewController is DeliverViewController destinationViewController)
                    destinationViewController.Delivery = _deliveries[selectedRow.Row];
            }

            base.PrepareForSegue(segue, sender);
        }
    }
}