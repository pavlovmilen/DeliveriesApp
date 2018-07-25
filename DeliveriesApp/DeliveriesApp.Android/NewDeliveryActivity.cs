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
using DeliveriesApp.Models;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "NewDelivery")]
    public class NewDeliveryActivity : Activity
    {
        private Button _saveButton;
        private EditText _packageNamEditText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewDeivery2);

            _saveButton = FindViewById<Button>(Resource.Id.saveButton);
            _packageNamEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);

            _saveButton.Click += OnSave_Clicked;
        }

        private async void OnSave_Clicked(object sender, EventArgs e)
        {
            var delivery = new Delivery
            {
                Name = _packageNamEditText.Text,
                Status = 0
            };

            await Delivery.InsertDelivery(delivery);
        }
    }
}