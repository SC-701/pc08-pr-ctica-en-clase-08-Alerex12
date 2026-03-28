using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {

        private IProductoDA _ProductoDA;
        private readonly IProductoReglas _productoReglas;

        public ProductoFlujo(IProductoDA productoDA, IProductoReglas productoReglas)
        {
            _ProductoDA = productoDA;
            _productoReglas = productoReglas;
        }


        public async Task<Guid> Agregar(ProductoRequest producto)
        {

            return await _ProductoDA.Agregar(producto);
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            return await _ProductoDA.Editar(Id, producto);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _ProductoDA.Eliminar(Id);
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            return await _ProductoDA.Obtener();
        }

        public async Task<ProductoDetalle> Obtener(Guid Id)
        {
            var producto = await _ProductoDA.Obtener(Id);
            if (producto == null) return null;

            producto.PrecioUSD = await _productoReglas.CalcularPrecioUSD(producto.Precio);

            return producto;

        }
    }
}
