using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace LevelUp.Usuario
{
    public partial class Carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCarrinho();
            }
        }

        private void CarregarCarrinho()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.getConnection()))
                {
                    int usuarioId = 1; // teste temporário

                    string sql = @"
                SELECT c.ProdutoId, p.ProdutoNome, p.Preco, c.Quantidade AS Qtd,
                       ISNULL(pimg.ImagemUrl, '../Imagem/No_image.png') AS ImagemUrl
                FROM Carrinho c
                INNER JOIN Produto p ON c.ProdutoId = p.ProdutoId
                LEFT JOIN ProdutoImg pimg ON p.ProdutoId = pimg.ProdutoId AND pimg.ImagemPadrao = 1
                WHERE c.UsuarioId = @UsuarioId";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        rCarrinho.DataSource = dt;
                        rCarrinho.DataBind();
                    }
                    else
                    {
                        lblMsg.Text = "Seu carrinho está vazio!";
                        lblMsg.CssClass = "alert alert-info";
                        lblMsg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao carregar o carrinho: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }



        protected void rCarrinho_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int produtoId = Convert.ToInt32(e.CommandArgument);
            int usuarioId = 1; 

            switch (e.CommandName)
            {
                case "Increase":
                    AlterarQuantidade(produtoId, usuarioId, +1);
                    break;

                case "Decrease":
                    AlterarQuantidade(produtoId, usuarioId, -1);
                    break;

                case "Remove":
                    ExecutarCrud("DELETE", produtoId, 0, usuarioId);
                    break;
            }

            CarregarCarrinho();
        }

        private void AlterarQuantidade(int produtoId, int usuarioId, int delta)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.getConnection()))
                {
                    con.Open();

                    // 🔹 Buscar quantidade atual
                    SqlCommand getCmd = new SqlCommand("Carrinho_Crud", con);
                    getCmd.CommandType = CommandType.StoredProcedure;
                    getCmd.Parameters.AddWithValue("@Action", "GETBYID");
                    getCmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                    getCmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    SqlDataAdapter sda = new SqlDataAdapter(getCmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        int qtdAtual = Convert.ToInt32(dt.Rows[0]["Quantidade"]);
                        int novaQtd = qtdAtual + delta;

                        if (novaQtd <= 0)
                        {
                            ExecutarCrud("DELETE", produtoId, 0, usuarioId);
                        }
                        else
                        {
                            ExecutarCrud("UPDATE", produtoId, novaQtd, usuarioId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao alterar quantidade: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        private void ExecutarCrud(string acao, int produtoId, int quantidade, int usuarioId)
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Carrinho_Crud", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", acao);
                cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                if (acao == "UPDATE" || acao == "INSERT")
                    cmd.Parameters.AddWithValue("@Quantidade", quantidade);

                cmd.ExecuteNonQuery();
            }
        }
        public int GetTotalItensCarrinho(int usuarioId)
        {
            int total = 0;
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Carrinho_Crud", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "TOTAL");
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    con.Open();
                    object result = cmd.ExecuteScalar();
                    total = result != null ? Convert.ToInt32(result) : 0;
                }
            }
            return total;
        }


    }
}
