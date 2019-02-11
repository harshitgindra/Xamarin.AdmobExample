using System;
using System.Threading.Tasks;

namespace Xamarin.AdmobExample
{
    public interface IAdmobInterstitialAds
    {
         Task Display(string adId);
    }
}
