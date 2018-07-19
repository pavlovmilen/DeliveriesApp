using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DeliveriesApp.Droid
{
	[Activity (Label = "Deliveries App", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        private EditText _emailEditText, _passwordEditText;
        private Button _signinButton, _registerButton;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            _emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            _signinButton = FindViewById<Button>(Resource.Id.signinButton);
            _registerButton = FindViewById<Button>(Resource.Id.registerButton);

            _signinButton.Click += SigninButton_Click;
            _registerButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterActivity));
            intent.PutExtra("email", _emailEditText.Text);
            StartActivity(intent);
        }

        private void SigninButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}


