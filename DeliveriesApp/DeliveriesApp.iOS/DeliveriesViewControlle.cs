﻿using DeliveriesApp.Models;
using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class DeliveriesViewControlle : UITableViewController
    {
        private List<Delivery> _deliveries;
        public DeliveriesViewControlle (IntPtr handle) : base (handle)
        {
            _deliveries = new List<Delivery>();
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            _deliveries = await Delivery.GetActiveDeliveries();
            TableView.ReloadData();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _deliveries.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("delivery") as DeliveryItemCell;
            var delivery = _deliveries[indexPath.Row];

            cell.NameLabel.Text = delivery.Name;
            cell.StatusLabel.Text = delivery.GetStatusForDelivery(delivery.Status);
            cell.CoordinatesLabel.Text = $"{delivery.DestinationLatitude} - {delivery.DestinationLongitude}";

            return cell;
        }
    }
}