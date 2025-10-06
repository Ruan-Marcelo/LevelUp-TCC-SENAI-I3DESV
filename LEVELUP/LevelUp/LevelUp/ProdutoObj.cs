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
                try
                {
                   var produtoImagem = produtoBO.ProdutosImagens;
                    #region INSERT Produto
                    con.Open();
                    transaction = con.BeginTransaction();

                    cmd = new SqlCommand("Produto_Crud", con, transaction);
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@ProdutoNome", produtoBO.ProdutoNome);
                    cmd.Parameters.AddWithValue("@DescricaoCurta", produtoBO.DescricaoCurta);
                    cmd.Parameters.AddWithValue("@DescricaoLonga", produtoBO.DescricaoLonga);
                    cmd.Parameters.AddWithValue("@AdicionalDescricao", produtoBO.AdicionalDescricao);
                    cmd.Parameters.AddWithValue("@Preco", produtoBO.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade", produtoBO.Quantidade);
                    cmd.Parameters.AddWithValue("@Tamanho", produtoBO.Tamanho);
                    cmd.Parameters.AddWithValue("@NomeEmpresa", produtoBO.NomeEmpresa);
                    cmd.Parameters.AddWithValue("@CategoriaId", produtoBO.CategoriaId);
                    cmd.Parameters.AddWithValue("@SubCategoriaId", produtoBO.SubCategoriaId);
                    cmd.Parameters.AddWithValue("@Personalizado", produtoBO.Personalizado);
                    cmd.Parameters.AddWithValue("@EstaAtivo", produtoBO.EstaAtivo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    if (produtoId == 0)
                    {
                        cmd = new SqlCommand("Produto_Crud", con, transaction);
                        cmd.Parameters.AddWithValue("@Action", "RECENT_PRODUTO");
                        cmd.CommandType = CommandType.StoredProcedure;
                        sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            produtoId = (int)sdr["ProdutoId"];
                        }
                        sdr.Close(); 
                    }
                    #endregion

                    #region salvar imagem
                    if (produtoId > 0)
                    {
                        foreach (var imagem in produtoImagem)
                        {
                            cmd = new SqlCommand("Produto_Crud", con, transaction);
                            cmd.Parameters.AddWithValue("@Action", "INSERT_PROD_IMG");
                            cmd.Parameters.AddWithValue("@ImagemUrl", imagem.ImagemUrl);
                            cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                            cmd.Parameters.AddWithValue("@ImagemPadrao", imagem.ImagemPadrao);
                            cmd.CommandType = CommandType.StoredProcedure;
                            sdr = cmd.ExecuteReader();
                            result = 1;
                        }
                    }
                    #endregion

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        result = 0;
                    }
                    catch(Exception e)
                    {
                        throw;
                    }
                }               
            }
            return result;
        }
    }
}