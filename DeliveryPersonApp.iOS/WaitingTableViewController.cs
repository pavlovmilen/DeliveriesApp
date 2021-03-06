﻿using Foundation;
using System;
using System.Collections.Generic;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class WaitingTableViewController : UITableViewController
    {
        private List<Delivery> _deliveries;
        public string UserId;
        public WaitingTableViewController (IntPtr handle) : base (handle)
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
            _deliveries = await Delivery.GetWaiting();
            TableView.ReloadData();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _deliveries.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("waitingCell");
            var delivery = _deliveries[indexPath.Row];

            cell.TextLabel.Text = delivery.Name;
            cell.DetailTextLabel.Text = $"{delivery.OriginLatitude}, {delivery.OriginLongitude}";

            return cell;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "PickupSegue")
            {
                var selectedRow = TableView.IndexPathForSelectedRow;
                if (segue.DestinationViewController is PickUpViewController destinationViewController)
                {
                    destinationViewController.Delivery = _deliveries[selectedRow.Row];
                    destinationViewController.UserId = UserId;
                }
            }

            base.PrepareForSegue(segue, sender);
        }
    }
}