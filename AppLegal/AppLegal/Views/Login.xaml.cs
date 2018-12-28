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
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent();
		}

        private async void IngresarLoginAsync(object sender, EventArgs e)
        {
            App.Current.MainPage = new RootPage();
        }

       
    }
}