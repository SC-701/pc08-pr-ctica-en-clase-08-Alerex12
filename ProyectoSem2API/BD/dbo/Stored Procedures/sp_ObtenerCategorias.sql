  CREATE PROCEDURE sp_ObtenerCategorias
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id],
        [Nombre]
    FROM [ProductosBD].[dbo].[Categorias];
END;