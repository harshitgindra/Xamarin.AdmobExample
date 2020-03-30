using System;
using System.Diagnostics;
using Xamarin.AdmobExample.Controls;
using Xamarin.Forms;

namespace Xamarin.AdmobExample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            AdmobControl admobControl = new AdmobControl()
            {
                AdUnitId = AppConstants.BannerId
            };
            Label adLabel = new Label() { Text = "Ads will be displayed here!" };

            Button showInterstitialAdsButton = new Button();
            showInterstitialAdsButton.Clicked += ShowInterstitialAdsButton_Clicked;
            showInterstitialAdsButton.Text = "Show Interstitial Ads";
            
            Button navigateToPage2Button = new Button();
            navigateToPage2Button.Clicked += NavigateToPage2_Clicked;
            navigateToPage2Button.Text = "Go To Page 2";

            Content = new StackLayout()
            {
                Children = { adLabel, admobControl, showInterstitialAdsButton , navigateToPage2Button}
            };

            this.Title = "Admob Page";
        }

        async void ShowInterstitialAdsButton_Clicked(object sender, EventArgs e)
        {
            if (AppConstants.ShowAds)
            {
                await DependencyService.Get<IAdmobInterstitialAds>().Display(AppConstants.InterstitialAdId);
            }
            Debug.WriteLine("Continue button click implementation");
        }
        
        async void NavigateToPage2_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new Page2());
        }
    }
}
