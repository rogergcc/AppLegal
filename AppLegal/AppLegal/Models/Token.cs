using System;
using System.Collections.Generic;
using System.Text;

namespace AppLegal.Models
{
    public class Token
    {
        public TokenDigital[] token { get; set; }
        public string mensaje { get; set; }
        public int tiempoexpiracion { get; set; }

        public class TokenDigital
        {
            public string token { get; set; }
            public int UsuarioID { get; set; }
            public DateTime FechaRegistro { get; set; }
            public DateTime FechaExpira { get; set; }
        }

    }
}
