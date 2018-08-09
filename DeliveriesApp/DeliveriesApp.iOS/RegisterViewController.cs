using System;
using DeliveriesApp.Models;
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

            RegisterButton.TouchUpInside += RegisterButton_TouchUpInside;
        }

        private async void RegisterButton_TouchUpInside(object sender, EventArgs e)
        {
            var result = await Delivery.Register(EmailTextfield.Text, PasswordTextField.Text,
                ConfirmPasswordTextField.Text);
    
            var alert = UIAlertController.Create(result ? "Success" : "Failure", result ? "User inserted" : "Could not register", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true,null);
        }
    }
}