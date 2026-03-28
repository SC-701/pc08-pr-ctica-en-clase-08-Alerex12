using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace ProductosWeb.Pages.Productos
{
    [Authorize(Roles = "2")]
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }


        [BindProperty]
        public ProductoResponse productoResponse { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid categoriaSeleccionada { get; set; }
        [BindProperty]
        public Guid subcategoriaSeleccionada { get; set; }



        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            var cliente = ObtenerClienteConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();


            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategorias();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                productoResponse = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

                if (productoResponse != null)
                {
                    categoriaSeleccionada = Guid.Parse(categorias.Where(m => m.Text == productoResponse.Categoria).FirstOrDefault().Value);
                    subcategorias = (await ObtenerSubcategoria(categoriaSeleccionada)).Select(m =>
                    new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Nombre,
                        Selected = m.Nombre == productoResponse.SubCategoria
                    }).ToList();
                    subcategoriaSeleccionada = Guid.Parse(subcategorias.Where(m => m.Text == productoResponse.SubCategoria).FirstOrDefault().Value);

                }
            }
            return Page();

        }


        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
            var cliente = ObtenerClienteConToken();

            var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(string.Format(endpoint, productoResponse.Id),
                new ProductoRequest
                {
                    Nombre = productoResponse.Nombre,
                    Descripcion = productoResponse.Descripcion,
                    Precio = productoResponse.Precio,
                    Stock = productoResponse.Stock,
                    CodigoBarras = productoResponse.CodigoBarras,
                    IdSubCategoria = subcategoriaSeleccionada
                    
                });
            if (!respuesta.IsSuccessStatusCode)
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
            return RedirectToPage("./Index");

        }



        private async Task ObtenerCategorias()
        {

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategoria");
            var cliente = ObtenerClienteConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);

            if (!respuesta.IsSuccessStatusCode)
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                throw new Exception(error);
            }

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var resultadoDeserializado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);
            categorias = resultadoDeserializado.Select(m =>

            new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nombre
            }
            ).ToList();
        }

        private async Task<List<Subcategoria>> ObtenerSubcategoria(Guid categoriaId)
        {

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubcategoria");
            var cliente = ObtenerClienteConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaId));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                return JsonSerializer.Deserialize<List<Subcategoria>>(resultado, opciones);

            }
            return new List<Subcategoria>();
        }

        public async Task<JsonResult> OnGetObtenerSubcategorias(Guid categoriaId)
        {

            var modelos = await ObtenerSubcategoria(categoriaId);
            return new JsonResult(modelos);
        }
        private HttpClient ObtenerClienteConToken()
        {
            var tokenClaim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "Token");
            var cliente = new HttpClient();
            if (tokenClaim != null)
                cliente.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer", tokenClaim.Value);
            return cliente;
        }
    }
}
