using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace Surveys.Droid
{
    [Activity(Label = "Surveys.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new Surveys.Core.App());
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

