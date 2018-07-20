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

            RegisterButton.TouchUpInside += RegisterButton_TouchUpInside;
        }

        private async void RegisterButton_TouchUpInside(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordTextField.Text)) return;

            if (PasswordTextField.Text != ConfirmPasswordTextField.Text) return;

            var user = new User
            {
                Email = EmailTextfield.Text,
                Password = PasswordTextField.Text
            };

            await AppDelegate.MobileService.GetTable<User>().InsertAsync(user);

            var alert = UIAlertController.Create("Success", "User inserted", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true,null);
        }
    }
}