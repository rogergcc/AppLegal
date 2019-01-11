using AppLegal.Models;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views.WebToken
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SesionWeb : ContentPage
    {
        //Map map;
        private HttpClient _Client = new HttpClient();
        public ObservableCollection<Zona> Zonas { get; set; }
        public Location userCurrentlocation { get; set; }
        //CustomMap customMap { get; set; }
        Xamarin.Forms.GoogleMaps.Map googleMaps { get; set; }
        public int codigoUsuario { get; set; }

        public bool accesoHabilitado { get; set; } = false;

        Label txtUsuario { get; set; }

        string ontokenRefresh { get; set; }
        Circle circle = null;
        public SesionWeb()
        {
            InitializeComponent();
            var margin = 20;

            //var deviceId = CrossDeviceInfo.Current.Id;

            //falta averiguar que antes q ejecute el mapa verifique permisos Location

            #region Layout por codigo
            #region codigo
            
            googleMaps = new Xamarin.Forms.GoogleMaps.Map
            {
                IsShowingUser = true,
                MyLocationEnabled=true,
                HeightRequest = App.ScreenHeight,
                WidthRequest = App.ScreenWidth,
                Margin = margin - 5,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            googleMaps.UiSettings.MyLocationButtonEnabled = true;
            //var locator = Plugin.Geolocator.CrossGeolocator.Current;

            //var loc = currentLocation;

            posicionAsync();
            //getZonas();
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

            //customMap.Pins.Add(pin);
            //googleMaps.Pins.Add(pin);

            var obtenerZonas = new Button { Text = "Obtener Zonas" };
            obtenerZonas.Clicked += ObtenerZonas_ClickedAsync;

            txtUsuario = new Label { Text = "Usuario" };
            var txtImei = new Label { Text = "Imei" };
            var imageUser = new Image
            {

                Source = "usuario.png"
            };
            var imageImei = new Image
            {
                
                Source = "contrasena.png"
            };
            
            var botonCodigoCel = new ImageButton
            {
                
                Source = "contrasena.png"
            };
            botonCodigoCel.Clicked += BotonCodigoCel_Clicked;
            var reLocate = new Button { Text = "Posicion Actual" };
            reLocate.Clicked += (sender, e) =>
            {
                
            };
            var stackUsuario = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { imageUser, txtUsuario }
            };
            var stackImei = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { botonCodigoCel, txtImei }
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
                //Margin = margin - 5,
                Orientation = StackOrientation.Horizontal,
                Children = {
                    obtenerZonas
                }
            };
            Content = new StackLayout
            {
                BackgroundColor = Color.FromHex("c1c1c1"),
                Spacing = 0,
                Children = {

                    stackLayout,
                    buttons,
                    //customMap,
                    googleMaps
                }
            };
            #endregion codigo

            #endregion fin Layout por codigo
        }

        private void BotonCodigoCel_Clicked(object sender, EventArgs e)
        {
            var tokenEnviadodesdeActividad = "";
            
            tokenEnviadodesdeActividad = App.Current.Properties["TokenPush"].ToString();
            ontokenRefresh = CrossFirebasePushNotification.Current.Token;
            
            System.Diagnostics.Debug.WriteLine("ontokenRefresh : " + ontokenRefresh);
            System.Diagnostics.Debug.WriteLine("tokenEnviadodesdeActividad : " + tokenEnviadodesdeActividad);

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("TokenRefresh :" + p.Token);
                ontokenRefresh = p.Token;
                System.Diagnostics.Debug.WriteLine("toklen: " + p.Token);
                Console.Out.WriteLine("TOKEN CONSOLE : + p." + p.Token);
            };

            var notificationReceived = "";

            //var refreshedToken = FirebaseInstanceId.Instance.Token;

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                //EJECUTAR ESTE METODO [OnNotificationReceived] POR DEFECTO Y NO CUANDO HAGA CLICK EN EL EVENTO
                Console.Out.WriteLine("TOKEN CONSOLE : + p." + p.Data);
                notificationReceived = p.Data.ToString();
                object objetoRecivido = p.Data;
                var data = new
                {
                    codigo = 0,
                    nombreUsuario = ""
                };
                //https://github.com/CrossGeeks/FirebasePushNotificationPlugin/blob/master/docs/FirebaseNotifications.md
                
                var json = JsonConvert.SerializeObject(p.Data, Newtonsoft.Json.Formatting.Indented);
                var myobject = JsonConvert.DeserializeObject<AOCAdvancedSettings>(json);
                
                System.Diagnostics.Debug.WriteLine("Received");
                txtUsuario.Text = myobject.nombreUsuario;
            };
        }

        private async void ObtenerZonas_ClickedAsync(object sender, EventArgs e)
        {
            googleMaps.Circles.Clear();
            int usuarioId = 5;
            String IP_LEGAL = "http://192.168.1.40";
            Zonas zonas = new Zonas();
            String url = IP_LEGAL + "/legal/ZonaTrabajo/ZonaTrabajoListarJsonExterno?id=" + usuarioId;
            var content = await _Client.GetStringAsync(url);
            var service = new RestClient<Zonas>();
           
            zonas = await service.GetRestServicieDataAsync(url);
            //Zonas = new ObservableCollection<Zona>(zonas.zonas);

            bool habilitarAcceso = false;
            int userObtenerID = 0;
            int codigoZonaTrabajo = 0;
            
            double distance = 0.0d;
            const double metersInKm = 1000.0d;
            for (int i = 0; i < zonas.zonas.Length; i++)
            {
                var position = new Position(zonas.zonas[i].Latitud, zonas.zonas[i].Longitud);
                googleMaps.Pins.Add(new Pin
                {
                    Position = position,
                    Label = zonas.zonas[i].Direccion
                });
                
                //https://github.com/amay077/Xamarin.Forms.GoogleMaps/blob/master/XFGoogleMapSample/XFGoogleMapSample/ShapesPage.xaml.cs
                circle = new Circle();
                circle.IsClickable = true;
                circle.Center = position;
                circle.Radius = Distance.FromMeters(zonas.zonas[i].Radio);

                circle.StrokeColor = Color.SaddleBrown;
                circle.StrokeWidth = 3f;
                circle.FillColor = Color.FromRgba(0, 0, 255, 32);
                circle.Tag = zonas.zonas[i].Direccion; // Can set any object
                googleMaps.Circles.Add(circle);

                distance= Location.CalculateDistance(userCurrentlocation,position.Latitude,
                    position.Longitude,DistanceUnits.Kilometers);//this result give in KM

                distance = distance * metersInKm;

                userObtenerID = zonas.zonas[i].UsuarioID;

                if (distance < zonas.zonas[i].Radio && codigoUsuario.Equals(userObtenerID))
                {
                    habilitarAcceso = true;
                    codigoZonaTrabajo = zonas.zonas[i].ZonaTrabajoId;
                }
                else
                {
                    habilitarAcceso = (habilitarAcceso) ? habilitarAcceso : false;
                }

            }
            String estas = "0";
            bool existeUsuario = false;
            for (int i = 0; i < zonas.zonas.Length; i++)
            {
                if (zonas.zonas[i].UsuarioID.Equals(codigoUsuario))
                {
                    existeUsuario = true;
                    break;
                }
            }
            if (existeUsuario == false)
                estas = " sin Zona de Trabajo asignado";
            else
                estas = habilitarAcceso ? " Acceso Habilitado y Zona: " + codigoZonaTrabajo : "Se encuentra fuera de una Zona de Trabajo asignado";
            String mensaje = "Usuario " + estas;
            if (existeUsuario == false)
            {

            }
                

        }

        public async void getZonas()
        {
            int usuarioId = 5;
            String IP_LEGAL = "http://192.168.1.40";
            Zonas zonas = new Zonas();
            String url = IP_LEGAL + "/legal/ZonaTrabajo/ZonaTrabajoListarJsonExterno?id=" + usuarioId;

            var content = await _Client.GetStringAsync(url);
            var service = new RestClient<Zonas>();
            //Zonas = await service.GetRestServicieDataAsync(url);

            zonas = await service.GetRestServicieDataAsync(url);

            Zonas = new ObservableCollection<Zona>(zonas.zonas);
        }
        
        public async void posicionAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var currentLocation = await Geolocation.GetLocationAsync(request);
            userCurrentlocation = currentLocation;
            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
            //    new Position(location2.Latitude, location2.Longitude),
            //    Distance.FromMiles(.2)));

            googleMaps.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(currentLocation.Latitude, currentLocation.Longitude),
                Distance.FromMiles(.2)));

        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Login(App.Current);
        }

        private class AOCAdvancedSettings
        {
            public string title { get; set; }
            public string body { get; set; }
            public string sound { get; set; }
            public string click_action { get; set; }
            public string data { get; set; }
            public int codigo { get; set; }
            public string nombreUsuario { get; set; }
        }
    }
}