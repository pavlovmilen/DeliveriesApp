using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware.Fingerprints;
using Android.Widget;
using Android.OS;
using Android.Preferences;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Support.V7.App;
using DeliveriesApp.Models;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Name = "DeliveryPersonApp.Android.MainActivity", Exported = true)]
    [MetaData("android.app.shortcuts", Resource = "@xml/shortcuts")]
    public class MainActivity : AppCompatActivity
    {
        private EditText _emailEditText, _passwordEditText;
        private Button _loginButton, _registerButton;
        private FingerprintManagerCompat _fingerprintManager;
        private global::Android.Support.V4.OS.CancellationSignal _cancellationSignal;

        private ISharedPreferences _preferences;

        private string _userId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _fingerprintManager = FingerprintManagerCompat.From(this);
            _cancellationSignal = new global::Android.Support.V4.OS.CancellationSignal();
            _preferences = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);

            _emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            _loginButton = FindViewById<Button>(Resource.Id.loginButton);
            _registerButton = FindViewById<Button>(Resource.Id.registerButton);

            _loginButton.Click += LoginButton_Click;
            _registerButton.Click += RegisterButton_Click;

            if (string.IsNullOrEmpty(Intent?.Data?.LastPathSegment)) return;
            if (Intent.Data.LastPathSegment == "register")
            {
                StartActivity(typeof(RegisterActivity));
            }
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }

        private async void LoginButton_Click(object sender, System.EventArgs e)
        {
            if (CanUseFingerprint())
            { 
                LogUserIn();
            }
            else
            {
                _userId = await DeliveryPerson.Login(_emailEditText.Text, _passwordEditText.Text);

                if (!string.IsNullOrEmpty(_userId))
                {
                    var preferencesEditor = _preferences.Edit();
                    preferencesEditor.PutString("userId", _userId);
                    preferencesEditor.Apply();

                    var intent = new Intent(this, typeof(TabsActivity));
                    intent.PutExtra("userId", _userId);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Failure", ToastLength.Long).Show();
                }
            }
        }

        private void LogUserIn()
        {
            var authenticationCallback = new AuthenticationCallback(this, _userId);
            Toast.MakeText(this, "Place fingerprint on sensor", ToastLength.Long).Show();
            _fingerprintManager.Authenticate(null, 0, _cancellationSignal, authenticationCallback, null);
        }

        private bool CanUseFingerprint()
        {
            _userId = _preferences.GetString("userId", string.Empty);

            if (string.IsNullOrEmpty(_userId)) return false;
            if (!_fingerprintManager.IsHardwareDetected) return false;
            if (!_fingerprintManager.HasEnrolledFingerprints) return false;

            var permissionResult = ContextCompat.CheckSelfPermission(this, Manifest.Permission.UseFingerprint);

            return permissionResult == Permission.Granted;
        }

        //  private void GetLocationPermission()
        //{
        //    //Check to see if any permission in our group is available, if one, then all are
        //    const string permission = Manifest.Permission.AccessFineLocation;
        //    if (CheckSelfPermission(permission) == (int)Permission.Granted)
        //    {
        //        RequestLocationUpdates();
        //        return;
        //    }

        //    if (ShouldShowRequestPermissionRationale(permission))
        //    {
        //        //Explain to the user why we need to read the contacts
        //        Snackbar.Make(_layout, "Location access is required to show coffee shops nearby.", Snackbar.LengthIndefinite)
        //            .SetAction("OK", v => RequestPermissions(PermissionsLocation, RequestLocationId))
        //            .Show();
        //        return;
        //    }
        //    //Finally request permissions with the list of permissions and Id
        //    RequestPermissions(PermissionsLocation, RequestLocationId);
        //}

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        //{
        //    switch (requestCode)
        //    {
        //        case RequestLocationId:
        //        {
        //            if (grantResults[0] == Permission.Granted)
        //            {
        //                //Permission granted
        //                var snack = Snackbar.Make(_layout, "Location permission is available, getting lat/long.", Snackbar.LengthShort);
        //                snack.Show();

        //                RequestLocationUpdates();
        //            }
        //            else
        //            {
        //                //Permission Denied :(
        //                //Disabling location functionality
        //                var snack = Snackbar.Make(_layout, "Location permission is denied.", Snackbar.LengthShort);
        //                snack.Show();
        //            }
        //        }
        //            break;
        //    }
        //}

    }

    public class AuthenticationCallback : FingerprintManagerCompat.AuthenticationCallback
    {
        private Activity _activity;
        private string _userId;
        public AuthenticationCallback(Activity activity, string userId)
        {
            _activity = activity;
            _userId = userId;
        }

        public override void OnAuthenticationSucceeded(FingerprintManagerCompat.AuthenticationResult result)
        {
            base.OnAuthenticationSucceeded(result); 

            var intent = new Intent(_activity, typeof(TabsActivity));
            intent.PutExtra("userId", _userId);
            _activity.StartActivity(intent);
        }

        public override void OnAuthenticationFailed()
        {
            base.OnAuthenticationFailed();

            Toast.MakeText(_activity, "Failure", ToastLength.Long).Show();
        }
    }
}

