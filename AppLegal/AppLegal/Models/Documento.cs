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
        public bool respuesta { get; set; }

        public DocumentoEntidad[] documentoEntidadRevis { get; set; }
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
        public class DocumentoEntidadRevision
        {
            public int DocumentoId { get; set; }
            public int SubTipoServicioId { get; set; }
            public string NombreArchivo { get; set; }
            public string FechaRegistro { get; set; }
            public int StatusDocumento { get; set; }
            public string Nemonico { get; set; }
            public int TipoDocumentoId { get; set; }
            public int ProgramacionAtencionId { get; set; }
            public bool EsBorrador { get; set; }
            public string Version { get; set; }
            public string Etapa { get; set; }
            public int EsAprobado { get; set; }
            public int StatusTramite { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFin { get; set; }
            public string Status { get; set; }
            public string StatusGeneral { get; set; }
            public string Moneda { get; set; }
            public int Minuta { get; set; }
        }
    }
}
