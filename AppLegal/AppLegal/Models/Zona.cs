using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{


    public class Zonas
    {
        public Zona[] zonas { get; set; }
        public string mensaje { get; set; }
    }
    public class Zona
    {
        public int ZonaTrabajoId { get; set; }
        public int UsuarioID { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public bool Estado { get; set; }
        public string Radio { get; set; }
        public bool UbicadoZona { get; set; }
    }
    
}
