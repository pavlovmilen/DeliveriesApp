using Foundation;
using System;
using UIKit;

namespace DeliveriesApp.iOS
{
    public partial class RegisterViewController : UIViewController
    {
        public string EmailAddress { get; set; }

        public RegisterViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            EmailTextfield.Text = EmailAddress;
        }
    }
}