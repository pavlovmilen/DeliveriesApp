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
            var result = await AzureHelper.Login(EmailTextField.Text, PasswordTextField.Text);

            var alert = UIAlertController.Create(result? "Success":"Failure", result? "Wellcome": "Invaild email or password", UIAlertControllerStyle.Alert );

            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

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



