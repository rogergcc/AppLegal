using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AppLegal
{
    public partial class MainPage : ContentPage
    {
        //Map map;

        public MainPage()
        {

            InitializeComponent();
            /*map = new Map
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var locator = Plugin.Geolocator.CrossGeolocator.Current;
            //var currentPosition = new Position();
            //var currentPosition =await locator.GetPositionAsync(TimeSpan.FromSeconds(10000));

            //var lat = currentPosition.ToString();




            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(36.9628066, -122.0194722), Distance.FromMiles(3)));
            var position = new Position(36.9628066, -122.0194722);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "Santa Cruz",
                Address = "custom detail info"
            };
            map.Pins.Add(pin);



            var morePins = new Button { Text = "Add more pins" };
            morePins.Clicked += (sender, e) => {
                map.Pins.Add(new Pin
                {
                    Position = new Position(36.9641949, -122.0177232),
                    Label = "Boardwalk"
                });
                map.Pins.Add(new Pin
                {
                    Position = new Position(36.9571571, -122.0173544),
                    Label = "Wharf"
                });
                map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(36.9628066, -122.0194722), Distance.FromMiles(1.5)));

            };
            
            var reLocate = new Button { Text = "Re-center" };
            reLocate.Clicked += (sender, e) => {

                map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(36.9628066, -122.0194722), Distance.FromMiles(3)));
            };
            var buttons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    morePins, reLocate
                }
            };

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    map,
                    buttons
                }
            };*/
        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            var locator = Plugin.Geolocator.CrossGeolocator.Current;
            try
            {
                locator.DesiredAccuracy = 50;
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location2 = await Geolocation.GetLocationAsync(request);
                Location currentLocation = new Location();
                currentLocation = location2;
                
                //var currentPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(10000));
                
                //var afs = currentPosition.Latitude;
            }
            catch (Exception ex)
            {

                Console.Write("ERROR:  "+ ex.ToString());
                throw new NotImplementedException();
            }
            

            
        }
    }
}
