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

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            _deliveries = await Delivery.GetDelivered(UserId);
            TableView.ReloadData();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _deliveries.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("deliveredCell");
            var delivery = _deliveries[indexPath.Row];

            cell.TextLabel.Text = delivery.Name;
            cell.DetailTextLabel.Text = $"{delivery.DestinationLatitude}, {delivery.DestinationLongitude}";

            return cell;
        }
    }
}