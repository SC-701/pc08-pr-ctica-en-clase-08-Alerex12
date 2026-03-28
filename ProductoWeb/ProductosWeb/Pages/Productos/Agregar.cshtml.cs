using Abstracciones.Modelos;
using Abstracciones.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ProductosWeb.Pages.Productos
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid categoriaSeleccionada { get; set; }


        public async Task<ActionResult> OnGet()
        {
            await ObtenerCategorias();
            return Page();

        }
        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Post, endpoint);

            var respuesta = await cliente.PostAsJsonAsync(endpoint, producto);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");

        }

        private async Task ObtenerCategorias()
        {

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategoria");
            var cliente = new HttpClient();
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
            var cliente = new HttpClient();
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
    }
}
