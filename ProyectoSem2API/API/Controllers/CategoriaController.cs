using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Flujo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase, ICategoriaController
    {
        private ILogger<IProductoController> _logger;
        private readonly ICategoriaFlujo _categoriaFlujo;

        public CategoriaController(ILogger<IProductoController> logger, ICategoriaFlujo categoriaFlujo)
        {
            _logger = logger;
            _categoriaFlujo = categoriaFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _categoriaFlujo.Obtener();

            if (!resultado.Any())
            {
                return NoContent();
            }

            return Ok(resultado);
        }
    }
}
