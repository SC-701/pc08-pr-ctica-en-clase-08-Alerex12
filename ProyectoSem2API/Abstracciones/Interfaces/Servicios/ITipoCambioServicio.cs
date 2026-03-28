using Abstracciones.Modelos.Servicios.TipoCambio;


namespace Abstracciones.Interfaces.Servicios
{
    public interface ITipoCambioServicio
    {
        public Task<decimal> ObtenerTipoCambioDolar();

    }
}
