using Foundation;
using System;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class LoginViewController : UIViewController
    {
        private bool _hasLoggedIn = false;
        private string _userId = string.Empty;

        public LoginViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            LoginButton.TouchUpInside += LoginButton_TouchUpInside;

        }

        private async void LoginButton_TouchUpInside(object sender, EventArgs e)
        {
            _userId = await DeliveryPerson.Login(UsernameTextField.Text, PasswordTextField.Text);

            if (!string.IsNullOrEmpty(_userId))
            {
                _hasLoggedIn = true;
                PerformSegue("LoginSegue", this);
            }
            else
            {
                
            }

        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "LoginSegue")
            {
                if (segue.DestinationViewController is MainTabBarViewController desctVC)
                {
                    desctVC.UserId = _userId;
                }
            }

            base.PrepareForSegue(segue, sender);
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if (segueIdentifier == "LoginSegue")
            {
                return _hasLoggedIn;
            }
            return base.ShouldPerformSegue(segueIdentifier, sender);
        }
    }
}