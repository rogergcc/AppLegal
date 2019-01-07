using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views.Documentos
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AprobarDocSubirDocumento : ContentPage
	{
		public AprobarDocSubirDocumento (object datos)
		{
			InitializeComponent ();
		}

        private async void btnSubiDocumento(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Sin exito", "Seleccion de una foto no soportada", "Ok");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file==null)
            {
                return;
            }
            Imagen.Source = ImageSource.FromStream(() => file.GetStream());
        }
    }
}