using System;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class MainTabBarViewController : UITabBarController
    {
        public string UserId { get; set; }

        public MainTabBarViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            NavigationItem.SetHidesBackButton(true, false);

            if(TabBarController.ViewControllers[0] is DeliveringTableViewController deliveringVC)
            {
                deliveringVC.UserId = UserId;
            }

            if(TabBarController.ViewControllers[1] is WaitingTableViewController waitingVC)
            {
                waitingVC.UserId = UserId;
            }

            if(TabBarController.ViewControllers[2] is WaitingTableViewController deliveredVC)
            {
                deliveredVC.UserId = UserId;
            }
        }
    }
}