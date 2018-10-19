using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AppLegal.Droid
{
    [Activity(Label = "AppLegal", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //setContentView(R.layout.activity_maps);
            
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.activity_maps);
            global::Xamarin.Forms.Forms.Init(ApplicationContext, savedInstanceState);
            LoadApplication(new App());
        }
    }
}