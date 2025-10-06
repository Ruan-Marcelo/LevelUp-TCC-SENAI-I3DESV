using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LevelUp
{
    public class ProdutoObj
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoLonga { get; set; }
        public string AdicionalDescricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Tamanho { get; set; }
        public string Cor { get; set; }
        public string NomeEmpresa { get; set; }
        //public string Tags { get; set; }
        public int CategoriaId { get; set; }
        public int SubCategoriaId { get; set; }
        public bool Personalizado { get; set; }
        public bool EstaAtivo { get; set; }
        public DateTime DataCriacao { get; set; }

        public List<ProdutoImgObj> ProdutosImagens { get; set; } = new List<ProdutoImgObj>();
        public int PosicaoPadraoImg { get; set; }
    }

    public class ProdutoImgObj
    {
        public int ImagemId { get; set; }
        public int ProdutoId { get; set; }
        public string ImagemUrl { get; set; }
        public bool ImagemPadrao { get; set; }
    }

    public class ProdutoDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        SqlDataAdapter sda;
        DataTable dt;
        SqlTransaction transaction = null;

        public int AddUpdateProduto(ProdutoObj produtoBO)
        {
            int result = 0;
            int produtoId = 0;
            using (con = new SqlConnection(Utils.getConnection()))
            {
                SqlTransaction transaction = null;
                try
                {
                    var produtoImagem = produtoBO.ProdutosImagens;

                    con.Open();
                    transaction = con.BeginTransaction();

                    cmd = new SqlCommand("Produto_Crud", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@ProdutoNome", (object)produtoBO.ProdutoNome ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DescricaoCurta", (object)produtoBO.DescricaoCurta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DescricaoLonga", (object)produtoBO.DescricaoLonga ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AdicionalDescricao", (object)produtoBO.AdicionalDescricao ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Preco", produtoBO.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade", produtoBO.Quantidade);
                    cmd.Parameters.AddWithValue("@Tamanho", (object)produtoBO.Tamanho ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cor",
                                                 string.IsNullOrWhiteSpace(produtoBO.Cor) ? (object)DBNull.Value : produtoBO.Cor);
                    cmd.Parameters.AddWithValue("@NomeEmpresa", (object)produtoBO.NomeEmpresa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CategoriaId", produtoBO.CategoriaId);
                    cmd.Parameters.AddWithValue("@SubCategoriaId", produtoBO.SubCategoriaId);
                    cmd.Parameters.AddWithValue("@Personalizado", produtoBO.Personalizado);
                    cmd.Parameters.AddWithValue("@EstaAtivo", produtoBO.EstaAtivo);

                    object scalar = cmd.ExecuteScalar();
                    if (scalar != null && int.TryParse(scalar.ToString(), out int newId))
                    {
                        produtoId = newId;
                    }

                    if (produtoId > 0 && produtoImagem != null && produtoImagem.Count > 0)
                    {
                        foreach (var imagem in produtoImagem)
                        {
                            cmd = new SqlCommand("Produto_Crud", con, transaction);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Action", "INSERT_PROD_IMG");
                            cmd.Parameters.AddWithValue("@ImagemUrl", (object)imagem.ImagemUrl ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                            cmd.Parameters.AddWithValue("@ImagemPadrao", imagem.ImagemPadrao);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    result = 1;
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction?.Rollback();
                        throw new Exception("Erro ao salvar produto: " + ex.Message, ex);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao desfazer transação: " + e.Message, e);
                    }
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
            return result;
        }
    }
}
