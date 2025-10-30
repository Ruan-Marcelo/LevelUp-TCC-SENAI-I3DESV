using LevelUp.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Usuario
{
    public partial class Loja : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        Utils utils;
        DataView dv;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["cid"] != null) //por catgeoria
                {
                    getProdutosByCategoria();
                }
                else if (Request.QueryString["sid"] != null)// por subcategoria
                {
                    getProdutosBySubCategoria();
                }
                else//todos os propdutos
                {
                    getTodosProdutos();
                }
            }
        }

        void getTodosProdutos()
        {
            try
            {
                using (con = new SqlConnection(Utils.getConnection()))
                {
                    cmd = new SqlCommand("Produto_Crud", con);
                    cmd.Parameters.AddWithValue("@Action", "ACTIVEPRODUTO");
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rProdutos.DataSource = dt;
                    }
                    else
                    {
                        rProdutos.DataSource = dt;
                        rProdutos.FooterTemplate = null;
                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                    rProdutos.DataBind();
                    Session["produto"] = dt;
                }
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "');</script>");
            }
        }

        void getProdutosByCategoria()
        {
            try
            {
                using (con = new SqlConnection(Utils.getConnection()))
                {
                    int categoriaId = Convert.ToInt32(Request.QueryString["cid"]);
                    cmd = new SqlCommand("Produto_Crud", con);
                    cmd.Parameters.AddWithValue("@Action", "PRDTBYCATEGORIA");
                    cmd.Parameters.AddWithValue("@CategoriaId", categoriaId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rProdutos.DataSource = dt;
                    }
                    else
                    {
                        rProdutos.DataSource = dt;
                        rProdutos.FooterTemplate = null;
                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                    rProdutos.DataBind();
                    Session["produto"] = dt;
                }
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "');</script>");
            }
        }

        void getProdutosBySubCategoria()
        {
            try
            {
                using (con = new SqlConnection(Utils.getConnection()))
                {
                    int subCategoriaId = Convert.ToInt32(Request.QueryString["sid"]);
                    cmd = new SqlCommand("Produto_Crud", con);
                    cmd.Parameters.AddWithValue("@Action", "PRDTBYSUBCATEGORIA");
                    cmd.Parameters.AddWithValue("@SubCategoriaId", subCategoriaId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        rProdutos.DataSource = dt;
                    }
                    else
                    {
                        rProdutos.DataSource = dt;
                        rProdutos.FooterTemplate = null;
                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                    rProdutos.DataBind();
                    Session["produto"] = dt;
                }
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "');</script>");
            }
        }
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            public CustomTemplate(ListItemType listItemType)
            {
                ListItemType = listItemType;
            }

            public void InstantiateIn(Control container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<b>nenhum produto é exibido.</b>");
                    container.Controls.Add(footer);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["produto"];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dv = new DataView(dt);
                    dv.RowFilter = "ProdutoNome LIKE '" + txtSearchInput.Value.Trim().Replace("'", "''") + "%' ";
                    if (dv.Count > 0)
                    {
                        rProdutos.DataSource = dv;
                    }
                    else
                    {
                        rProdutos.DataSource = dv;
                        rProdutos.FooterTemplate = null;
                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                }
                else
                {
                    rProdutos.DataSource = dv;
                    rProdutos.FooterTemplate = null;
                    rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                }
                rProdutos.DataBind();
            }
        }

        protected void ddlSOrdernarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrdernarPor.SelectedIndex != 0)
            {
                dt = (DataTable)Session["produto"];
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dv = new DataView(dt);
                        if (ddlOrdernarPor.SelectedIndex == 1)
                        {
                            dv.Sort = "DataCriacao ASC";
                        }
                        else if (ddlOrdernarPor.SelectedIndex == 2)
                        {
                            dv.Sort = "ProdutoNome ASC";
                        }
                        else
                        {
                            dv.Sort = "Preco ASC";
                        }

                        if (dv.Count > 0)
                        {
                            rProdutos.DataSource = dv;
                        }
                        else
                        {
                            rProdutos.DataSource = dv;
                            rProdutos.FooterTemplate = null;
                            rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                        }
                        rProdutos.DataBind();
                    }
                    else
                    {
                        rProdutos.DataSource = dv;
                        rProdutos.FooterTemplate = null;
                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
                    }
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            rProdutos.DataSource = null;
            rProdutos.DataSource = (DataTable)Session["produto"];
            rProdutos.DataBind();
            txtSearchInput.Value = string.Empty;
        }

        protected void btnOrdernarReset_Click(object sender, EventArgs e)
        {
            rProdutos.DataSource = null;
            rProdutos.DataSource = (DataTable)Session["produto"];
            rProdutos.DataBind();
            ddlOrdernarPor.ClearSelection();
        }

        protected void rProdutos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "addToCart")
            {
                int produtoId = Convert.ToInt32(e.CommandArgument);
               
                // Quando o login estiver pronto, basta descomentar estas linhas:
                //
                // if (Session["USERID"] == null)
                // {
                //     lblMsg.Visible = true;
                //     lblMsg.Text = "Você precisa estar logado para adicionar produtos ao carrinho.";
                //     lblMsg.CssClass = "alert alert-warning";
                //     return;
                // }
                //
                // int usuarioId = Convert.ToInt32(Session["USERID"]);

                //teste com usu fixo chefe
                int usuarioId = 1;

                int quantidadeAtual = isItemExistInCarrinho(produtoId);

                try
                {
                    using (SqlConnection con = new SqlConnection(Utils.getConnection()))
                    {
                        con.Open();

                        if (quantidadeAtual == 0)
                        {
                            using (SqlCommand cmd = new SqlCommand("Carrinho_Crud", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Action", "INSERT");
                                cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                                cmd.Parameters.AddWithValue("@Quantidade", 1);
                                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId); // ID fixo temporário
                                cmd.ExecuteNonQuery();
                            }

                            lblMsg.Visible = true;
                            lblMsg.Text = "Produto adicionado ao carrinho!";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            Utils utils = new Utils();
                            bool atualizado = utils.atualizarCarrinhoQuantidade(quantidadeAtual + 1, produtoId, usuarioId);

                            lblMsg.Visible = true;
                            lblMsg.Text = atualizado
                                ? "Quantidade atualizada no carrinho!"
                                : "Não foi possível atualizar a quantidade.";
                            lblMsg.CssClass = "alert alert-info";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Erro: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
        }

        int isItemExistInCarrinho(int produtoId)
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Carrinho_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
            //cmd.Parameters.AddWithValue("@UsuarioId", Session["USERID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            int quantidade = 0;
            if (dt.Rows.Count > 0)
            {
                quantidade = Convert.ToInt32(dt.Rows[0]["Quantidade"]);
            }
            return quantidade;
        }
        
    }
}