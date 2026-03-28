CREATE PROCEDURE EliminarProducto

	@Id uniqueIdentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE 
	FROM            
							 Producto
	WHERE        (Id = @Id)

	SELECT @Id 
END