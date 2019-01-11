using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Geolocator;
using Plugin.DeviceInfo;
using Xamarin.Forms;
using Plugin.FirebasePushNotification;
using Firebase.Iid;
using Xamarin.Forms.GoogleMaps.Android;

namespace AppLegal.Droid
{
    [Activity(Label = "AppLegal", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] PermissionsLocation =
          {
              Android.Manifest.Permission.AccessCoarseLocation,
              Android.Manifest.Permission.AccessFineLocation,
              Android.Manifest.Permission.ReadPhoneState
            };

        const int RequestLocationId = 0;


        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            
            //var locator = Plugin.Geolocator.CrossGeolocator.Current;
            //var currentPosition = new Position();
            //var currentPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(10000));
            

            base.OnCreate(savedInstanceState);

            //RequestPermissions(PermissionsLocation, RequestLocationId);

            //SetContentView(Resource.Layout.activity_maps);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(ApplicationContext, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            RequestPermissions(PermissionsLocation, RequestLocationId);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            var deviceId = CrossDeviceInfo.Current.Id;

            Android.Telephony.TelephonyManager mTelephonyMgr;
            mTelephonyMgr = (Android.Telephony.TelephonyManager)GetSystemService(TelephonyService);


            //IMEI number  
            //String m_deviceId = mTelephonyMgr.DeviceId;

            //String m_deviceId2 = GetIMEI();
            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };
            
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig); // initialize for Xamarin.Forms.GoogleMaps
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            LoadApplication(new App(refreshedToken));
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            
            
        }
        public string GetIMEI()
        {
            Android.Telephony.TelephonyManager mTelephonyMgr = (Android.Telephony.TelephonyManager)Forms.Context.GetSystemService(Android.Content.Context.TelephonyService);
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                // TODO: Some phones has more than 1 SIM card or may not have a SIM card inserted at all
                return mTelephonyMgr.GetMeid(0);
            else
            #pragma warning disable CS0618 // Type or member is obsolete
                            return mTelephonyMgr.DeviceId;
            #pragma warning restore CS0618 // Type or member is obsolete
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}