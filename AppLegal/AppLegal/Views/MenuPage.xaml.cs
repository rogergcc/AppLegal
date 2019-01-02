using AppLegal.Helpers;
using AppLegal.ViewModel;
using AppLegal.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLegal.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
        RootPage root;
        List<Models.MenuItem> menuItems;
        
        public MenuPage(RootPage root)
        {

            this.root = root;
            InitializeComponent();

            // BackgroundColor = Color.FromHex("#03A9F4");
            //  ListViewMenu.BackgroundColor = Color.FromHex("#F5F5F5");
            Title = "Home";
            BindingContext = new BaseViewModel
            {
                //Subtitle = App.User.FirstName+" "+ App.User.LastName,
                Title = App.Current.Properties["usuarioNombre"].ToString(),
                //Icon = "slideout.png"

            };
            //icon.Source = "userimage.png";

            ListViewMenu.ItemsSource = menuItems = new List<Models.MenuItem>
                {
                   new Models.MenuItem { Title = "Home", MenuType = MenuType.Dashboard, Icon ="home.png" },
                   new Models.MenuItem { Title = "Documentos", MenuType = MenuType.Dashboard, Icon ="docs.png",IsSeparatorVisible = true },
                   //new Models.MenuItem { Title = "EditProfile", MenuType = MenuType.EditProfile, Icon ="Editprofile.png", IsSeparatorVisible = true },
                   //new Models.MenuItem { Title = "Forgot Password", MenuType = MenuType.ForgotPassword, Icon ="ForgotPassword.png" },
                   new Models.MenuItem { Title = "Salir", MenuType = MenuType.Signout, Icon ="logout.png" },
                };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                ListViewMenu.SelectedItem = null;
                if (ListViewMenu.SelectedItem == null)
                    return;
                await this.root.NavigateAsync(((Models.MenuItem)e.SelectedItem).MenuType);
            };
        }
    }
}