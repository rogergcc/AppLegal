using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;

using UIKit;
using Plugin.FirebasePushNotification;

using Xamarin.Forms.GoogleMaps.iOS;
using Xamarin.Forms.GoogleMaps.iOS.Factories;

namespace AppLegal.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init(); 

            global::Xamarin.Forms.Forms.Init();
            //ImageCircleRenderer.Init();
           
            var refreshedToken = CrossFirebasePushNotification.Current.Token;

            string getUniqueIdIOS = UIDevice.CurrentDevice.IdentifierForVendor.ToString();

            //var deviceId = CrossDeviceInfo.Current.Id;

            var platformConfig = new PlatformConfig
            {
                ImageFactory = new CachingImageFactory()
            };
            // Xamarin.FormsGoogleMaps.Init("AIzaSyAp85Un8_O431Ibn_ml8iBNGWgXCd2cixc");
            Xamarin.FormsGoogleMaps.Init("AIzaSyAp85Un8_O431Ibn_ml8iBNGWgXCd2cixc", platformConfig);
            LoadApplication(new App(refreshedToken));

            return base.FinishedLaunching(app, options);
        }
    }

}
