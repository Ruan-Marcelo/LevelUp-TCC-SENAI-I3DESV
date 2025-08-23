
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE Categoria_Crud
	-- Add the parameters for the stored procedure here
	@Action VARCHAR(15),
	@CategoriaId INT = NULL,
	@CategoriaNome VARCHAR(100) = NULL,
	@CategotiaImgUrl  VARCHAR(MAX) = NULL,
	@EstaAtivo BIT = FALSE
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- GET Categoria
	IF(@Action = 'GETALL')
	BEGIN
		SELECT * FROM Categoria
	END

	  -- INSERT  Categoria
	IF(@Action = 'INSERT')
	BEGIN
		INSERT INTO Categoria(CategoriaNome, CategoriaImgUrl, EstaAtivo, DataCriacao)
		VALUES(@CategoriaNome, @CategotiaImgUrl, @EstaAtivo, GETDATE())
	END

	-- UPDATE  Categoria
	IF(@Action = 'UPDATE')
	BEGIN
		DECLARE @UPDATE_IMAGE VARCHAR(20)
		SELECT @UPDATE_IMAGE = (CASE WHEN @CategotiaImgUrl IS NULL THEN 'NO' ELSE 'YES'  END)
		IF (@UPDATE_IMAGE = 'NO')
			BEGIN			
				UPDATE Categoria
				SET CategoriaNome = @CategoriaNome, EstaAtivo = @EstaAtivo
				WHERE CategoriaId = CategoriaId
			END
		ELSE
				BEGIN
					UPDATE Categoria
					SET CategoriaNome = @CategoriaNome, CategoriaImgUrl = @CategotiaImgUrl, EstaAtivo = @EstaAtivo
					WHERE CategoriaId = CategoriaId
				END
	END

	-- DELLET Categoria
	IF(@Action = 'DELETE')
	BEGIN
		DELETE FROM Categoria WHERE CategoriaId = @CategoriaId
	END

	-- GET ATIVO Categoria POR CategoriaId
	IF(@Action = 'ACTIVECATEGORIA')
	BEGIN
		SELECT * FROM Categoria c
		WHERE c.EstaAtivo = 1 ORDER BY c.CategoriaNome
	END
END
GO
