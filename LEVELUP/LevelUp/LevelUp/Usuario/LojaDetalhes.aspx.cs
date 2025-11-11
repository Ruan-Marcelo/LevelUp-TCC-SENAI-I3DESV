//using LevelUp.Admin;
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace LevelUp.Usuario
//{
//    public partial class LojaDetalhes : System.Web.UI.Page
//    {
//        SqlConnection con;
//        SqlCommand cmd;
//        SqlDataAdapter sda;
//        DataTable dt;
//        DataView dv;

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                if (Request.QueryString["id"] != null)
//                {
//                    getProdutoDetalhes();
//                }
//                else
//                {
//                    Response.Redirect("Index.aspx");
//                }

//                if (Request.QueryString["cid"] != null) //por catgeoria
//                {
//                    getProdutosByCategoria();
//                }
//                else if (Request.QueryString["sid"] != null)// por subcategoria
//                {
//                    getProdutosBySubCategoria();
//                }
//                else//todos os propdutos
//                {
//                    getTodosProdutos();
//                }
//            }
//        }

//        void getProdutoDetalhes()
//        {
//            int produtoId = Convert.ToInt32(Request.QueryString["id"]);
//            con = new SqlConnection(Utils.getConnection());
//            cmd = new SqlCommand("Produto_Crud", con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@Action", "GETBYID");
//            cmd.Parameters.AddWithValue("@ProdutoId", produtoId);

//            sda = new SqlDataAdapter(cmd);
//            dt = new DataTable();
//            sda.Fill(dt);
//        }

//        void getTodosProdutos()
//        {
//            try
//            {
//                using (con = new SqlConnection(Utils.getConnection()))
//                {
//                    cmd = new SqlCommand("Produto_Crud", con);
//                    cmd.Parameters.AddWithValue("@Action", "ACTIVEPRODUTO");
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    sda = new SqlDataAdapter(cmd);
//                    dt = new DataTable();
//                    sda.Fill(dt);
//                    if (dt.Rows.Count > 0)
//                    {
//                        rProdutos.DataSource = dt;
//                    }
//                    else
//                    {
//                        rProdutos.DataSource = dt;
//                        rProdutos.FooterTemplate = null;
//                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
//                    }
//                    rProdutos.DataBind();
//                    Session["produto"] = dt;
//                }
//            }
//            catch (Exception e)
//            {
//                Response.Write("<script>alert('" + e.Message + "');</script>");
//            }
//        }

//        void getProdutosByCategoria()
//        {
//            try
//            {
//                using (con = new SqlConnection(Utils.getConnection()))
//                {
//                    int categoriaId = Convert.ToInt32(Request.QueryString["cid"]);
//                    cmd = new SqlCommand("Produto_Crud", con);
//                    cmd.Parameters.AddWithValue("@Action", "PRDTBYCATEGORIA");
//                    cmd.Parameters.AddWithValue("@CategoriaId", categoriaId);
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    sda = new SqlDataAdapter(cmd);
//                    dt = new DataTable();
//                    sda.Fill(dt);
//                    if (dt.Rows.Count > 0)
//                    {
//                        rProdutos.DataSource = dt;
//                    }
//                    else
//                    {
//                        rProdutos.DataSource = dt;
//                        rProdutos.FooterTemplate = null;
//                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
//                    }
//                    rProdutos.DataBind();
//                    Session["produto"] = dt;
//                }
//            }
//            catch (Exception e)
//            {
//                Response.Write("<script>alert('" + e.Message + "');</script>");
//            }
//        }

//        void getProdutosBySubCategoria()
//        {
//            try
//            {
//                using (con = new SqlConnection(Utils.getConnection()))
//                {
//                    int subCategoriaId = Convert.ToInt32(Request.QueryString["sid"]);
//                    cmd = new SqlCommand("Produto_Crud", con);
//                    cmd.Parameters.AddWithValue("@Action", "PRDTBYSUBCATEGORIA");
//                    cmd.Parameters.AddWithValue("@SubCategoriaId", subCategoriaId);
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    sda = new SqlDataAdapter(cmd);
//                    dt = new DataTable();
//                    sda.Fill(dt);
//                    if (dt.Rows.Count > 0)
//                    {
//                        rProdutos.DataSource = dt;
//                    }
//                    else
//                    {
//                        rProdutos.DataSource = dt;
//                        rProdutos.FooterTemplate = null;
//                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
//                    }
//                    rProdutos.DataBind();
//                    Session["produto"] = dt;
//                }
//            }
//            catch (Exception e)
//            {
//                Response.Write("<script>alert('" + e.Message + "');</script>");
//            }
//        }
//        private sealed class CustomTemplate : ITemplate
//        {
//            private ListItemType ListItemType { get; set; }
//            public CustomTemplate(ListItemType listItemType)
//            {
//                ListItemType = listItemType;
//            }

//            public void InstantiateIn(Control container)
//            {
//                if (ListItemType == ListItemType.Footer)
//                {
//                    var footer = new LiteralControl("<b>nenhum produto é exibido.</b>");
//                    container.Controls.Add(footer);
//                }
//            }
//        }

//        protected void btnSearch_Click(object sender, EventArgs e)
//        {
//            dt = (DataTable)Session["produto"];
//            if (dt != null)
//            {
//                if (dt.Rows.Count > 0)
//                {
//                    dv = new DataView(dt);
//                    dv.RowFilter = "ProdutoNome LIKE '" + txtSearchInput.Value.Trim().Replace("'", "''") + "%' ";
//                    if (dv.Count > 0)
//                    {
//                        rProdutos.DataSource = dv;
//                    }
//                    else
//                    {
//                        rProdutos.DataSource = dv;
//                        rProdutos.FooterTemplate = null;
//                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
//                    }
//                }
//                else
//                {
//                    rProdutos.DataSource = dv;
//                    rProdutos.FooterTemplate = null;
//                    rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
//                }
//                rProdutos.DataBind();
//            }
//        }

//        protected void ddlSOrdernarPor_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (ddlSOrdernarPor.SelectedIndex != 0)
//            {
//                dt = (DataTable)Session["produto"];
//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        dv = new DataView(dt);
//                        if (ddlSOrdernarPor.SelectedIndex == 1)
//                        {
//                            dv.Sort = "DataCriacao ASC";
//                        }
//                        else if (ddlSOrdernarPor.SelectedIndex == 2)
//                        {
//                            dv.Sort = "ProdutoNome ASC";
//                        }
//                        else
//                        {
//                            dv.Sort = "Preco ASC";
//                        }

//                        if (dv.Count > 0)
//                        {
//                            rProdutos.DataSource = dv;
//                        }
//                        else
//                        {
//                            rProdutos.DataSource = dv;
//                            rProdutos.FooterTemplate = null;
//                            rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
//                        }
//                        rProdutos.DataBind();
//                    }
//                    else
//                    {
//                        rProdutos.DataSource = dv;
//                        rProdutos.FooterTemplate = null;
//                        rProdutos.FooterTemplate = new CustomTemplate(ListItemType.Footer);
//                    }
//                }
//            }
//        }

//        protected void btnReset_Click(object sender, EventArgs e)
//        {
//            rProdutos.DataSource = null;
//            rProdutos.DataSource = (DataTable)Session["produto"];
//            rProdutos.DataBind();
//            txtSearchInput.Value = string.Empty;
//        }

//        protected void btnOrdernarReset_Click(object sender, EventArgs e)
//        {
//            rProdutos.DataSource = null;
//            rProdutos.DataSource = (DataTable)Session["produto"];
//            rProdutos.DataBind();
//            ddlSOrdernarPor.ClearSelection();
//        }

//    }
//}
using LevelUp.Admin;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Usuario
{
    public partial class LojaDetalhes : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataSet ds;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int produtoId = Convert.ToInt32(Request.QueryString["id"]);
                    CarregarProdutoDetalhes(produtoId);
                }
                else
                {
                    Response.Redirect("Padrao.aspx");
                }
            }
        }

        private void CarregarProdutoDetalhes(int produtoId)
        {
            try
            {
                using (con = new SqlConnection(Utils.getConnection()))
                using (cmd = new SqlCommand("Produto_Crud", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GETPRODUTO_PUBLIC");
                    cmd.Parameters.AddWithValue("@ProdutoId", produtoId);

                    sda = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    sda.Fill(ds);

                    if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Produto não encontrado ou inativo!";
                        lblMsg.CssClass = "alert alert-danger";
                        return;
                    }

                    DataRow produto = ds.Tables[0].Rows[0];

                    lblNome.Text = produto["ProdutoNome"]?.ToString() ?? "";
                    lblPreco.Text = produto["Preco"] != DBNull.Value ? Convert.ToDecimal(produto["Preco"]).ToString("N2") : "0,00";
                    lblDescricaoCurta.Text = produto["DescricaoCurta"]?.ToString() ?? "";
                    lblDescricaoLonga.Text = produto["DescricaoLonga"]?.ToString() ?? "";

                    if (produto.Table.Columns.Contains("AdicionalDescricao") && produto["AdicionalDescricao"] != DBNull.Value && !string.IsNullOrWhiteSpace(produto["AdicionalDescricao"].ToString()))
                    {
                        pnlAdicional.Visible = true;
                        lblAdicional.Text = produto["AdicionalDescricao"].ToString();
                    }
                    else
                    {
                        pnlAdicional.Visible = false;
                    }

                    lblCategoria.Text = produto.Table.Columns.Contains("CategoriaNome") ? produto["CategoriaNome"].ToString() : "";
                    lblEstoque.Text = produto.Table.Columns.Contains("Quantidade") ? produto["Quantidade"].ToString() : "0";


                    if (produto.Table.Columns.Contains("Cor") && produto["Cor"] != DBNull.Value)
                    {
                        string cor = produto["Cor"].ToString();
                        if (!string.IsNullOrEmpty(cor))
                        {
                            pnlCor.Visible = true;
                            foreach (var c in cor.Split(','))
                            {
                                Button btnCor = new Button();
                                btnCor.Text = c.Trim();
                                btnCor.CssClass = "variacao-btn";
                                btnCor.OnClientClick = $"selecionarVariacao('cor', '{c.Trim()}', this); return false;";
                                divCores.Controls.Add(btnCor);
                            }
                        }
                    }

                    if (produto.Table.Columns.Contains("Tamanho") && produto["Tamanho"] != DBNull.Value)
                    {
                        string tamanho = produto["Tamanho"].ToString();
                        if (!string.IsNullOrEmpty(tamanho))
                        {
                            pnlTamanho.Visible = true;
                            foreach (var t in tamanho.Split(','))
                            {
                                Button btnTam = new Button();
                                btnTam.Text = t.Trim();
                                btnTam.CssClass = "variacao-btn";
                                btnTam.OnClientClick = $"selecionarVariacao('tamanho', '{t.Trim()}', this); return false;";
                                divTamanhos.Controls.Add(btnTam);
                            }
                        }
                    }



                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable imagens = ds.Tables[1];

                        foreach (DataRow r in imagens.Rows)
                        {
                            if (r["ImagemUrl"] != DBNull.Value)
                            {
                                string url = r["ImagemUrl"].ToString();
                                if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("~"))
                                {
                                    r["ImagemUrl"] = ResolveUrl("~/Imagem/Produto/" + url);
                                }
                            }
                        }

                        rImagens.DataSource = imagens;
                        rImagens.DataBind();
                    }
                    else
                    {
                        rImagens.DataSource = new DataTable();
                        rImagens.DataBind();
                    }

                    if (produto.Table.Columns.Contains("CategoriaId") && produto["CategoriaId"] != DBNull.Value)
                    {
                        int categoriaId = Convert.ToInt32(produto["CategoriaId"]);
                        CarregarProdutosRelacionados(categoriaId, produtoId);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        private void CarregarProdutosRelacionados(int categoriaId, int produtoAtualId)
        {
            try
            {
                using (con = new SqlConnection(Utils.getConnection()))
                using (cmd = new SqlCommand("Produto_Crud", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GETPRODUTO_PUBLIC");
                    cmd.Parameters.AddWithValue("@ProdutoId", produtoAtualId);

                    sda = new SqlDataAdapter(cmd);
                    DataSet dsRel = new DataSet();
                    sda.Fill(dsRel);

                    if (dsRel.Tables.Count < 3) return;

                    DataTable dtRelacionados = dsRel.Tables[2];

                    // não duplica o prod
                    DataView dv = new DataView(dtRelacionados);
                    dv.RowFilter = $"ProdutoId <> {produtoAtualId}";

                    foreach (DataRowView rowView in dv)
                    {
                        if (rowView["ImagemUrl"] != DBNull.Value)
                        {
                            string url = rowView["ImagemUrl"].ToString();
                            if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                            {
                                rowView["ImagemUrl"] = ResolveUrl("~/Imagem/Produto/" + url);
                            }
                        }
                    }

                    rProdutosRelacionados.DataSource = dv;
                    rProdutosRelacionados.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro relacionados: " + ex.Message);
            }
        }

        protected void lbAdicionarCart_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["usuarioId"] == null)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Você precisa estar logado para adicionar produtos ao carrinho.";
                    lblMsg.CssClass = "alert alert-warning";
                    return;
                }

                int usuarioId = Convert.ToInt32(Session["usuarioId"]);
                int produtoId = Convert.ToInt32(Request.QueryString["id"]);
                int quantidade = Convert.ToInt32(txtQuantidade.Text);

                int quantidadeAtual = isItemExistInCarrinho(produtoId);

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
                            cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                            cmd.ExecuteNonQuery();
                        }
                        lblMsg.Text = "Produto adicionado ao carrinho!";
                        lblMsg.CssClass = "alert alert-success";
                    }
                    else
                    {
                        int novaQuantidade = quantidadeAtual + quantidade;
                        Utils utils = new Utils();
                        bool atualizado = utils.atualizarCarrinhoQuantidade(novaQuantidade, produtoId, usuarioId);

                        lblMsg.Text = atualizado ?
                            "Quantidade atualizada no carrinho!" :
                            "Não foi possível atualizar a quantidade.";

                        lblMsg.CssClass = atualizado ?
                            "alert alert-info" :
                            "alert alert-warning";
                    }

                    lblMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Erro: " + ex.Message;
            }
        }




        protected void rProdutoLista_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbQuantidade = e.Item.FindControl("lblEstoque") as Label;

                if (lbQuantidade != null)
                {
                    int quantidade = 0;
                    int.TryParse(lbQuantidade.Text, out quantidade);

                    if (quantidade <= 5)
                    {
                        lbQuantidade.CssClass = "badge badge-danger";
                        lbQuantidade.ToolTip = "Item prestes a ficar fora de estoque!";
                    }
                    else
                    {
                        lbQuantidade.CssClass = "badge badge-success";
                        lbQuantidade.ToolTip = "Item com bastante estoque!";
                    }
                }
            }
        }

        protected void rProdutos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "addToCart")
            {
                int produtoId = Convert.ToInt32(e.CommandArgument);

                if (Session["usuarioId"] == null)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Você precisa estar logado para adicionar produtos ao carrinho.";
                    lblMsg.CssClass = "alert alert-warning";
                    return;
                }

                int usuarioId = Convert.ToInt32(Session["usuarioId"]);


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
                                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
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
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            using (SqlCommand cmd = new SqlCommand("Carrinho_Crud", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                cmd.Parameters.AddWithValue("@UsuarioId", Session["usuarioId"]);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                    return Convert.ToInt32(dt.Rows[0]["Quantidade"]);
            }
            return 0;
        }


    }
}

