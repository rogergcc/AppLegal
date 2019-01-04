using AppLegal.Models;
using AppLegal.Views.PopUp;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        int estadoId = 0;
        string estadoNom = "";
        ObservableCollection<Documento.Datum> docDatas;
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
           
            InitializeComponent();
            estadoId = estado.EstadoProcesoId;
            estadoNom = estado.Nombre;
            BindingContext = estado;

            
            //DocumentoPorEspecialistaListarExternoJson();
            
        }
        protected override async void OnAppearing()
        {
            String IP_LEGAL = "http://192.168.1.40";
            Documento documento = new Documento();
            
            if (estadoNom.Equals("POR APROBAR"))
            {
                String servicio = "";
                if (rol.Equals("1003") || rol.Equals("1004"))
                {
                    servicio = "DocumentoPorResponsableRevisionListarExternoJson";
                }
                else
                {
                    servicio = "DocumentoPorEspecialistaListarExternoJson";
                }
                
                //String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp";
                String url = IP_LEGAL + "/legal/Documento/" + servicio + "?estadoProcesoId=" + estadoId + "&usuarioId=" + usuarioId;
                var service = new RestClient<Documento>();
                documento = await service.GetRestServicieDataAsync(url);
                ObservableCollection<Documento.Datum> docDatas = new ObservableCollection<Documento.Datum>(documento.data);
                DocumentosEnTramite_List.ItemsSource = docDatas;
            }
            else if (estadoNom.Equals("APROBADOS"))
            {
                var post = new
                {
                    empleadoId = empleadoId,
                    perfil = rol
                };
                String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosAprobadosApp";
                //String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp";
                var service = new RestClient<Documento>();
                documento = await service.GetRestServicieDataPostAsync(url, post);
                ObservableCollection<Documento.Datum> docDatas = new ObservableCollection<Documento.Datum>(documento.data);
                DocumentosEnTramite_List.ItemsSource = docDatas;
            }
            else if (estadoNom.Equals("RECHAZADOS"))
            {
                var post = new
                {
                    empleadoId = empleadoId,
                    perfil = rol
                };
                String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosRechazadosApp";

                //String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp";
                var service = new RestClient<Documento>();
                documento = await service.GetRestServicieDataPostAsync(url, post);
                ObservableCollection<Documento.Datum> docDatas = new ObservableCollection<Documento.Datum>(documento.data);
                DocumentosEnTramite_List.ItemsSource = docDatas;
            }
            else if (estadoNom.Equals("STATUS TRAMITE"))
            {
                var post = new
                {
                    empleadoId = empleadoId
                };
                String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp";
                var service = new RestClient<Documento>();
                documento = await service.GetRestServicieDataPostAsync(url, post);
                docDatas = new ObservableCollection<Documento.Datum>(documento.data);
                DocumentosEnTramite_List.ItemsSource = docDatas;

            }

            DocumentosEnTramite_List.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    //popupListView.IsVisible = true;

                    var documentoSeleccionado = new Documento.Datum();
                    documentoSeleccionado = (Documento.Datum)e.SelectedItem;

                    //todo poup
                    var animationPopup = new PopupPageDoc(e.SelectedItem as Documento.Datum);

                    //PopupNavigation.Instance.PushAsync(new PopupPageDoc());
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
                        //<animations:ScaleAnimation PositionIn="Bottom" PositionOut="Center" ScaleIn="1" ScaleOut="0.7" DurationIn="700" EasingIn="BounceOut"/>
                    };

                    animationPopup.Animation = scaleAnimation;
                    //PopupNavigation.PushAsync(animationPopup);
                    await PopupNavigation.Instance.PushAsync(animationPopup);



                    //DocumentoId.Text = documentoSeleccionado.DocumentoId.ToString();

                    //Status.Text = documentoSeleccionado.Status;
                    //SubTipoServicio.Text = documentoSeleccionado.SubTipoServicio;

                    DocumentosEnTramite_List.SelectedItem = null;
                }
            };
            base.OnAppearing();
        }
    }
}