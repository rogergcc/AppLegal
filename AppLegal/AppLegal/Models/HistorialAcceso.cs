using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    public class HistorialAcceso
    {
        public int HistorialAccesoId { get; set; }

        public int UsuarioID { get; set; }

        public string Clave { get; set; }

        public string IMEI { get; set; }

        public string MAC { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Latitud { get; set; }

        public string Longitud { get; set; }

        public string Pais { get; set; }

        public string Ciudad { get; set; }

        public string Direccion { get; set; }

        public bool Exito { get; set; }
        public String FechaRegistroString { get; set; }
    }
}
