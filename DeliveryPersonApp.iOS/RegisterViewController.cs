using Foundation;
using System;
using DeliveriesApp.Models;
using UIKit;

namespace DeliveryPersonApp.iOS
{
    public partial class RegisterViewController : UIViewController
    {
        public RegisterViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            RegisterSaveButton.TouchUpInside += RegisterSaveButton_TouchUpInside;
        }

        private async void RegisterSaveButton_TouchUpInside(object sender, EventArgs e)
        {
            var success = await DeliveryPerson.Register(UsenameTextField.Text, PasswordTextField.Text, ConfirmPasswordTextField.Text);

            var alert = UIAlertController.Create(success? "Success": "Failure", 
                success ? "New user has been registered" : "Something went wrong when registering", UIAlertControllerStyle.Alert);

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true, null);

        }
    }
}