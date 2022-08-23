using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Modelos
{
    public class PostProductoVendido
    {
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

    }
}
