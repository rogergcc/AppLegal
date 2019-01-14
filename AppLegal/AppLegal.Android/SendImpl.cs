using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppLegal.IViewModel;
using Xamarin.Forms;


[assembly: Dependency(typeof(AppLegal.Droid.SendImpl))]
namespace AppLegal.Droid
{
    public class SendImpl : ISend
    {
        public void Send(int codigo, string nombreUsuario)
        {
            Intent intent = new Intent("com.companyname.AppLegal.fcm_legal");
            intent.PutExtra("codigo", codigo);
            intent.PutExtra("nombreUsuario", nombreUsuario);
            Forms.Context.SendBroadcast(intent);
        }

        
    }
}