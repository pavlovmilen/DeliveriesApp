using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText _emailEditText, _passwordEditText;
        private Button _loginButton, _registerButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            _passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            _loginButton = FindViewById<Button>(Resource.Id.loginButton);
            _registerButton = FindViewById<Button>(Resource.Id.registerButton);

            _loginButton.Click += LoginButton_Click;
            _registerButton.Click += RegisterButton_Click;
        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(TabsActivity));
        }
    }
}

