using System;

namespace WebApi.DTOs
{
    public class RespuestaAutenticacion
    {
        public string Respuesta { get; set; }
        public DateTime Expiracion { get; set; }
        public string Token { get; internal set; }
    }
}
