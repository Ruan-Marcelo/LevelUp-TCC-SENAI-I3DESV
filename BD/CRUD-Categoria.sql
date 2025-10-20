ALTER PROCEDURE [dbo].[Categoria_Crud]
    @Action           VARCHAR(15),
    @CategoriaId      INT = NULL,
    @CategoriaNome    NVARCHAR(100) = NULL,
    @CategoriaImgUrl  NVARCHAR(400) = NULL,
    @EstaAtivo        BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    IF (@Action = 'GETALL')
    BEGIN
        SELECT * FROM Categoria;
        RETURN;
    END

    IF (@Action = 'GETBYID')
    BEGIN
        SELECT * FROM Categoria WHERE CategoriaId = @CategoriaId;
        RETURN;
    END

    IF (@Action = 'INSERT')
    BEGIN
        INSERT INTO Categoria (CategoriaNome, CategoriaImgUrl, EstaAtivo, DataCriacao)
        VALUES (@CategoriaNome, @CategoriaImgUrl, @EstaAtivo, GETDATE());
        RETURN;
    END

    IF (@Action = 'UPDATE')
    BEGIN
        IF (@CategoriaImgUrl IS NULL OR @CategoriaImgUrl = '')
        BEGIN
            UPDATE Categoria
            SET CategoriaNome = @CategoriaNome,
                EstaAtivo     = @EstaAtivo
            WHERE CategoriaId = @CategoriaId;
        END
        ELSE
        BEGIN
            UPDATE Categoria
            SET CategoriaNome   = @CategoriaNome,
                CategoriaImgUrl = @CategoriaImgUrl,
                EstaAtivo       = @EstaAtivo
            WHERE CategoriaId = @CategoriaId;
        END
        RETURN;
    END

    IF (@Action = 'DELETE')
    BEGIN
        DELETE FROM Categoria WHERE CategoriaId = @CategoriaId;
        RETURN;
    END

    IF (@Action = 'ACTIVECATEGORIA')
    BEGIN
       SELECT c.CategoriaId, c.CategoriaNome, c.CategoriaImgUrl,
		(
			SELECT COUNT(*) FROM SubCategoria s WHERE s.CategoriaId = c.CategoriaId AND s.EstaAtivo = 1
		) AS SubCategoriaCount,
		(
			
			SELECT COUNT(*) FROM Produto p WHERE c.CategoriaId = p.CategoriaId AND p.EstaAtivo = 1
		) AS ProdutoCount
		FROM Categoria c 
		WHERE c.EstaAtivo = 1 ORDER BY c.CategoriaNome


        --SELECT CategoriaId, CategoriaNome, CategoriaImgUrl, EstaAtivo, DataCriacao
        --FROM Categoria
        --WHERE EstaAtivo = 1
        --ORDER BY CategoriaNome;
        --RETURN;
    END
END

