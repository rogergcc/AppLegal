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
	public partial class DocumentosxEstadoProceso : ContentPage
	{
        EstadoProceso estado { get; set; }
        public DocumentosxEstadoProceso (EstadoProceso estadoProceso)
		{
            //2 VISTA B
            InitializeComponent ();
            estado = estadoProceso;

            BindingContext = estado;
        }
        protected override async void OnAppearing()
        {
            String IP_LEGAL = "http://192.168.1.40";
            Documento documento = new Documento();

            var post = new
            {
                empleadoId = "",
                estado = estado.EstadoProcesoId
            };
            //String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp";
            String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorStatusDocumentoApp";


            var service = new RestClient<Documento>();
            documento = await service.GetRestServicieDataPostAsync(url, post);
            ObservableCollection<Documento.Datum> docDatas = new ObservableCollection<Documento.Datum>(documento.data);

            //TODO EN LOS DETALLES POR DOC SELECCIONADO dialog o botoom sheet

            DocumentosSegunEstado.ItemsSource = docDatas;
            base.OnAppearing();
        }
    }
}