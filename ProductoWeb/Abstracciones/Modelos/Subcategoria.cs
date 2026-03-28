using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Subcategoria
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public Guid IdCategoria { get; set; }
    }
}
