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
		public PopupPageDoc (Documento.Datum documentoSeleccionado)
		{
            DocumentoId.Text = documentoSeleccionado.DocumentoId.ToString();
            //https://xamarinlatino.com/como-trabajar-con-ventanas-emergentes-pop-up-avanzados-en-xamarin-forms-7ebe109b0a37
            //https://www.youtube.com/watch?v=dOU0Qei3Qlk&t=438s

            Status.Text = documentoSeleccionado.Status;
            SubTipoServicio.Text = documentoSeleccionado.SubTipoServicio;

           
            InitializeComponent();
		}
	}
}