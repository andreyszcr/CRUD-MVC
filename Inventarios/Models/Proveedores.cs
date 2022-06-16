using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventarios.Models
{
    public class Proveedores
    {
        public int CodigoProveedor { get; set; }
        public string Nombre { get; set; }
        public bool estado { get; set; }
        public int cedula { get; set; }
        public string direccion { get; set; }
        public int telefono { get; set; }
    }
}