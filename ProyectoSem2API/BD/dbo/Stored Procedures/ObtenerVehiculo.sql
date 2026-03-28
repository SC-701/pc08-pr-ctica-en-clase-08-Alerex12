CREATE PROCEDURE ObtenerVehiculo

	@Id uniqueIdentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT        Producto.Id, Producto.IdSubCategoria, Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Stock, Producto.CodigoBarras, SubCategorias.Nombre AS Subcategoria, Categorias.Nombre AS Categoria
FROM            Categorias INNER JOIN
                         SubCategorias ON Categorias.id = SubCategorias.IdCategoria INNER JOIN
                         Producto ON SubCategorias.Id = Producto.IdSubCategoria
WHERE        (Producto.Id = @Id)
END