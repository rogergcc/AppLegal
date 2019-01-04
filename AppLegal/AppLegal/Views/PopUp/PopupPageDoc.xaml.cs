using AppLegal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views.PopUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupPageDoc 
	{
        Documento.Datum datum { get; set; }
        public PopupPageDoc (Documento.Datum documentoSeleccionado)
		{
            
            datum = documentoSeleccionado;
            //DocumentoId.Text = datum.DocumentoId.ToString();
            //https://xamarinlatino.com/como-trabajar-con-ventanas-emergentes-pop-up-avanzados-en-xamarin-forms-7ebe109b0a37
            //https://www.youtube.com/watch?v=dOU0Qei3Qlk&t=438s

            

           
            InitializeComponent();
            datum = documentoSeleccionado;
            Nemonico.Text += datum.Nemonico.ToString();
            
            NombreArchivo.Text += documentoSeleccionado.NombreArchivo.ToString();
            SubTipoServicio.Text += documentoSeleccionado.SubTipoServicio.ToString();
            Fecha.Text = documentoSeleccionado.FechaRegistroString;
        }
    }
}