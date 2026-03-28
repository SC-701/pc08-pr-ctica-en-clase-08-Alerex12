using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;


namespace Flujo
{
    public class CategoriaFlujo : ICategoriaFlujo
    {
        private readonly ICategoriaDA _categoriaDa;

        public CategoriaFlujo(ICategoriaDA categoriaDa)
        {
            _categoriaDa = categoriaDa;
        }

        public async Task<IEnumerable<Categoria>> Obtener()
        {
            return await _categoriaDa.Obtener();
        }
    }
}
