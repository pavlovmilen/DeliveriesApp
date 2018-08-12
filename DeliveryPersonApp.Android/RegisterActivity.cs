using System;
using Android.App;
using Android.OS;
using Android.Widget;
using DeliveriesApp.Models;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private EditText _emailRegisterEditText, _passwordRegisterEditText, _confirmPasswordRegisterEditText;
        private Button _registerViewButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Register);

            _emailRegisterEditText = FindViewById<EditText>(Resource.Id.emailRegisterEditText);
            _passwordRegisterEditText = FindViewById<EditText>(Resource.Id.passwordRegisterEditText);
            _confirmPasswordRegisterEditText = FindViewById<EditText>(Resource.Id.confirmRegisterPasswordEeditText);
            _registerViewButton = FindViewById<Button>(Resource.Id.registerViewButton);

            _registerViewButton.Click += RegisterViewButton_Click;
        }

        private async void RegisterViewButton_Click(object sender, EventArgs e)
        {
            var success = await DeliveryPerson.Register(_emailRegisterEditText.Text, _passwordRegisterEditText.Text, _confirmPasswordRegisterEditText.Text);

            if (success)
                Toast.MakeText(this, "Success", ToastLength.Long).Show();
            else
                Toast.MakeText(this, "Failure", ToastLength.Long).Show();
        }
    }
}