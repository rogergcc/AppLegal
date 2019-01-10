using AppLegal.Models;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
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
        //var documentoSeleccionado = new Documento.Datum();
        Documento.Datum documentoSeleccionado { get; set; }
        Documento documento = new Documento();
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
        bool displayFlag = false;

        int counter= 0;
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
            //https://dzone.com/articles/mobile-alerts-in-xamarin
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

            else
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
            #region Listener click a un item de la lista
            if (counter==0)
            {
                DocumentosEnTramite_List.ItemSelected += async (sender, e) =>
                {
                    if (e.SelectedItem != null)
                    {
                        //popupListView.IsVisible = true;
                        documentoSeleccionado = (Documento.Datum)e.SelectedItem;

                        //todo poup nuevo xmal
                        //var animationPopup = new PopupPageDoc(e.SelectedItem as Documento.Datum);
                        //fin todo poup nuevo xmal

                        Rg.Plugins.Popup.Pages.PopupPage animationPopup = new Rg.Plugins.Popup.Pages.PopupPage();

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
                        };
                        Label NombreArchivo = new Label
                        {
                            Text = "Nemonico: " + documentoSeleccionado.NombreArchivo,
                            FontSize = 13,
                            HorizontalOptions = LayoutOptions.Center,
                            Margin = 2
                        };
                        Label Nemonico = new Label
                        {
                            Text = documentoSeleccionado.Nemonico,
                            FontSize = 14,
                            HorizontalOptions = LayoutOptions.Center,
                            Margin = 2
                        };
                        Label SubTipoServicio = new Label
                        {
                            Text = "Tipo Contrato: " + documentoSeleccionado.SubTipoServicio,
                            FontSize = 14,
                            HorizontalOptions = LayoutOptions.Center,
                            Margin = 2
                        };
                        Label Fecha = new Label
                        {
                            Text = documentoSeleccionado.FechaRegistroString,
                            TextColor = Color.Brown,
                            FontSize = 13,
                            HorizontalOptions = LayoutOptions.Center,
                            Margin = 2
                        };
                        Button btnAprobarDocumento = new Button
                        {

                            Text = "APROBAR DOCUMENTO",
                            BackgroundColor = Color.DarkGoldenrod,
                            HeightRequest = 35,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            TextColor = Color.White
                        };
                        btnAprobarDocumento.Clicked += BtnAprobarDocumento_ClickedAsync;
                        Button btnCancelarDocumento = new Button
                        {

                            Text = "RECHAZAR DOCUMENTO",
                            BackgroundColor = Color.OrangeRed,
                            HeightRequest = 35,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            TextColor = Color.White
                        };
                        btnCancelarDocumento.Clicked += BtnCancelarDocumento_Clicked;
                        if (!estadoNom.Equals("POR APROBAR"))
                        {
                            btnAprobarDocumento.IsVisible = false;
                            btnCancelarDocumento.IsVisible = false;
                        }
                        animationPopup.Content = new StackLayout
                        {
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                            Padding = 0,
                            Margin = 0,
                            Children =
                        {
                            new Frame
                            {
                                Padding=15,
                                HeightRequest=200,
                                WidthRequest=270,
                                Content = new StackLayout
                                {
                                    Children = {
                                        NombreArchivo,Nemonico,SubTipoServicio,Fecha,
                                        btnAprobarDocumento,btnCancelarDocumento
                                    }
                                }
                            }
                        }
                        };
                        animationPopup.Animation = scaleAnimation;
                        //PopupNavigation.PushAsync(animationPopup);
                        await PopupNavigation.Instance.PushAsync(animationPopup);
                        DocumentosEnTramite_List.SelectedItem = null;
                    }
                };
                counter++;
            }
            #endregion fin Listener

            base.OnAppearing();
        }

        private void BtnCancelarDocumento_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private async void BtnAprobarDocumento_ClickedAsync(object sender, EventArgs e)
        {
            bool displayFlag = false;
            
            if (rol.Equals("1003") || rol.Equals("1004"))
            {
                //Bundle bundle = new Bundle();
                RevisionDocumento docDatos = new RevisionDocumento
                {
                    usuarioId = Int32.Parse(usuarioId),
                    documentoId = documentoSeleccionado.DocumentoId,
                    esRechazado = "No",
                    observacion = "",
                    perfil = rol,
                    nombre = documentoSeleccionado.NombreArchivo.ToString()
                };
                var documentosEnTramite = new AprobarDocSubirDocumento(docDatos as RevisionDocumento);

                //await rootPage.Navigation.PushAsync(documentosEnTramite);
                //await App.Current.MainPage .Navigation.PushAsync(documentosEnTramite);
                //App.Current.MainPage = new AprobarDocSubirDocumento(docDatos as object);
                
                await Navigation.PushAsync(documentosEnTramite);
                await Task.Delay(200);
                await PopupNavigation.Instance.PopAllAsync();
            }
            else
            {
                String IP_LEGAL = "http://192.168.1.40";
                String url = IP_LEGAL + "/legal/RevisionDocumento/RevizarDocumentoJson";
                var post = new
                {
                    usuarioId = usuarioId,
                    documentoId = documentoSeleccionado.DocumentoId,
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
                //label.BackgroundColor = Color.FromHex("#232323");
                label.VerticalTextAlignment = TextAlignment.Center;

                label.HeightRequest = 48;
                
                //label.Margin = new Thickness(16, 0, 0, 0);
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
                    //Padding = new Thickness(16, 0, 0, 0),
                    
                    HeightRequest = 50,
                    WidthRequest = 70,
                    Children = {
                        new StackLayout
                        {
                            BackgroundColor =Color.FromHex("#232323"),
                            Margin=0,
                            Padding=new Thickness(16,0),
                            Children= {
                                label
                            }
                        }
                        
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