using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppBase.Models
{
    public class User
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string rol { get; set; }
        public string correo { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string password { get; set; }
    }
}