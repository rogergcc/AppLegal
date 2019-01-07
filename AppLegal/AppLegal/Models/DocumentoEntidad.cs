using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    //return Json(new { data = lista.ToList(), mensaje = errormensaje, respuestaConsulta= respuestaConsulta });

    public class DocumentoEntidad
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
