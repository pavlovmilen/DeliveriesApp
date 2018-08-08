using System;
using Android.App;
using Android.OS;
using Android.Widget;

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

        private void RegisterViewButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}