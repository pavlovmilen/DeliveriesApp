using Foundation;
using System;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class NewDeliveryViewController : UIViewController
    {
        public NewDeliveryViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SaveBarButtonItem.Clicked += SaveBarButtonItem_Clicked;
        }

        private async void SaveBarButtonItem_Clicked(object sender, EventArgs e)
        {
            var delivery = new Delivery
            {
                Name = PackageNameTextField.Text,
                Status = 0
            };

            await Delivery.InsertDelivery(delivery);
        }
    }
}