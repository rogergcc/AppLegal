using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    public class RevisionDocumento
    {
       
        public int usuarioId { get; set; }
        public int documentoId { get; set; }
        public string esRechazado { get; set; }
        public string observacion { get; set; }
        public string perfil { get; set; }
        public string fileImagenOrPdf { get; set; }
        public string nombre { get; set; }
        public bool esImagen { get; set; }
    }
}
