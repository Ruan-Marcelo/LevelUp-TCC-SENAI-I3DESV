using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
            string tipo = "insert";
            using (con = new SqlConnection(Utils.getConnection()))
            {
                SqlTransaction transaction = null;
                try
                {
                    var produtoImagem = produtoBO.ProdutosImagens;

                    con.Open();
                    transaction = con.BeginTransaction();
                    produtoId = produtoBO.ProdutoId;

                    cmd = new SqlCommand("Produto_Crud", con, transaction);                    
                    cmd.Parameters.AddWithValue("@Action", produtoId == 0 ? "INSERT" : "UPDATE");
                    cmd.Parameters.AddWithValue("@ProdutoNome",produtoBO.ProdutoNome);
                    cmd.Parameters.AddWithValue("@DescricaoCurta",produtoBO.DescricaoCurta);
                    cmd.Parameters.AddWithValue("@DescricaoLonga",produtoBO.DescricaoLonga);
                    cmd.Parameters.AddWithValue("@AdicionalDescricao", produtoBO.AdicionalDescricao);
                    cmd.Parameters.AddWithValue("@Preco", produtoBO.Preco);
                    cmd.Parameters.AddWithValue("@Quantidade", produtoBO.Quantidade);
                    cmd.Parameters.AddWithValue("@Tamanho", produtoBO.Tamanho);
                    cmd.Parameters.AddWithValue("@Cor",produtoBO.Cor);
                    cmd.Parameters.AddWithValue("@NomeEmpresa",produtoBO.NomeEmpresa);
                    cmd.Parameters.AddWithValue("@CategoriaId", produtoBO.CategoriaId);
                    cmd.Parameters.AddWithValue("@SubCategoriaId", produtoBO.SubCategoriaId);
                    cmd.Parameters.AddWithValue("@Personalizado", produtoBO.Personalizado);
                    cmd.Parameters.AddWithValue("@EstaAtivo", produtoBO.EstaAtivo);
                    if (produtoId > 0)
                    {
                        cmd.Parameters.AddWithValue("@ProdutoId", produtoBO.ProdutoId);
                        tipo = "update";
                    }

                    cmd.CommandType = CommandType.StoredProcedure;

                    object scalar = cmd.ExecuteScalar();
                    if (scalar != null && int.TryParse(scalar.ToString(), out int newId))
                    {
                        produtoId = newId;
                    }

                    if (produtoId > 0 && produtoImagem != null && produtoImagem.Count > 0)
                    {
                        if (tipo == "insert")
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
                                result = 1;
                            } 
                        }
                        else //atuzalçação imgs
                        {
                            bool verdadeiro = false;
                            if (produtoImagem.Count != 0)
                            {
                                cmd = new SqlCommand("Produto_Crud", con, transaction);                               
                                cmd.Parameters.AddWithValue("@Action", "DELETE_PROD_IMG");    
                                cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                verdadeiro = true;
                            }
                            else
                            {
                                int padraoIdImgPos = produtoBO.PosicaoPadraoImg;
                                if (padraoIdImgPos > 0)
                                {
                                    cmd = new SqlCommand("Produto_Crud", con, transaction);
                                    cmd.Parameters.AddWithValue("@Action", "UPDATE_IMG_POS");
                                    cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                                    cmd.Parameters.AddWithValue("@ImagemPadrao", padraoIdImgPos);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.ExecuteNonQuery();
                                    result = 1;
                                }
                                else
                                {
                                    result = 1;
                                }
                            }
                            if (verdadeiro)
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
                                    result = 1;
                                }
                            }
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

        public DataTable ProdutoComImgs(int produtoId)
        {
            try
            {
                DataTable dt = ProdutoById(produtoId);
                dt.Columns.Add("Imagem2");
                dt.Columns.Add("Imagem3");
                dt.Columns.Add("Imagem4");
                dt.Columns.Add("ImagemPadrao");
                DataRow dr = dt.NewRow();
                string imagens = dt.Rows[0]["Imagem1"].ToString();
                string[]  imgArr = imagens.Split(';');
                string imag;
                int rb = 0;
                foreach (string img in imgArr)
                {
                    imag = img.Substring(img.IndexOf(": ") + 1);
                    if (imag.Trim() == "1")
                    {
                        break;
                    }
                    else
                    {
                        rb++;
                    }
                }

                foreach (DataRow dataRow in dt.Rows)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        dataRow["imagem" + (i + 1)] = imgArr[i].Trim();
                    }
                    dataRow["ImagemPadrao"] = rb;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable ProdutoById(int pId)
        {
            try
            {
                using(con = new SqlConnection(Utils.getConnection()))
                {
                    con.Open();
                    cmd = new SqlCommand("Produto_Crud", con);
                    cmd.Parameters.AddWithValue("@Action", "GETBYID");
                    cmd.Parameters.AddWithValue("@ProdutoId", pId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ProdutoComImgPadrao()
        {
            try
            {
                using (con = new SqlConnection(Utils.getConnection()))
                {
                    con.Open();
                    cmd = new SqlCommand("Produto_Crud", con);
                    cmd.Parameters.AddWithValue("@Action", "SELECT");
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
