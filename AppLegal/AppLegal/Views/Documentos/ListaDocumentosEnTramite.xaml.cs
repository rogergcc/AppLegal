using AppLegal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views.Documentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDocumentosEnTramite : ContentPage
    {
       
        string usuarioId = App.Current.Properties["usuarioId"].ToString();
        string usuarioNombre = App.Current.Properties["usuarioNombre"].ToString();
        string rol = App.Current.Properties["rol"].ToString();
        string empleadoId = App.Current.Properties["empleadoId"].ToString();

        string estadoNom = "";
        private HttpClient _Client = new HttpClient();
        public void DocumentoPorEspecialistaListarExternoJson()
        {
            // TODO  POR APROBAR

            String servicio = "";

            if (rol.Equals("1003") || rol.Equals("1004"))
            {
                servicio = "DocumentoPorResponsableRevisionListarExternoJson";
            }
            else
            {
                servicio = "DocumentoPorEspecialistaListarExternoJson";
            }

        }
        public async void ListarDocumentosPorControlStatusAppAsync()
        {
            //TODO STATUS TRAMITE
            String IP_LEGAL = "http://192.168.1.40";



            String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp?empleadoId=" + empleadoId;


            var content = await _Client.GetStringAsync(url);
            var service = new RestClient<Documento>();
            var documento = await service.GetRestServicieDataAsync(url);
            //_post = new ObservableCollection<Zona>(zonas.zonas);
        }
        public ListaDocumentosEnTramite(EstadoProceso estado)
        {
            var estadoid = estado.EstadoProcesoId;
            estadoNom = estado.Nombre;
            InitializeComponent();
            
            //DocumentoPorEspecialistaListarExternoJson();

        }
        protected override async void OnAppearing()
        {
            String IP_LEGAL = "http://192.168.1.40";
            Documento documento = new Documento();
            if (estadoNom.Equals("STATUS TRAMITE"))
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://192.168.1.40/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp");

                var login = new
                {
                    empleadoId = empleadoId

                };
                var json = JsonConvert.SerializeObject(login);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(client.BaseAddress, httpContent);
                if (response.IsSuccessStatusCode)
                {

                }

                String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp?empleadoId=" + empleadoId;


                var respuestaGetJson = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                documento = JsonConvert.DeserializeObject<Documento>(respuestaGetJson, settings);
                //documento = await service.GetRestServicieDataAsync(url);
                
            }

            
            base.OnAppearing();
        }
    }
}