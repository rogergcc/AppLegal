using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppLegal.Helpers
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page root) : base(root)
        {
            Init();

        }

        public CustomNavigationPage()
        {
            Init();

        }

        void Init()
        {


            BarTextColor = Color.White;
        }
    }
}