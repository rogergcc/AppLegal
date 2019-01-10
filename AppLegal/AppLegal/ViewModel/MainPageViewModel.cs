using AppLegal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AppLegal.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<Zona> Zonas { get; set; }
        public async Task LoadZonas()
        {
            var url = "http://192.168.0.12/legal/ZonaTrabajo/ZonaTrabajoListarJsonExterno?id=2";
           
            var service = new RestClient<Zonas>();
            var zonas = await service.GetRestServicieDataAsync(url);
            Zonas = new ObservableCollection<Zona>(zonas.zonas);
        }
    }
}
