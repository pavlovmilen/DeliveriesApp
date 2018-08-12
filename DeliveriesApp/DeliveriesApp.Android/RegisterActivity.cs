using System;
using Android.App;
using Android.OS;
using Android.Widget;
using DeliveriesApp.Models;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private EditText _emailEditText, _passwordEditText, _confirmPasswordEditText;
        private Button _registerUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);

            _emailEditText = FindViewById<EditText>(Resource.Id.registerEmailEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.registerPasswordEditText);
            _confirmPasswordEditText = FindViewById<EditText>(Resource.Id.confirmPasswordEditText);

            _registerUser = FindViewById<Button>(Resource.Id.registerUserButton);

            _registerUser.Click += RegisterUser_Click;

            var email = Intent.GetStringExtra("email");
            _emailEditText.Text = email;
        }

        private async void RegisterUser_Click(object sender, EventArgs e)
        {
            var result = await User.Register(_emailEditText.Text, _passwordEditText.Text,
                _confirmPasswordEditText.Text);

            if(result)
            {
                Toast.MakeText(this, "Success", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Could not register", ToastLength.Long).Show();
            }
        }
    }
}