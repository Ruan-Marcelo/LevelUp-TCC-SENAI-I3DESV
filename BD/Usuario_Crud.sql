SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE Usuario_Crud
	@Action VARCHAR(20),
	@UsuarioId INT = NULL,
	@Nome VARCHAR(50) = NULL,
	@NomeDeUsuario VARCHAR(50) = NULL,
	@Celular VARCHAR(50) = NULL,
	@Email VARCHAR(50) = NULL,
	@Endereco VARCHAR(MAX) = NULL,
	@CodigoPostal VARCHAR(50) = NULL,
	@Senha VARCHAR(50) = NULL,
	@ImagemUrl VARCHAR(MAX) = NULL,
	@NivelId INT = NULL
AS
BEGIN

	SET NOCOUNT ON;
   
   IF @Action = 'SELECT4LOGIN'
   BEGIN
		SELECT * FROM Usuarios WHERE NomeDeUsuario = @NomeDeUsuario AND Senha = @Senha
   END

   IF @Action = 'SELECT4PERFIL'
   BEGIN
		SELECT * FROM Usuarios WHERE UsuarioId = @UsuarioId
   END
   
   IF @Action = 'INSERT'
   BEGIN
		INSERT INTO Usuarios (Nome, NomeDeUsuario, Celular, Email, Endereco, CodigoPostal,Senha,ImagemUrl,NivelId, DataCriacao)
		VALUES (@Nome, @NomeDeUsuario, @Celular, @Email, @Endereco, @CodigoPostal,@Senha,@ImagemUrl,@NivelId, GETDATE())
   END
   
   IF @Action = 'UPDATE'
   BEGIN
		DECLARE @UPDATE_IMAGEM VARCHAR(20) 
		SELECT @UPDATE_IMAGEM = (CASE WHEN @ImagemUrl IS NULL THEN 'NO' ELSE 'YES' END)
		IF @UPDATE_IMAGEM = 'NO'
			BEGIN
				UPDATE Usuarios
				SET Nome = @Nome, NomeDeUsuario =@NomeDeUsuario, Celular = @Celular, Email = @Email, CodigoPostal = @CodigoPostal, Senha = @Senha
				WHERE UsuarioId = @UsuarioId
		    END
		ELSE
			BEGIN
				UPDATE Usuarios
				SET Nome = @Nome, NomeDeUsuario =@NomeDeUsuario, Celular = @Celular, Email = @Email, CodigoPostal = @CodigoPostal, ImagemUrl = @ImagemUrl, Senha = @Senha
				WHERE UsuarioId = @UsuarioId
			END	
   END

   IF @Action = 'SELECT4ADMIN'
   BEGIN 
		SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS [SrNo], UsuarioId, Nome,
		NomeDeUsuario, Email, DataCriacao
		FROM Usuarios WHERE NivelId = 2
	END

	IF @Action = 'DELETE'
	BEGIN
		DELETE FROM Usuarios WHERE UsuarioId = @UsuarioId
	END

	IF @Action = 'GETBYNOMEDEUSUARIO'
	BEGIN
		SELECT Email, Senha FROM Usuarios WHERE NomeDeUsuario = @NomeDeUsuario
	END

END
GO
