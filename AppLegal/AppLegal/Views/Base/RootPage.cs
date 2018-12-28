using AppLegal.Helpers;
using AppLegal.ViewModel;
using AppLegal.Views.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppLegal.Views.Base
{
    public class RootPage : MasterDetailPage
    {
        Dictionary<MenuType, NavigationPage> Pages { get; set; }
        public RootPage()
        {

            Pages = new Dictionary<MenuType, NavigationPage>();
            Master = new MenuPage(this);
            Title = "Home";

            //BackgroundImage = "menubackground.png";
            BindingContext = new BaseViewModel
            {
                Title = "Adani",
                //Icon = "slideout.png"
            };
            //setup home page
            NavigateAsync(MenuType.Dashboard);

            InvalidateMeasure();
        }

        public async Task NavigateAsync(MenuType id)
        {
            try
            {

                if (Detail != null)
                {
                    IsPresented = false;
                }

                Page newPage;
                if (!Pages.ContainsKey(id))
                {

                    switch (id)
                    {
                        case MenuType.Dashboard:
                            Pages.Add(id, new CustomNavigationPage(new DashboardPage()));
                            break;
                        case MenuType.EditProfile:
                            Pages.Add(id, new CustomNavigationPage(new Dashboard.DashboardPage()));
                            IsPresented = false;

                            break;
                        case MenuType.ForgotPassword:
                            Pages.Add(id, new CustomNavigationPage(new Dashboard.DashboardPage()));
                            break;

                        case MenuType.Signout:
                            Pages.Clear();
                            // DependencyService.Get<INotification>().UnRegisterNotification();
                            //App.Database.DropTable();
                            
                            App.Current.MainPage = new Login();
                            break;
                    }
                }

                newPage = Pages[id];
                if (newPage == null)
                    return;

                Detail = newPage;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
