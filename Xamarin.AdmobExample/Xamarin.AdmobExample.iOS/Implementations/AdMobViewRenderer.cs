using System;
using System.ComponentModel;
using Foundation;
using Google.MobileAds;
using UIKit;
using Xamarin.AdmobExample.Controls;
using Xamarin.AdmobExample.iOS.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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
                var bannerView = CreateBannerView();
                if (bannerView != null)
                {
                    SetNativeControl(bannerView);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            //if (e.PropertyName == nameof(BannerView.AdUnitID))
            if (Control != null)
            {
                Control.AdUnitID = Element.AdUnitId;
            }
        }

        private BannerView CreateBannerView()
        {
            try
            {
                var bannerView = new BannerView(AdSizeCons.SmartBannerPortrait)
                {
                    //AdUnitID = AppConstants.BannerId,
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
            catch (Exception ex)
            {
                return null;
            }
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
