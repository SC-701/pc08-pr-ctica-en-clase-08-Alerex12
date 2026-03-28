using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class SubcategoriaFlujo : ISubcategoriaFlujo
    {
        private readonly ISubcategoriaDA _subcategoriaDA;

        public SubcategoriaFlujo(ISubcategoriaDA subcategoriaDA)
        {
            _subcategoriaDA = subcategoriaDA;
        }

        public async Task<IEnumerable<Subcategoria>> Obtener(Guid idCategoria)
        {
            return await _subcategoriaDA.Obtener(idCategoria);
        }
    }
}
