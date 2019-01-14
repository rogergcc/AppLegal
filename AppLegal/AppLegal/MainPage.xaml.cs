using AppLegal.Models;
using AppLegal.Views;
using Plugin.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppLegal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        Map map;
        Location currentLocation = new Location();
        private HttpClient _Client = new HttpClient();
        public ObservableCollection<Zona> Zonas { get; set; }

        string usuarioId = App.Current.Properties["usuarioId"].ToString();
        string usuarioNombre = App.Current.Properties["usuarioNombre"].ToString();
        string rol = App.Current.Properties["rol"].ToString();
        string empleadoId = App.Current.Properties["empleadoId"].ToString();
        public MainPage()
        {


            InitializeComponent(); // layout por defecto
            getZonas();

            //ToolbarItem toolbarItem = new ToolbarItem
            //{
            //    Icon = "logo.png"
            //};

            //ToolbarItems.Add(toolbarItem);
            var margin = 20;

            //var deviceId = CrossDeviceInfo.Current.Id;

            //falta averiguar que antes q ejecute el mapa verifique permisos Location

            #region Layout por codigo
            #region codigo
            map = new Map
            {
                IsShowingUser = true,
                HeightRequest = App.ScreenHeight,
                WidthRequest = App.ScreenWidth,
                Margin = margin - 5,

                VerticalOptions = LayoutOptions.FillAndExpand

            };

            //var locator = Plugin.Geolocator.CrossGeolocator.Current;

            //var loc = currentLocation;

            posicionAsync();

            //map.MoveToRegion(MapSpan.FromCenterAndRadius(
            //    new Position(36.9628066, -122.0194722), Distance.FromMiles(3)));
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
            morePins.Clicked += (sender, e) =>
            {
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
            var txtUsuario = new Label { Text = "Usuario" };
            var txtImei = new Label { Text = "Imei" };
            var imageUser = new Image
            {

                Source = "usuario.png"
            };
            var imageImei = new Image
            {

                Source = "contrasena.png"
            };

            var reLocate = new Button { Text = "Posicion Actual" };
            reLocate.Clicked += (sender, e) =>
            {
                //var request = new GeolocationRequest(GeolocationAccuracy.High);
                //var location2 = await Geolocation.GetLocationAsync(request);
                //map.MoveToRegion(MapSpan.FromCenterAndRadius(
                //    new Position(location2.Latitude, location2.Longitude),
                //    Distance.FromMiles(.2)));
            };

            var stackUsuario = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { imageUser, txtUsuario }
            };

            var stackImei = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { imageImei, txtImei }
            };
            CornerRadius cornerRadius = new CornerRadius();
            cornerRadius = 10;


            var stackLayout = new StackLayout
            {

                Orientation = StackOrientation.Vertical,
                Padding = 20,
                Margin = margin - 5,
                BackgroundColor = Color.FromHex("FFF"),

                Children = { stackUsuario, stackImei }
            };
            var buttons = new StackLayout
            {
                Margin = margin - 5,
                Orientation = StackOrientation.Horizontal,
                Children = {
                    morePins, reLocate
                }
            };

            Content = new StackLayout
            {
                BackgroundColor = Color.FromHex("c1c1c1"),
                Spacing = 0,
                Children = {

                    stackLayout,
                    //buttons,
                    map
                }
            };
            #endregion codigo

            #endregion fin Layout por codigo
        }
        public async void posicionAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location2 = await Geolocation.GetLocationAsync(request);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(location2.Latitude, location2.Longitude),
                Distance.FromMiles(.2)));
        }
        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            var locator = Plugin.Geolocator.CrossGeolocator.Current;
            try
            {
                locator.DesiredAccuracy = 50;
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location2 = await Geolocation.GetLocationAsync(request);

                currentLocation = location2;

                //var currentPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(10000));

                //var afs = currentPosition.Latitude;
            }
            catch (Exception ex)
            {

                Console.Write("ERROR:  " + ex.ToString());
                throw new NotImplementedException();
            }



        }

        public async void getZonas()
        {
            String IP_LEGAL = App.Current.Properties["IpPublicado"].ToString();
            Zonas zonas = new Zonas();
            String url = IP_LEGAL + "/legal/ZonaTrabajo/ZonaTrabajoListarJsonExterno?id=" + usuarioId;

            var content = await _Client.GetStringAsync(url);
            var service = new RestClient<Zonas>();
            //Zonas = await service.GetRestServicieDataAsync(url);

            zonas = await service.GetRestServicieDataAsync(url);

            Zonas = new ObservableCollection<Zona>(zonas.zonas);

        }
        protected override async void OnAppearing()
        {


        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Login(App.Current);
        }
    }
}
