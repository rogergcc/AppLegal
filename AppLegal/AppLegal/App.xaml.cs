using AppLegal.IViewModel;
using AppLegal.Views;
using AppLegal.Views.Base;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppLegal
{
    public partial class App : Application, ILoginManager
    {
        public static double ScreenHeight;
        public static double ScreenWidth;
        public static App Current;
        static ILoginManager loginManager;
        public App(string tokenRecibido)
        {
            InitializeComponent();
            //var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;
            #region Lista y Cards
            //var cards = new CardData();

            //var cardstack = new StackLayout
            //{
            //    Spacing = 15,
            //    Padding = new Thickness(10),
            //    VerticalOptions = LayoutOptions.StartAndExpand,
            //};

            //foreach (var card in cards)
            //{
            //    cardstack.Children.Add(new CardView(card));
            //}

            //var shadowcardstack = new StackLayout
            //{
            //    Spacing = 5,
            //    Padding = new Thickness(2),
            //    VerticalOptions = LayoutOptions.StartAndExpand,
            //};

            //foreach (var card in cards)
            //{
            //    shadowcardstack.Children.Add(new ShadowCardView(card));
            //}

            //var page = new ContentPage
            //{
            //    Title = "Documentos",
            //    BackgroundColor = Color.White,
            //    Content = new ScrollView()
            //    {
            //        Content = new StackLayout()
            //        {
            //            Children = {
            //                cardstack,
            //                shadowcardstack
            //            }
            //        }
            //    }
            //};

            //MainPage = new NavigationPage(page)
            //{
            //    BarBackgroundColor = StyleKit.BarBackgroundColor,
            //    BarTextColor = Color.White
            //};
            #endregion fin lista Cards
            
            Current = this;

            Current.Properties["IpPublicado"] = "http://192.168.0.12";

            //
            App.Current.Properties["TokenPush"] = tokenRecibido;
            //
            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;

            // we remember if they're logged in, and only display the login page if they're not
            if (isLoggedIn)
                App.Current.MainPage = new RootPage();
            else
                App.Current.MainPage = new Login(this);

            //MainPage = new Login(); ///login

            //App.Current.MainPage = new Login(); //LOGIN

            //MainPage = new NavigationPage(new Login());
            //MainPage = new RootPage(); //Navigation Drawer

            //MainPage = new MainPage();
            var ontokenRefresh="";
            ontokenRefresh = CrossFirebasePushNotification.Current.Token;
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("TOKEN OnTokenRefresh:" + p.Token);
                ontokenRefresh = p.Token;
                
            };
            
            var notificationReceived = "";

            //var refreshedToken = FirebaseInstanceId.Instance.Token;

            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{
            //    Console.Out.WriteLine("TOKEN CONSOLE : + p." + p.Data);
            //    notificationReceived = p.Data.ToString();
            //    object objetoRecivido = p.Data;
            //    var data = new
            //    {
            //        codigo = 0,
            //        nombreUsuario = ""
            //    };
                
            //    var json = JsonConvert.SerializeObject(p.Data, Newtonsoft.Json.Formatting.Indented);
            //    //var myobject = JsonConvert.DeserializeObject<AOCAdvancedSettings>(json);

            //    System.Diagnostics.Debug.WriteLine("Received");
                
            //};

            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Opened");
            //    foreach (var data in p.Data) 
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //    }
            //};
        }

        public void ShowMainPage()
        {

            App.Current.MainPage = new RootPage();
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false; // only gets set to 'true' on the LoginPage
            Properties["usuarioId"] = "";
            Properties["usuarioNombre"] = "";
            Properties["rol"] = "";
            Properties["empleadoId"] = "";
            //Properties["IpPublicado"] = ""; no borrar la ip
            App.Current.MainPage = new Login(this);

        }
    }
}
