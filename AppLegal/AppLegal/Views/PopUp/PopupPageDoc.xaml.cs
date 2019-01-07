using AppLegal.IViewModel;
using AppLegal.Models;
using AppLegal.Views.Documentos;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLegal.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupPageDoc
    {
        Documento.Datum datum { get; set; }
        //Documento.Datum datum { get; set; }
        Documento documento = new Documento();
        string usuarioId = App.Current.Properties["usuarioId"].ToString();
        string usuarioNombre = App.Current.Properties["usuarioNombre"].ToString();
        string rol = App.Current.Properties["rol"].ToString();
        string empleadoId = App.Current.Properties["empleadoId"].ToString();
        public PopupPageDoc(Documento.Datum documentoSeleccionado)
        {

            datum = documentoSeleccionado;
            //DocumentoId.Text = datum.DocumentoId.ToString();
            //https://xamarinlatino.com/como-trabajar-con-ventanas-emergentes-pop-up-avanzados-en-xamarin-forms-7ebe109b0a37
            //https://www.youtube.com/watch?v=dOU0Qei3Qlk&t=438s
            //https://askxammy.com/how-to-work-with-advanced-pop-ups-in-xamarin-forms/



            InitializeComponent();
            datum = documentoSeleccionado;
            Nemonico.Text += datum.Nemonico.ToString();

            NombreArchivo.Text += documentoSeleccionado.NombreArchivo.ToString();
            SubTipoServicio.Text += documentoSeleccionado.SubTipoServicio.ToString();
            Fecha.Text = documentoSeleccionado.FechaRegistroString;

            
        }
        //public void RevizarDocumentoJson()
        private async void aprobarDocumentoAsync(object sender, EventArgs e)
        {
            if (rol.Equals("1003") || rol.Equals("1004"))
            {
                //Bundle bundle = new Bundle();
                object docDatos = new {
                    usuarioId= usuarioId,
                    documentoId = datum.DocumentoId,
                    esRechazado = "No",
                    observacion ="",
                    rol=rol,
                    documentoNombre=datum.NombreArchivo
                };
                var documentosEnTramite = new AprobarDocSubirDocumento(docDatos as object);
                
                //await rootPage.Navigation.PushAsync(documentosEnTramite);
                //await App.Current.MainPage .Navigation.PushAsync(documentosEnTramite);

                App.Current.MainPage = new AprobarDocSubirDocumento(docDatos as object);

            }
            else {
                String IP_LEGAL = "http://192.168.1.40";
                
                String url = IP_LEGAL+"/legal/RevisionDocumento/RevizarDocumentoJson";
                
                var post = new
                {
                    usuarioId = usuarioId,
                    documentoId = datum.DocumentoId,
                    esRechazado = "No",
                    observacion = "",
                    perfil = rol,
                    fileImagenOrPdf = "",
                    nombre = "",
                    esImagen = false,
                };
                var service = new RestClient<Documento>();
                documento = await service.GetRestServicieDataPostAsync(url, post);
                ObservableCollection<Documento.Datum> docDatas = new ObservableCollection<Documento.Datum>(documento.data);
               
                //await DisplayAlert("Documento: " + datum.DocumentoId, documento.mensaje, "OK");
                String message = documento.mensaje;

                //if (Device.OS == TargetPlatform.Android)
                //{
                //    DependencyService.Get<IMessage>().ShortAlert(message);
                //}
                //if (Device.OS == TargetPlatform.iOS)
                //{
                //    DependencyService.Get<IMessage>().ShortAlert(message);
                //}

                #region Utilizando la libreria popup
                Rg.Plugins.Popup.Pages.PopupPage popupPage = new Rg.Plugins.Popup.Pages.PopupPage();
                Label label = new Label();
                label.Text = message;
                label.TextColor = Color.White;
                label.BackgroundColor = Color.FromHex("#232323");
                label.VerticalTextAlignment = TextAlignment.Center;
                
                label.HeightRequest = 35;
               
                //label.Margin = 3;
                var scaleAnimation = new ScaleAnimation
                {
                    PositionIn = MoveAnimationOptions.Bottom,
                    PositionOut = MoveAnimationOptions.Bottom,
                    ScaleIn = 2,
                    ScaleOut = 2,
                    //DurationIn = 400,
                    //DurationOut = 800,
                    EasingIn = Easing.Linear,
                    HasBackgroundAnimation = true,
                };
                popupPage.Animation = scaleAnimation;
                popupPage.Content = new FlexLayout
                {
                    Direction = FlexDirection.Column,
                    JustifyContent = FlexJustify.End,
                    BackgroundColor = Color.Transparent,
                    
                    Margin = 0,
                    
                    HeightRequest = 50,
                    WidthRequest = 70,
                    Children = {
                        label
                        }
                };
                //popupPage.HeightRequest = 50;

                
                await PopupNavigation.Instance.PushAsync(popupPage);

                await Task.Delay(2000);
                await PopupNavigation.Instance.PopAllAsync();
                #endregion fin usando la liberia popup
            }
        }
    }
}