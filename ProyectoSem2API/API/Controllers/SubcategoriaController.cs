using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriaController : ControllerBase, ISubcategoriaController
    {
        private ILogger<IProductoController> _logger;
        private readonly ISubcategoriaFlujo _subcategoriaFlujo;

        public SubcategoriaController(ILogger<IProductoController> logger, ISubcategoriaFlujo subcategoriaFlujo)
        {
            _logger = logger;
            _subcategoriaFlujo = subcategoriaFlujo;
        }

        [HttpGet("{idCategoria}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid idCategoria)
        {
            var resultado = await _subcategoriaFlujo.Obtener(idCategoria);

            if (!resultado.Any())
            {
                return NoContent();
            }

            return Ok(resultado);
        }
    }
}
