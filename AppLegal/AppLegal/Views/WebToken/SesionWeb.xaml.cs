using AppLegal.IViewModel;
using AppLegal.Models;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using Plugin.FirebasePushNotification;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        Entry ingresarIP { get; set; }
        Button obtenerZonas { get; set; }
        string ontokenRefresh { get; set; }
        Circle circle = null;

        private ISend _isendService;
        public SesionWeb()
        {
            InitializeComponent();
            var margin = 20;

            //_isendService = DependencyService.Get<ISend>();
            //_isendService.Send(myobject.codigo, myobject.nombreUsuario);

            //Device.BeginInvokeOnMainThread(() => {

            //    txtUsuario.Text = (myobject!=null)?myobject.nombreUsuario:"usuario";

            //});
            //var deviceId = CrossDeviceInfo.Current.Id;
            //falta averiguar que antes q ejecute el mapa verifique permisos Location

            #region Layout MAPS por codigo
            #region  MAPS codigo lib GoogleMaps
            googleMaps = new Xamarin.Forms.GoogleMaps.Map
            {
                IsShowingUser = true,
                MyLocationEnabled = true,
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
            var position = new Position(36.9628066, -122.0194722);

            obtenerZonas = new Button { Text = "Obtener Zonas" };
            obtenerZonas.Clicked += ObtenerZonas_ClickedAsync;

            txtUsuario = new Label
            {
                WidthRequest = 60,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Usuario"
            };
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
            var stackUsuario = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { imageUser, txtUsuario }
            };
            var stackImei = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                WidthRequest = 60,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { botonCodigoCel, txtImei }
            };
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
            var tokenEnviadodesdeMainActivity = "";

            tokenEnviadodesdeMainActivity = App.Current.Properties["TokenPush"].ToString();

            var deviceId = CrossDeviceInfo.Current.Id;

            enviarTokenAlServidorAsync(tokenEnviadodesdeMainActivity, deviceId);

            ontokenRefresh = CrossFirebasePushNotification.Current.Token;
            System.Diagnostics.Debug.WriteLine("ontokenRefresh : " + ontokenRefresh);
            System.Diagnostics.Debug.WriteLine("tokenEnviadodesdeActividad : " + tokenEnviadodesdeMainActivity);

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("TokenRefresh :" + p.Token);
                ontokenRefresh = p.Token;
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
                try
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //App.Current.MainPage = new SesionWeb();
                        System.Diagnostics.Debug.WriteLine("Enter OnNotificationReceived");
                        var json = JsonConvert.SerializeObject(p.Data, Newtonsoft.Json.Formatting.Indented);
                        FirebasePushNotificactionData myobject;
                        myobject = new FirebasePushNotificactionData();
                        myobject = JsonConvert.DeserializeObject<FirebasePushNotificactionData>(json);
                        txtUsuario.Text = myobject.nombreUsuario;
                        obtenerDatosDelServicioZonasTrabajoAsync(myobject.codigo);
                    });
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine("error: " + ex.Message);
                }
            };
        }

        private async void enviarTokenAlServidorAsync(string tokenEnviadodesdeMainActivity, string deviceId)
        {
            String IP_LEGAL = App.Current.Properties["IpPublicado"].ToString();
            String url = IP_LEGAL + "/legal/DispositivoUsuario/ActualizarTokenDispositivoDelUsuarioSegunImeiRegistradoJson";

            var post = new
            {
                imei = deviceId,
                token = tokenEnviadodesdeMainActivity,
                
            };
            Documento documento = new Documento();
            var service = new RestClient<Documento>();
            documento = await service.GetRestServicieDataPostAsync(url, post);

            var respuesta = documento.mensaje;
            var mensaje = (documento.respuestaConsulta) ? "Token Registrado Correctamente" : "Token No Registrado, Verifique IMEI \n este registrado en el Sistema";
            mensajeEstaDentroZonaUsuarioAsync(mensaje, documento.respuestaConsulta, documento.respuestaConsulta);
        }

        public async void obtenerDatosDelServicioZonasTrabajoAsync(int codigoUsuario)
        {
            googleMaps.Circles.Clear();
            googleMaps.Pins.Clear();
            String IP_LEGAL = App.Current.Properties["IpPublicado"].ToString();
            Zonas zonas = new Zonas();
            String url = IP_LEGAL + "/legal/ZonaTrabajo/ZonaTrabajoListarJsonExterno?id=" + codigoUsuario;
            var content = await _Client.GetStringAsync(url);
            var service = new RestClient<Zonas>();

            zonas = await service.GetRestServicieDataAsync(url);
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
                circle = new Circle();
                circle.IsClickable = true;
                circle.Center = position;
                circle.Radius = Distance.FromMeters(zonas.zonas[i].Radio);
                circle.StrokeColor = Color.SaddleBrown;
                circle.StrokeWidth = 3f;
                circle.FillColor = Color.FromRgba(0, 0, 255, 32);
                circle.Tag = zonas.zonas[i].Direccion; // Can set any object
                googleMaps.Circles.Add(circle);

                distance = Location.CalculateDistance(userCurrentlocation, position.Latitude,
                    position.Longitude, DistanceUnits.Kilometers);//this result give in KM
                distance = distance * metersInKm;

                userObtenerID = zonas.zonas[i].UsuarioID;
                if (distance < zonas.zonas[i].Radio && codigoUsuario.Equals(userObtenerID))
                {
                    habilitarAcceso = true;
                    codigoZonaTrabajo = zonas.zonas[i].ZonaTrabajoId;
                }
                else
                    habilitarAcceso = (habilitarAcceso) ? habilitarAcceso : false;
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
            Position positionGMaps = new Position(userCurrentlocation.Latitude, userCurrentlocation.Longitude);
            Geocoder geocoder = new Geocoder();
            IEnumerable<string> address = await geocoder.GetAddressesForPositionAsync(positionGMaps);
            var pais = "4";
            var ciudad = "2";
            var direccion = "0";
            direccion = address.ElementAt(0);
            ciudad = address.ElementAt(2);
            pais = address.ElementAt(4);
            string imei = "";
            HistorialAccesoInsertarJsonExternoAsync(codigoUsuario, imei,
                                    userCurrentlocation.Latitude,
                                    userCurrentlocation.Longitude,
                                    pais,
                                    ciudad,
                                    direccion);
            mensajeEstaDentroZonaUsuarioAsync(mensaje, existeUsuario, habilitarAcceso);
        }

        private async void HistorialAccesoInsertarJsonExternoAsync(int codigoUsuario, string imei, double latitude, double longitude, string pais, string ciudad, string direccion)
        {
            String IP_LEGAL = App.Current.Properties["IpPublicado"].ToString();
            String url = IP_LEGAL + "/legal/HistorialAcceso/HistorialAccesoInsertarJsonExterno";
            
            HistorialAcceso historialAcceso = new HistorialAcceso
            {
                UsuarioID = codigoUsuario,
                IMEI = imei,
                Latitud = latitude.ToString(),
                Longitud = longitude.ToString(),
                Pais = pais,
                Ciudad=ciudad,
                Direccion=direccion
            };
            Documento documento = new Documento();
            var service = new RestClient<Documento>();
            documento = await service.GetRestServicieDataPostAsync(url, historialAcceso);

            var respuesta = documento.mensaje;
        }

        private async void mensajeEstaDentroZonaUsuarioAsync(string mensaje, bool existeUsuario, bool habilitarAcceso)
        {
            Color color;
            if (existeUsuario)
                color = Color.DarkRed;
            if (habilitarAcceso)
                color = Color.DarkGreen;
            else
                color = Color.DarkRed;
            Rg.Plugins.Popup.Pages.PopupPage popupPage = new Rg.Plugins.Popup.Pages.PopupPage();
            Label label = new Label();
            label.Text = mensaje;
            label.TextColor = Color.White;
            //label.BackgroundColor = Color.FromHex("#232323");
            label.VerticalTextAlignment = TextAlignment.Center;

            label.HeightRequest = 48;

            //label.Margin = new Thickness(16, 13);
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Bottom,
                PositionOut = MoveAnimationOptions.Bottom,
                ScaleIn = 2,
                ScaleOut = 2,
                //DurationIn = 400,
                //DurationOut = 800,
                EasingIn = Easing.Linear,
                HasBackgroundAnimation = true,
            };

            popupPage.Animation = scaleAnimation;
            popupPage.Content = new FlexLayout
            {
                Direction = FlexDirection.Column,
                JustifyContent = FlexJustify.End,
                BackgroundColor = Color.Transparent,
                Margin = 0,
                //Padding = new Thickness(16, 0, 0, 0),
                HeightRequest = 50,
                WidthRequest = 70,
                Children = {
                        new StackLayout
                        {
                            BackgroundColor = color,
                            Margin=0,
                            Padding=new Thickness(16,0),
                            Children= {
                                label
                            }
                        }
                    }
            };
            //popupPage.HeightRequest = 50;


            await PopupNavigation.Instance.PushAsync(popupPage);
            await Task.Delay(2500);
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void ObtenerZonas_ClickedAsync(object sender, EventArgs e)
        {
            obtenerDatosDelServicioZonasTrabajoAsync(5);
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

        private class FirebasePushNotificactionData
        {
            public string title { get; set; }
            public string body { get; set; }
            public string sound { get; set; }
            public string click_action { get; set; }
            public string data { get; set; }
            public int codigo { get; set; }
            public string nombreUsuario { get; set; }
        }

        private async void TlbIpPublicada_ClickedAsync(object sender, EventArgs e)
        {

            Rg.Plugins.Popup.Pages.PopupPage animationPopup = new Rg.Plugins.Popup.Pages.PopupPage();
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Bottom,
                PositionOut = MoveAnimationOptions.Bottom,
                ScaleIn = 1,
                ScaleOut = 1,
                //DurationIn = 400,
                //DurationOut = 800,
                EasingIn = Easing.Linear,
                //EasingOut = Easing.CubicOut,
                HasBackgroundAnimation = true,
            };
            Label IpPublica = new Label
            {
                Text = "IP PUBLICADA ",
                FontSize = 13,
                HorizontalOptions = LayoutOptions.Center,
                Margin = 2
            };
            Label lblIp = new Label
            {
                Text = "IP: ",
                FontSize = 14,
                HorizontalOptions = LayoutOptions.Center,
                Margin = 2
            };
            ingresarIP = new Entry
            {
                Placeholder = "Ingrese IP",
                Text = App.Current.Properties["IpPublicado"].ToString()
            };
            StackLayout layoutIngresarIpPublica = new StackLayout
            {
                Children =
                {
                    lblIp,ingresarIP
                }
            };
            Button btnCambiarIpPublicada = new Button
            {

                Text = "ACTUALIZAR IP",
                BackgroundColor = Color.DarkGoldenrod,
                HeightRequest = 35,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White
            };
            btnCambiarIpPublicada.Clicked += BtnCambiarIpPublicada_ClickedAsync;

            animationPopup.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Margin = 0,
                Children =
                        {
                            new Frame
                            {
                                Padding=15,
                                HeightRequest=200,
                                WidthRequest=270,
                                Content = new StackLayout
                                {
                                    Children = {
                                        IpPublica,layoutIngresarIpPublica,
                                        btnCambiarIpPublicada
                                    }
                                }
                            }
                        }
            };
            animationPopup.Animation = scaleAnimation;
            //PopupNavigation.PushAsync(animationPopup);
            await PopupNavigation.Instance.PushAsync(animationPopup);
        }

        private async void BtnCambiarIpPublicada_ClickedAsync(object sender, EventArgs e)
        {

            App.Current.Properties["IpPublicado"] = ingresarIP.Text;
            await Task.Delay(2500);
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}