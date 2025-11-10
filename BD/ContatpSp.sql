
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE ContatoSp
	@Action VARCHAR(10),
	@ContatoId INT = NULL,
	@Nome VARCHAR(50) = NULL,
	@Email VARCHAR(50) = NULL,
	@Assunto VARCHAR(200) = NULL,
	@Mensagem VARCHAR(MAX) = NULL
AS
BEGIN

	SET NOCOUNT ON;

	IF @Action = 'INSERT'
	BEGIN
		INSERT INTO dbo.Contato(Nome, Email,Assunto,Mensagem,DataCriacao)
		VALUES(@Nome,@Email,@Assunto,@Mensagem,GETDATE())
    END

	IF @Action = 'SELECT'
	BEGIN
		SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS [SrNo],* FROM dbo.Contato
    END

	IF @Action = 'DELETE'
	BEGIN
		DELETE FROM dbo.Contato WHERE ContatoId = @ContatoId
    END

  
END
GO
