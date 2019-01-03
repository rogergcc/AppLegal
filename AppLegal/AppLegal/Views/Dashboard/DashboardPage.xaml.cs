using AppLegal.Models;
using AppLegal.ViewModel;
using AppLegal.Views.Documentos;
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
        private const string url = "http://192.168.1.40/legal/ZonaTrabajo/ZonaTrabajoListarJsonExterno?id=2";
        private HttpClient _Client = new HttpClient();
        private ObservableCollection<Zona> _post;
        public MainPageViewModel ViewModel { get; set; }
        public ObservableCollection<Zona> Zonas { get; set; }
        public DashboardPage ()
		{
			InitializeComponent ();
            Estados_List.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    var estadoP = new EstadoProceso();
                    estadoP = (EstadoProceso)e.SelectedItem;

                    var documentosEnTramite = new DocumentosEnTramite(e.SelectedItem as EstadoProceso);
                    if (estadoP.Nombre.Equals("EN TRAMITE"))
                    {
                        await Navigation.PushAsync(documentosEnTramite);
                    }
                    else{
                        
                    }
                    
                    Estados_List.SelectedItem = null;
                }
            };
        }
        protected override async void OnAppearing()
        {


            var content = await _Client.GetStringAsync(url);
            var service = new RestClient<Zonas>();
            var zonas = await service.GetRestServicieDataAsync(url);
            _post = new ObservableCollection<Zona>(zonas.zonas);

            ObservableCollection<EstadoProceso> estadoProcesos = new ObservableCollection<EstadoProceso>();

            EstadoProceso estaProcesoPrimero = new EstadoProceso();
            estaProcesoPrimero.EstadoProcesoId = 32;
            estaProcesoPrimero.Nombre="EN TRAMITE";
            estaProcesoPrimero.Tipo = "STATUS DOCUMENTO";
            estadoProcesos.Add(estaProcesoPrimero);

            EstadoProceso estaProcesoSEg = new EstadoProceso();
            estaProcesoSEg.EstadoProcesoId = 29;
            estaProcesoSEg.Nombre = "TERMINADO";
            estaProcesoSEg.Tipo = "STATUS DOCUMENTO";
            estadoProcesos.Add(estaProcesoSEg);

            EstadoProceso estaProcesoTer = new EstadoProceso();
            estaProcesoTer.EstadoProcesoId = 31;
            estaProcesoTer.Nombre = "CANCELADO";
            estaProcesoTer.Tipo= "STATUS DOCUMENTO";
            estadoProcesos.Add(estaProcesoTer);

            EstadoProceso estaProcesoCuar = new EstadoProceso();
            estaProcesoCuar.EstadoProcesoId = 30;
            estaProcesoCuar.Nombre = "CURSO";
            estaProcesoCuar.Tipo= "STATUS DOCUMENTO";
            estadoProcesos.Add(estaProcesoCuar);

            Estados_List.ItemsSource = estadoProcesos;
            base.OnAppearing();
        }

        private void TextCell_BindingContextChanged(object sender, System.EventArgs e)
        {

        }
    }
}