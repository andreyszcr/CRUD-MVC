using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventarios.Models
{
    public class Articulos
    {
        public int IdArticulo { get; set; }
        public int CodigoArticulos { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public decimal costo { get; set; }
        public int CodigoProveedor { get; set; }
    }
}