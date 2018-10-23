using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Geolocator;

namespace AppLegal.Droid
{
    [Activity(Label = "AppLegal", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] PermissionsLocation =
          {
              Android.Manifest.Permission.AccessCoarseLocation,
              Android.Manifest.Permission.AccessFineLocation,

            };

        const int RequestLocationId = 0;


        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            
            var locator = Plugin.Geolocator.CrossGeolocator.Current;
            //var currentPosition = new Position();
            //var currentPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(10000));
            

            base.OnCreate(savedInstanceState);
            
            //RequestPermissions(PermissionsLocation, RequestLocationId);

            //SetContentView(Resource.Layout.activity_maps);
            global::Xamarin.Forms.Forms.Init(ApplicationContext, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            RequestPermissions(PermissionsLocation, RequestLocationId);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}