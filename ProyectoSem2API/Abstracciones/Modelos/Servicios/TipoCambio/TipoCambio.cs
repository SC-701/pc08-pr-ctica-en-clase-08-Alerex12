using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.TipoCambio
{
    public class TipoCambio
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public IEnumerable<Dato> Datos { get; set; }

    }
}
