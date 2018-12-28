using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    public class Zonas
    {
        private string ZonaTrabajoId { get => ZonaTrabajoId; set => ZonaTrabajoId = value; }
        private string Descripcion { get => Descripcion; set => Descripcion = value; }
        private string UsuarioID { get => UsuarioID; set => UsuarioID = value; }
        private string Direccion { get => Direccion; set => Direccion = value; }
        private string Latitud { get => Latitud; set => Latitud = value; }
        private string Longitud { get => Longitud; set => Longitud = value; }
        private string Radio { get => Radio; set => Radio = value; }
        private string DentroZona { get => DentroZona; set => DentroZona = value; }
        private string Estado { get => Estado; set => Estado = value; }

        public Zonas()
        {
        }
        
        public Zonas(string zonaTrabajoId, string descripcion, string usuarioID, string direccion, string latitud, string longitud, string radio, string dentroZona, string estado)
        {
            ZonaTrabajoId = zonaTrabajoId;
            Descripcion = descripcion;
            UsuarioID = usuarioID;
            Direccion = direccion;
            Latitud = latitud;
            Longitud = longitud;
            Radio = radio;
            DentroZona = dentroZona;
            Estado = estado;
        }

        
    }

}
