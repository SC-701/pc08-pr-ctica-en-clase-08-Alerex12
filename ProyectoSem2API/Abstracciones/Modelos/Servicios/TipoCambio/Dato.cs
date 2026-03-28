using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.TipoCambio
{
    public class Dato
    {
        public string Titulo { get; set; }
        public string Periodicidad { get; set; } 
        public IEnumerable<Indicador> Indicadores { get; set; }
    }
}
