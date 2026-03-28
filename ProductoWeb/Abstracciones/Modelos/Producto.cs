using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "La propiedad Nombre es requerida")]
        [StringLength(100, ErrorMessage = "La propiedad Nombre debe ser mayor a 4 caracteres y menor a 100", MinimumLength = 4)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La propiedad Descripción es requerida")]
        [StringLength(300, ErrorMessage = "La propiedad Nombre debe ser mayor a 10 caracteres y menor a 300", MinimumLength = 10)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La propiedad Precio es requerida")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El precio no puede ser negativo")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "La propiedad Stock es requerida")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "La propiedad Codigo de barras es requerida")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El stock no puede ser negativo")]

        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }

    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string? SubCategoria { get; set; }
        public string? Categoria { get; set; }
    }
    public class ProductoDetalle : ProductoResponse
    {
        public decimal PrecioUSD { get; set; }
    }


}
