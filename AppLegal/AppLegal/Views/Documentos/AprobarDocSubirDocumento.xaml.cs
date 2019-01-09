using AppLegal.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
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
        RevisionDocumento documentoRevision { get; set; }
           
            
        string imagenorPdf { get; set; }
		public AprobarDocSubirDocumento (RevisionDocumento datos)
		{

            documentoRevision = datos;
           
            BindingContext = datos;
            InitializeComponent ();
		}

        private async void btnElegirDocumento(object sender, EventArgs e)
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
            
            byte[] b = System.IO.File.ReadAllBytes(file.Path);
            String s = Convert.ToBase64String(b);
            //imagenorPdf = ImageSource.FromStream(() => file.GetStream()).ToString();
            imagenorPdf  = Convert.ToBase64String(b);
            documentoRevision.esImagen = true;
        }

        private async void btnSubirDocumentoAsync(object sender, EventArgs e)
        {
            String IP_LEGAL = "http://192.168.1.40";
            String url = IP_LEGAL + "/legal/RevisionDocumento/RevizarDocumentoJson";
            documentoRevision.fileImagenOrPdf = imagenorPdf;
            
            Documento documento = new Documento();
            var service = new RestClient<Documento>();
            documento = await service.GetRestServicieDataPostAsync(url, documentoRevision);
            
            //ObservableCollection<Documento.Datum> docDatas = new ObservableCollection<Documento.Datum>(documento.data);

        }

        private async void btnElegirPdfAsync(object sender, EventArgs e)
        {
            try
            {
                Plugin.FilePicker.Abstractions.FileData fileData = await Plugin.FilePicker.CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                string fileName = fileData.FileName;
                //string contents = System.Text.Encoding.UTF8.GetString(fileData.DataArray);

                imagenorPdf = Convert.ToBase64String(fileData.DataArray);
                documentoRevision.esImagen = false;
                //System.Console.WriteLine("File name chosen: " + fileName);
                //System.Console.WriteLine("File data: " + contents);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }
    }
}