using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    public class EstadoProceso
    {
        public int EstadoProcesoId { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public int CantidaDocumentoxEstado { get; set; }
    }
}
