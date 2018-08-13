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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            DeliverBarButtonItem.Clicked += DeliverBarButtonItem_Clicked;
        }

        private async void DeliverBarButtonItem_Clicked(object sender, EventArgs e)
        {
            await Delivery.MarkAsDelivered(Delivery.Id);
        }
    }
}