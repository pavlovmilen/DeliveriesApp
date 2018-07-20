using System;
using Android.App;
using Android.OS;
using Android.Widget;

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
            if(!string.IsNullOrEmpty(_passwordEditText.Text) )
            {
                if (_passwordEditText.Text == _confirmPasswordEditText.Text)
                {
                    var user = new User
                    {
                        Email = _emailEditText.Text,
                        Password = _passwordEditText.Text
                    };

                    await MainActivity.MobileService.GetTable<User>().InsertAsync(user);

                    Toast.MakeText(this, "Success", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Passwords do not match", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Password cannot be empty", ToastLength.Long).Show();
            }
        }
    }
}