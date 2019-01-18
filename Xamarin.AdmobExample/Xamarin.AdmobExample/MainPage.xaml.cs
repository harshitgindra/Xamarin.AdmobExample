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
            Label adLabel = new Label() { Text = "Ads will be displayed here!"};

            Content = new StackLayout()
            {
                Children = { adLabel, admobControl }
            };

            this.Title = "Admob Page";
        }
    }
}
