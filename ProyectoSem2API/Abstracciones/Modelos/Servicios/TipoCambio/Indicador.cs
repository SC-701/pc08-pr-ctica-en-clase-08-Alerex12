using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.TipoCambio
{
    public class Indicador
    {
        public string CodigoIndicador { get; set; }
        public string NombreIndicador { get; set; }
        public IEnumerable<Serie> Series { get; set; }
    }
}
