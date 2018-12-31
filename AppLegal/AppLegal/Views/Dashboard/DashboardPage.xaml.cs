using AppLegal.Models;
using AppLegal.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views.Dashboard
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardPage : ContentPage
	{
        private const string url = "http://192.168.0.12/legal/ZonaTrabajo/ZonaTrabajoListarJsonExterno?id=2";
        private HttpClient _Client = new HttpClient();
        private ObservableCollection<Zona> _post;
        public MainPageViewModel ViewModel { get; set; }
        public ObservableCollection<Zona> Zonas { get; set; }
        public DashboardPage ()
		{
			InitializeComponent ();

		}
        protected override async void OnAppearing()
        {

            var content = await _Client.GetStringAsync(url);
            var service = new RestClient<Zonas>();
            var zonas = await service.GetRestServicieDataAsync(url);
            _post = new ObservableCollection<Zona>(zonas.zonas);

            Post_List.ItemsSource = _post;
            base.OnAppearing();
        }
    }
}