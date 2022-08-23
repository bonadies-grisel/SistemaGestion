using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Modelos
{
    public class PostTest
    {
        public PostVenta PostVenta { get; set; }
        public List <Producto> productos { get; set; }
        public PostProductoVendido PostVendido { get; set; }
    }
}
