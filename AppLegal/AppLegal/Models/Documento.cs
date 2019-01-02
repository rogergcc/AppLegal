using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    public class Documento
    {
        
            public Datum[] data { get; set; }
            public string mensaje { get; set; }
            public bool respuestaConsulta { get; set; }
       

        public class Datum
        {
            public int DocumentoId { get; set; }
            public object NombreArchivo { get; set; }
            public string Nemonico { get; set; }
            public string TipoDocumento { get; set; }
            public string SubTipoServicio { get; set; }
            public string FechaRegistroString { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFin { get; set; }
            public string Objeto { get; set; }
            public object Version { get; set; }
            public string Status { get; set; }
        }

    }
}
