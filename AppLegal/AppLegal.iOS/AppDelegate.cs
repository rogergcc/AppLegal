﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using Plugin.FirebasePushNotification;
using Plugin.DeviceInfo;

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
            global::Xamarin.Forms.Forms.Init();
            //ImageCircleRenderer.Init();
            //Xamarin.FormsGoogleMaps.Init("your_google_maps_ios_api_key");
            var refreshedToken = CrossFirebasePushNotification.Current.Token;

            string getUniqueIdIOS = UIDevice.CurrentDevice.IdentifierForVendor.ToString();

            var deviceId = CrossDeviceInfo.Current.Id;

            LoadApplication(new App(refreshedToken));
            Rg.Plugins.Popup.Popup.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}
