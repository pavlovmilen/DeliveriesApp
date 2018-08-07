using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
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
        }
    }
}