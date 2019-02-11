using System;
using Android.Gms.Ads;
using System.Threading.Tasks;
using Xamarin.AdmobExample.Droid.Implementations;
using Xamarin.Forms;

[assembly: Dependency(typeof(InterstitialAdsImplementation))]
namespace Xamarin.AdmobExample.Droid.Implementations
{
    public class InterstitialAdsImplementation : IAdmobInterstitialAds
    {
        public Task Display(string adId)
        {
             var displayTask = new TaskCompletionSource<bool>();
            InterstitialAd AdInterstitial = new InterstitialAd(context: Forms.Forms.Context)
            {
                AdUnitId = adId
            };
            {
                var adInterstitialListener = new AdInterstitialListener(AdInterstitial)
                {
                    AdClosed = () =>
                    {
                        if (displayTask != null)
                        {
                            displayTask.TrySetResult(AdInterstitial.IsLoaded);
                            displayTask = null;
                        }
                    },
                    AdFailed = () =>
                    {
                        if (displayTask != null)
                        {
                            displayTask.TrySetResult(AdInterstitial.IsLoaded);
                            displayTask = null;
                        }
                    }
                };

                AdRequest.Builder requestBuilder = new AdRequest.Builder();
                AdInterstitial.AdListener = adInterstitialListener;
                AdInterstitial.LoadAd(requestBuilder.Build());
            }

            return Task.WhenAll(displayTask.Task);
        }
    }

    public class AdInterstitialListener : AdListener
    {
       private readonly InterstitialAd _interstitialAd;

        public AdInterstitialListener(InterstitialAd interstitialAd)
        {
            _interstitialAd = interstitialAd;
        }

        public Action AdLoaded { get; set; }
        public Action AdClosed{ get; set; }
        public Action AdFailed{ get; set; }

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();

            if (_interstitialAd.IsLoaded)
            {
                _interstitialAd.Show();
            }
            AdLoaded?.Invoke();
        }

        public override void OnAdClosed()
        {
            base.OnAdClosed();
            AdClosed?.Invoke();
        }

        public override void OnAdFailedToLoad(int errorCode)
        {
            base.OnAdFailedToLoad(errorCode);
            AdFailed?.Invoke();
        }
    }
}
