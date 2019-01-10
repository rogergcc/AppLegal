﻿using AppLegal.Models;
using System;
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
        //CustomMap customMap { get; set; }
        Xamarin.Forms.GoogleMaps.Map googleMaps { get; set; }
        Circle circle = null;
        public SesionWeb()
        {
            InitializeComponent();
            var margin = 20;

            //var deviceId = CrossDeviceInfo.Current.Id;

            //falta averiguar que antes q ejecute el mapa verifique permisos Location

            #region Layout por codigo
            #region codigo
            //var customMap = new CustomMap
            //{
            //    MapType = MapType.Street,
            //    WidthRequest = App.ScreenWidth,
            //    HeightRequest = App.ScreenHeight
            //};

            //customMap = new CustomMap
            //{
            //    IsShowingUser = true,
            //    HeightRequest = App.ScreenHeight,
            //    WidthRequest = App.ScreenWidth,
            //    Margin = margin - 5,

            //    VerticalOptions = LayoutOptions.FillAndExpand

            //};

            googleMaps = new Xamarin.Forms.GoogleMaps.Map
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

        private async void ObtenerZonas_ClickedAsync(object sender, EventArgs e)
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

            CustomCircle customCircle = new CustomCircle
            {

            };


            for (int i = 0; i < zonas.zonas.Length; i++)
            {
                var position = new Position(zonas.zonas[i].Latitud, zonas.zonas[i].Longitud);
                //customMap.Circle = new CustomCircle
                //{
                //    Position = position,
                //    Radius = zonas.zonas[i].Radio
                //};

                //customMap.Pins.Add( new Pin
                //{
                //    Position = new Position(zonas.zonas[i].Latitud, zonas.zonas[i].Longitud),
                //    Label = zonas.zonas[i].Direccion
                //}

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
        protected override async void OnAppearing()
        {


        }
        public async void posicionAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location2 = await Geolocation.GetLocationAsync(request);

            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(
            //    new Position(location2.Latitude, location2.Longitude),
            //    Distance.FromMiles(.2)));

            googleMaps.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(location2.Latitude, location2.Longitude),
                Distance.FromMiles(.2)));

        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Login(App.Current);
        }
    }
}