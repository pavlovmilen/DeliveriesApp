using Foundation;
using System;
using System.Threading.Tasks;
using DeliveriesApp.Models;
using LocalAuthentication;
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
            var success = CheckLogin();

            if (success)
            {
                BiometricsAuth();
            }
            else
            {
                await TraditionalLogin();
            }
        }

        private async Task TraditionalLogin()
        {
            _userId = await DeliveryPerson.Login(UsernameTextField.Text, PasswordTextField.Text);

            if (!string.IsNullOrEmpty(_userId))
            {
                NSUserDefaults.StandardUserDefaults.SetString(_userId, "userId");
                NSUserDefaults.StandardUserDefaults.Synchronize();

                _hasLoggedIn = true;
                PerformSegue("LoginSegue", this);
            }
        }

        private async Task BiometricsAuth()
        {
            var context = new LAContext();
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out _))
            {
                InvokeOnMainThread( async () =>
                    {
                        var authenticated = await context.EvaluatePolicyAsync(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Login");

                        if (authenticated.Item1)
                        {
                            _hasLoggedIn = true;
                            PerformSegue("LoginSegue", this);
                        }
                        else
                        {
                            await TraditionalLogin();
                        }
                    });
            }
            else
            {
                await  TraditionalLogin();
            }
        }

        private bool CheckLogin()
        {
            var hasId = false;

            _userId = NSUserDefaults.StandardUserDefaults.StringForKey("userId");

            if (!string.IsNullOrEmpty(_userId))
                hasId = true;

            return hasId;
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