using System;
using System.Threading.Tasks;
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
            
            if(ViewControllers[0] is DeliveringTableViewController deliveringVC)
            {
                deliveringVC.UserId = UserId;
            }

            if(ViewControllers[1] is WaitingTableViewController waitingVC)
            {
                waitingVC.UserId = UserId;
            }

            if(ViewControllers[2] is WaitingTableViewController deliveredVC)
            {
                deliveredVC.UserId = UserId;
            }
        }
    }
}