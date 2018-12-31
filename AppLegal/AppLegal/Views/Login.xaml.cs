using AppLegal.ViewModel;
using AppLegal.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        public MainPageViewModel ViewModel { get; set; }
        public string _username;
        public string _password;
        private bool _areCredentialsInvalid;
        
        public Login ()
		{
			InitializeComponent();
            CredencialesValidas.IsVisible = false;

        }

        private bool UserAuthenticated(string username, string password)
        {
            if (string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return username.ToLowerInvariant() == "joe"
                && password.ToLowerInvariant() == "secret";
        }
        public async Task<bool> validacionLoginAsync()
        {
            _username = Username.Text;
            _password = Password.Text;
            bool validado = false;
            if (string.IsNullOrEmpty(_username))
            {
                Username.Focus();
                validado = false;
            }
            if (string.IsNullOrEmpty(_password))
            {
                Password.Focus();
                validado = false;
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.1.38/legal/Usuario/ValidacionLoginExternoJson?usuLogin=utecnico&usuPassword=1111");

            string jsonData = @"{""usuLogin"" : ""myusername"", ""usuPassword"" : ""mypassword""}";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/foo/login", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            CredencialesValidas.IsVisible = (validado)?validado:true;
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
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ViewModel = new MainPageViewModel();
            this.BindingContext = ViewModel;
            await ViewModel.LoadZonas();
        }
       
    }
}