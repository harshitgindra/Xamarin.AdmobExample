
using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;

namespace Xamarin.AdmobExample.Droid
{
    [Activity(Label = "Xamarin.AdmobExample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Forms.Forms.Init(this, savedInstanceState);
            MobileAds.Initialize(ApplicationContext, AppConstants.AppId);
            LoadApplication(new App());
        }
    }
}