using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;


namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambioServicio _tipoCambioServicio;


        public ProductoReglas(ITipoCambioServicio tipoCambioServicio)
        {
            _tipoCambioServicio = tipoCambioServicio;
        }



        public async Task<decimal> CalcularPrecioUSD(decimal precioColones)
        {
            decimal tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioDolar();

            return Math.Round(precioColones / tipoCambio, 2);
        }
    }
}
