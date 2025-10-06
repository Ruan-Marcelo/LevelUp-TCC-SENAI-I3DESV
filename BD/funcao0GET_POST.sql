CREATE FUNCTION GET_POS
(
	@ProdutoID INT,
	@pos INT
)
RETURNS INT
AS
BEGIN
	RETURN
	(
		SELECT ImagemId FROM
		(
			SELECT ImagemId,ProdutoId,ROW_NUMBER() OVER(PARTITION BY ProdutoId ORDER BY ImagemId) pos
 			FROM ProdutoImg
		) z WHERE ProdutoId = @ProdutoID AND pos =
		(
			SELECT CASE WHEN MAX(pos) >= @pos THEN @pos
			ELSE 1 
			END abc
		FROM 
		(
			SELECT ImagemId,ProdutoId,ROW_NUMBER() OVER(PARTITION BY ProdutoId ORDER BY ImagemId) pos
 			FROM ProdutoImg
		)H 
	  )
	)
END;
