using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TipoCambio;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;


namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlBase;
        private readonly string _bearerToken;

        public TipoCambioServicio(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _urlBase = configuration["BancoCentralCR:UrlBase"] ?? throw new ArgumentNullException("BancoCentralCR:UrlBase no configurado");
            _bearerToken = configuration["BancoCentralCR:BearerToken"] ?? throw new ArgumentNullException("BancoCentralCR:BearerToken no configurado");
        }

        public async Task<decimal> ObtenerTipoCambioDolar()
        {
            string fecha = DateTime.Now.ToString("yyyy/MM/dd");
            string url = $"{_urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

            HttpResponseMessage respuesta = await _httpClient.GetAsync(url);

            respuesta.EnsureSuccessStatusCode();

            string json = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var resultado = JsonSerializer.Deserialize<TipoCambio>(json, opciones);

            decimal tipoCambio = resultado?
            .Datos?.FirstOrDefault()?
            .Indicadores?.FirstOrDefault()?
            .Series?.FirstOrDefault()?
            .ValorDatoPorPeriodo ?? 0;

            if (tipoCambio == 0)
                throw new Exception("No se pudo obtener el tipo de cambio del BCCR");

            return tipoCambio;

        }
    }
}
