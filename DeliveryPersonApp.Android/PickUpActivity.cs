﻿using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "PickUpActivity")]
    public class PickUpActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.PickUp);
        }
    }
}