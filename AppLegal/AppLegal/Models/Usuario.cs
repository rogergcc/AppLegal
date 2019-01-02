using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    public class Usuario
    {
            public string usuarioId { get; set; }
            public string usuarioNombre { get; set; }
            public string correo { get; set; }
            public string cargo { get; set; }
            public string rol { get; set; }
            public bool respuesta { get; set; }
            public string mensaje { get; set; }
            public Empleado empleado { get; set; }
            public Usuario usuario { get; set; }
        

        public class Empleado
        {
            public object NombreCompleto { get; set; }
            public int EmpleadoID { get; set; }
            public string Nombres { get; set; }
            public string ApellidosPaterno { get; set; }
            public string ApellidosMaterno { get; set; }
            public int CargoID { get; set; }
            public string CargoNombre { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string Direccion { get; set; }
            public int DOIID { get; set; }
            public string DOIIDNombre { get; set; }
            public string DOI { get; set; }
            public string Telefono { get; set; }
            public object Movil { get; set; }
            public string Genero { get; set; }
            public string MailJob { get; set; }
            public string MailPersonal { get; set; }
            public int EstadoEmpleado { get; set; }
            public DateTime FechaAlta { get; set; }
            public int Perfil { get; set; }
            public int rol_legal { get; set; }
        }

        public class UsuarioD
        {
            public string NombreEmpleado { get; set; }
            public object MailJob { get; set; }
            public int UsuarioID { get; set; }
            public int EmpleadoID { get; set; }
            public int TipoUsuarioID { get; set; }
            public string UsuarioNombre { get; set; }
            public string UsuarioContraseña { get; set; }
            public string FechaRegistro { get; set; }
            public int FailedAttempts { get; set; }
            public int Estado { get; set; }
            public int EstadoContrasena { get; set; }
            public bool SesionActiva { get; set; }
        }

    }
}
