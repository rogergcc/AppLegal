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
            String IP_LEGAL = "http://192.168.1.40";
            Documento documento = new Documento();

            var post = new
            {
                empleadoId = empleadoId,
                estado = estado.EstadoProcesoId
            };
            //String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorControlStatusApp";
            String url = IP_LEGAL + "/legal/RevisionDocumento/ListarDocumentosPorStatusDocumentoApp";


            var service = new RestClient<Documento>();
            documento = await service.GetRestServicieDataPostAsync(url, post);
            ObservableCollection<Documento.Datum> docDatas = new ObservableCollection<Documento.Datum>(documento.data);

//            //TODO EN LOS DETALLES POR DOC SELECCIONADO dialog o botoom sheet
//            botonCancelar.setOnClickListener(new View.OnClickListener() {

//                        @Override
//                        public void onClick(View v) {

//                            Bundle bundle = new Bundle();

//                            bundle.putString("vNombreDocRechazar", documentoViewHolder.getNombre());
//                            bundle.putInt("vUsuarioId", Integer.parseInt(usuarioId));
//                            bundle.putInt("vDocumentoId", documentoViewHolder.getDocumentoId());
//                            bundle.putString("vPerfil", perfil);
//                            bundle.putString("vRechazar", "Si");
//                            Intent newDDocumentsActivity = new Intent(v.getContext(), RechazarDocumentoActivity.class);
//                            newDDocumentsActivity.putExtras(bundle);
//                            v.getContext().startActivity(newDDocumentsActivity);
//                        }
//                    });
//                    botonAprobar.setOnClickListener(new View.OnClickListener() {
//                        @Override
//                        public void onClick(View v) {
//                            myDialog.hide();

//                            if (perfil.equals("1003") || perfil.equals("1004")) {
//                                Bundle bundle = new Bundle();

//                                bundle.putInt("vusuarioId", Integer.parseInt(usuarioId));
//                                bundle.putInt("vdocumentoId", documentoId);
//                                bundle.putString("vesRechazado", "No");
//                                bundle.putString("vobservacion", "");
//                                bundle.putString("vperfil", perfil);

//                                bundle.putString("vnombre", documentoViewHolder.getNombre());
//                                Intent newDDocumentsActivity = new Intent(v.getContext(), UploadImageActivity.class);
//                                newDDocumentsActivity.putExtras(bundle);
//                                v.getContext().startActivity(newDDocumentsActivity);
//                                //startActivity(new Intent(v.getContext(), UploadImageActivity.class));
//                            } else {
//                                RevizarDocumentoJson(Integer.parseInt(usuarioId),documentoId,"No","",perfil,"","",false);
//                                Snackbar.make(vista, respuestaRevisizarDocuento, Snackbar.LENGTH_LONG)
//                                        .setAction("Action", null).show();        

//                            }
            Documentos_List.ItemsSource = docDatas;
            base.OnAppearing();
        }
    }
}