using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;

namespace DeliveriesApp.Droid
{
	[Activity (Label = "Deliveries App", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
	    public static  MobileServiceClient MobileService = new MobileServiceClient("https://jukadeliveriesapp.azurewebsites.net");


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

        private async void SigninButton_Click(object sender, EventArgs e)
        {
            var email = _emailEditText.Text;
            var password = _passwordEditText.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Toast.MakeText(this, "Email or password cannot be empty", ToastLength.Long).Show();
                return;
            }

            var user = (await MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

            if (user == null) return;

            if(user.Password == password)
            {
                Toast.MakeText(this, "Login successful", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Incorrect password", ToastLength.Long).Show();
            }

        }
    }
}


