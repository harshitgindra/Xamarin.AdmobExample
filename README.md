# Xamarin.AdmobExample
This project showcases how to use Google's Admob for monetizing your Xamarin Forms mobile app for Android and iOS platforms. You can also check out the [**video tutorial**](https://www.youtube.com/watch?v=Q-lkUhWji3A). I'm sure there are plenty of ways of implementing Admob and many of the ways will be better than mine. If you know any better way or have any suggestions for me, please share with me. I'd love to hear it.

# Getting Started
Before you being, you need to have an account on [**Admob**](https://apps.admob.com/). If you don't have an account, please create an account and then proceed with this article. To display admob ads on the application, we're using nuget packages [Xamarin.GooglePlayServices.Ads.Lite](https://www.nuget.org/packages/Xamarin.GooglePlayServices.Ads.Lite/) for Android and [Xamarin.Google.iOS.MobileAds](https://www.nuget.org/packages/Xamarin.Google.iOS.MobileAds/) for iOS. 

## Portable Library
We start with creating our custom control AdmobControl which extends [**View**](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.view) class.
```
 public class AdmobControl : View
    {
        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
                       nameof(AdUnitId),
                       typeof(string),
                       typeof(AdmobControl),
                       string.Empty);

        public string AdUnitId
        {
            get => (string)GetValue(AdUnitIdProperty);
            set => SetValue(AdUnitIdProperty, value);
        }
    }
``` 
Then we use the **AdmobControl** in our view. Here's a simple example from this project
``` 
    public MainPage()
        {
            InitializeComponent();
            AdmobControl admobControl = new AdmobControl()
            {
                AdUnitId = AppConstants.BannerId
            };
            Label adLabel = new Label() { Text = "Ads will be displayed here!"};

            Content = new StackLayout()
            {
                Children = { adLabel, admobControl }
            };

            this.Title = "Admob Page";
        }
```        
## Android Implementation
        
First Step is to configure our application with Admob App Id. This needs to be done in **MainActivity.cs**
        
```        
          using Android.Gms.Ads;
         MobileAds.Initialize(ApplicationContext, AppConstants.AppId);
```         
Next Step is to allow permissions for the app in [**AndroidManifest.xml**](https://docs.microsoft.com/en-us/xamarin/android/platform/android-manifest)
```        
          <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
          <uses-permission android:name="android.permission.INTERNET" />
``` 
  We also need to define the **AdActivity** in the manifest
``` 
  <activity android:name="com.google.android.gms.ads.AdActivity" android:configChanges="keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize" android:theme="@android:style/Theme.Translucent" />
```
Now, we need to create the platform specific renderer for **AdmobControl**. This will be different for all platforms
  
``` 
 [assembly: ExportRenderer(typeof(AdmobControl), typeof(AdMobRenderer))]
namespace Xamarin.AdmobExample.Droid.Implementations
{
    public class AdMobRenderer : ViewRenderer<AdmobControl, AdView>
    {
        public AdMobRenderer(Context context) : base(context)
        {
        }

        private int GetSmartBannerDpHeight()
        {
            var dpHeight = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;

            if (dpHeight <= 400)
            {
                return 40;
            }
            if (dpHeight <= 720)
            {
                return 62;
            }
            return 102;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdmobControl> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var adView = new AdView(Context)
                {
                    AdSize = AdSize.SmartBanner,
                    AdUnitId = Element.AdUnitId
                };

                var requestbuilder = new AdRequest.Builder();

                adView.LoadAd(requestbuilder.Build());
                e.NewElement.HeightRequest = GetSmartBannerDpHeight();

                SetNativeControl(adView);
            }
        }
    }
}
```
We're all set with Android Implementation. We can now start testing.

## iOS Implementation

Similar to Android, we need to configure our App Id in the app. In iOS, we configure it in **AppDelegate.cs**
```
Google.MobileAds.MobileAds.Configure('{Your App Id}');
```
Now, we'll write the iOS renderer for **AdmobControl**
```
[assembly: ExportRenderer(typeof(AdmobControl), typeof(AdMobViewRenderer))]
namespace Xamarin.AdmobExample.iOS.Implementations
{
    [Protocol]
    public class AdMobViewRenderer : ViewRenderer<AdmobControl, BannerView>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<AdmobControl> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(CreateBannerView());
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Control.AdUnitID = Element.AdUnitId;
        }

        private BannerView CreateBannerView()
        {
            var bannerView = new BannerView(AdSizeCons.SmartBannerPortrait)
            {
                AdUnitID = Element.AdUnitId,
                RootViewController = GetVisibleViewController()
            };

            bannerView.LoadRequest(GetRequest());

            Request GetRequest()
            {
                var request = Request.GetDefaultRequest();
                return request;
            }

            return bannerView;
        }

        private UIViewController GetVisibleViewController()
        {
            var windows = UIApplication.SharedApplication.Windows;
            foreach (var window in windows)
            {
                if (window.RootViewController != null)
                {
                    return window.RootViewController;
                }
            }
            return null;
        }
    }
}
```
We're now ready to run the application and start earning

# Authors
- [Harshit Gindra](https://github.com/harshitgindra)

# License
This project is open source. 

# Acknowledgement

I've got some good references from these sources

 - [Montemagno](https://montemagno.com/xamarinforms-google-admob-ads-in-android/)
 - [Marius Bughiu](https://startdebugging.net/how-to-add-admob-to-your-xamarin-forms-app/)
