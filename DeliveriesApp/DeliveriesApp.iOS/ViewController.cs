using System;
using System.Linq;
using Foundation;
using UIKit;

namespace DeliveriesApp.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            SignInButton.TouchUpInside += SignInButton_TouchUpInside;
		}

        private async void SignInButton_TouchUpInside(object sender, EventArgs e)
        {
            var email = EmailTextField.Text;
            var password = PasswordTextField.Text;

            UIAlertController alert = null;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                alert = UIAlertController.Create("Incomplete", "Email or password cannot be empty", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK",UIAlertActionStyle.Default, null));
            }
            else
            {
                var user = (await AppDelegate.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

                if (user?.Password == password)
                {
                    alert = UIAlertController.Create("Succeed", "Welcome", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                }
                else
                {
                    alert = UIAlertController.Create("Failure", "Password incorrect", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                }
            }

            PresentViewController(alert, true, null);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
	    {
            base.PrepareForSegue(segue, sender);

	        if (segue.Identifier != "registerSegue")
	            return;

	        if (segue.DestinationViewController is RegisterViewController destinationViewController) 
	            destinationViewController.EmailAddress = EmailTextField.Text;
	    }

        public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

