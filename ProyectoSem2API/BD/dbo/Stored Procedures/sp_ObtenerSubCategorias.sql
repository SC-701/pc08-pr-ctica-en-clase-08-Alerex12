  CREATE PROCEDURE sp_ObtenerSubCategorias
  @IdCategoria AS  uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id],
        [IdCategoria],
        [Nombre]
    FROM [ProductosBD].[dbo].[SubCategorias]

    WHERE IdCategoria =  @IdCategoria

    END