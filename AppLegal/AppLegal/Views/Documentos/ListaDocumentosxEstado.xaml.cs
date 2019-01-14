using AppLegal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views.Documentos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaDocumentosxEstado : ContentPage
	{
        EstadoProceso estado { get; set; }
        string usuarioId = App.Current.Properties["usuarioId"].ToString();
        string usuarioNombre = App.Current.Properties["usuarioNombre"].ToString();
        string rol = App.Current.Properties["rol"].ToString();
        string empleadoId = App.Current.Properties["empleadoId"].ToString();
        public ListaDocumentosxEstado (EstadoProceso estadoProceso)
		{
			InitializeComponent ();
            estado = estadoProceso;

            BindingContext = estado;
        }
        protected override async void OnAppearing()
        {
            String IP_LEGAL = App.Current.Properties["IpPublicado"].ToString();
            Documento documento = new Documento();

            String servicio = "";
            if (rol.Equals("1003") || rol.Equals("1004"))
            {
                servicio = "DocumentoPorResponsableRevisionListarExternoJson";
            }
            else
            {
                servicio = "DocumentoPorEspecialistaListarExternoJson";
            }

            var post = new
            {
                empleadoId = empleadoId,
                estado = estado.EstadoProcesoId
            };
            //String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp";
            //String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorStatusDocumentoApp";

            //String url = IP_LEGAL + "/legal/Documento/" + servicio + "?estadoProcesoId=" + estadoId + "&usuarioId=" + usuarioId;

            var service = new RestClient<Documento>();
            //documento = await service.GetRestServicieDataPostAsync(url, post);
            //ObservableCollection<Documento.Datum> docDatas = new ObservableCollection<Documento.Datum>(documento.data);

            //TODO EN LOS DETALLES POR DOC SELECCIONADO dialog o botoom sheet

            //Documentos_List.ItemsSource = docDatas;

            base.OnAppearing();
        }
    }
}