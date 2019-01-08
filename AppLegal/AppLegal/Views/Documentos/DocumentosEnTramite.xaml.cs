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
	public partial class DocumentosEnTramite : ContentPage
	{
        EstadoProceso estado { get; set; }
        public DocumentosEnTramite (EstadoProceso estadoProceso)
		{
            //2DA VISTA A
			InitializeComponent ();
            estado = estadoProceso;

            BindingContext = estado;
            poblarDocumentosEnTramite();

            EstadosDocumentosEnTramite_List.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    var estadoP = new EstadoProceso();
                    estadoP = (EstadoProceso)e.SelectedItem;

                    var documentosEnTramite = new ListaDocumentosEnTramite(e.SelectedItem as EstadoProceso);
                    
                        await Navigation.PushAsync(documentosEnTramite);
                    
                    //if (estadoProcesoViewHolder.getNombre().equals("EN TRAMITE")){
                    //    Intent intentTramite = new Intent(v.getContext(), DocumentosActivity.class);
                    //    intentTramite.putExtras(bundle);
                    //    v.getContext().startActivity(intentTramite);
                    //}else{
                    //    Intent newDDocumentsActivity = new Intent(v.getContext(), DetalleDocumentosActivity.class);
                    //    newDDocumentsActivity.putExtras(bundle);
                    //    v.getContext().startActivity(newDDocumentsActivity);
                    //}


                    EstadosDocumentosEnTramite_List.SelectedItem = null;
                }
            };
        }
        public void poblarDocumentosEnTramite()
        {
            ObservableCollection<EstadoProceso> estadoProcesos = new ObservableCollection<EstadoProceso>();

            EstadoProceso estaProcesoPrimero = new EstadoProceso();
            estaProcesoPrimero.EstadoProcesoId = 32;
            estaProcesoPrimero.Nombre = "POR APROBAR";
            //estaProcesoPrimero.Tipo = "STATUS DOCUMENTO";
            estadoProcesos.Add (estaProcesoPrimero);

            EstadoProceso estaProcesoSEg = new EstadoProceso () ;
            estaProcesoSEg.EstadoProcesoId = 29;
            estaProcesoSEg.Nombre = "APROBADOS";
            //estaProcesoSEg.Tipo = "STATUS DOCUMENTO";
            estadoProcesos.Add(estaProcesoSEg);

            EstadoProceso estaProcesoTer = new EstadoProceso ();
            estaProcesoTer.EstadoProcesoId = 31;
            estaProcesoTer.Nombre = "RECHAZADOS";
            //estaProcesoTer.Tipo = "STATUS DOCUMENTO";
            estadoProcesos.Add(estaProcesoTer);

            EstadoProceso estaProcesoCuar = new EstadoProceso () ;
            estaProcesoCuar.EstadoProcesoId = 30;
            estaProcesoCuar.Nombre = "STATUS TRAMITE";
            //estaProcesoCuar.Tipo = "STATUS DOCUMENTO";
            estadoProcesos.Add(estaProcesoCuar);

            EstadosDocumentosEnTramite_List.ItemsSource = estadoProcesos;
        }
	}
}