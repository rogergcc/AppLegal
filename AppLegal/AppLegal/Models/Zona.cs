using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppLegal.Models
{


    public class Zonas
    {
        public Zona[] zonas { get; set; }
        public string mensaje { get; set; }

        public static implicit operator ObservableCollection<object>(Zonas v)
        {
            throw new NotImplementedException();
        }
    }
    public class Zona
    {
        public int ZonaTrabajoId { get; set; }
        public int UsuarioID { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool Estado { get; set; }
        public double Radio { get; set; }
        public bool UbicadoZona { get; set; }
    }
    
}
