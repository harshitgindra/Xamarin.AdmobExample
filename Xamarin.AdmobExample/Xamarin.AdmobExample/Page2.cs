using Xamarin.AdmobExample.Controls;
using Xamarin.Forms;

namespace Xamarin.AdmobExample
{
    public class Page2: ContentPage
    {
        public Page2()
        {
            var label = new Label()
            {
                Text =  "Page 2",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 30
            };
            
            AdmobControl admobControl = new AdmobControl()
            {
                AdUnitId = AppConstants.BannerId
            };
            
            var stackLayout = new StackLayout();
            stackLayout.Children.Add(label);
            stackLayout.Children.Add(admobControl);

            this.Content = stackLayout;
        }
    }
}