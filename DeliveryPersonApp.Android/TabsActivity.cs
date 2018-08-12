using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using DeliveryPersonApp.Android.Fragments;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "TabsActivity")]
    public class TabsActivity : FragmentActivity
    {
        public string UserId { get; set; }

        private TabLayout _tabLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Tabs);

            UserId = Intent.GetStringExtra("userId");

            _tabLayout = FindViewById<TabLayout>(Resource.Id.mainTabLayout);
            _tabLayout.TabSelected += TabLayout_TabSelected;
        }

        private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            switch (e.Tab.Position)
            {
                    case 0:
                        TabNavigation(new DeliveringFragment());
                        break;
                    case 1:
                        TabNavigation(new WaitingFragment());
                        break;
                    case 2:
                        TabNavigation(new DeliveredFragment());
                        break;
                    default:
                        TabNavigation(new DeliveringFragment());
                        break;

            }
        }

        private void TabNavigation(global::Android.Support.V4.App.Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, fragment);
            transaction.Commit();
        }
    }
}