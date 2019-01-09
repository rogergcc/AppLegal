using AppLegal.IViewModel;
using AppLegal.Models;
using AppLegal.ViewModel;
using AppLegal.Views.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
    {
        //http://simettric.com/blog/login-xamarin-forms/
        public MainPageViewModel ViewModel { get; set; }
        public string _username;
        public string _password;
        private bool _areCredentialsInvalid;

        ILoginManager ilogin;
        public Login (ILoginManager ilm)
		{
			InitializeComponent();
            ilogin = ilm;
            CredencialesValidas.IsVisible = false;

        }
        
        public async Task<bool> validacionLoginAsync()
        {
            _username = Username.Text;
            _password = Password.Text;
            Usuario usuario = new Usuario();
            bool validado = false;
            String mensaje = "";
            if (string.IsNullOrEmpty(_username))
            {
                Username.Focus();
                validado = false;
                return validado;
            }
            if (string.IsNullOrEmpty(_password))
            {
                Password.Focus();
                validado = false;
                return validado;
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.1.40/legal/Usuario/ValidacionLoginExternoJson");

            
            var login = new 
            {
                usuLogin = _username,
                usuPassword = _password
               
            };

            //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await client.PostAsync("/foo/login", content);
            //var result = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.SerializeObject(login);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
           
            var response = await client.PostAsync(client.BaseAddress, httpContent);
            if (response.IsSuccessStatusCode)
            {
                var respuestaGetJson = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                //var jsonModel = JsonConvert.DeserializeObject<Customer>(jsonString, settings);
                //JObject jsonobjec = JObject.Parse(respuestaGetJson);
                //var service = new RestClient<Usuario>();
                usuario = JsonConvert.DeserializeObject<Usuario>(respuestaGetJson, settings);
                validado = usuario.respuesta;
                mensaje = usuario.mensaje;
                
               
            }
            
            CredencialesValidas.IsVisible = (validado)?validado:true;
            CredencialesValidas.Text = mensaje;

            App.Current.Properties["IsLoggedIn"] = validado;
            App.Current.Properties["usuarioId"] = usuario.usuarioId;
            App.Current.Properties["usuarioNombre"] = usuario.usuarioNombre;
            App.Current.Properties["rol"] = usuario.rol;
            App.Current.Properties["empleadoId"] = usuario.empleado.EmpleadoID;
            if (validado)
            {
                ilogin.ShowMainPage();
            }
            
            return validado;
        }
        private async void IngresarLoginAsync(object sender, EventArgs e)
        {
            _areCredentialsInvalid = false;

            _areCredentialsInvalid = await validacionLoginAsync();
            if (_areCredentialsInvalid)
            {
                
                App.Current.MainPage = new RootPage();
            }
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //ViewModel = new MainPageViewModel();
            //this.BindingContext = ViewModel;
            //await ViewModel.LoadZonas();
        }

        private void BtnIngresarSesionWeb_ClickedAsync(object sender, EventArgs e)
        {
            //https://www.c-sharpcorner.com/article/navigation-in-xamarin-forms/

            App.Current.MainPage = new MainPage();
            //await Navigation.PushAsync(new MainPage());
        }
    }
}