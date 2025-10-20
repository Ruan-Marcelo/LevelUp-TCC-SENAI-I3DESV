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
                getTodosProdutos();
            }
        }

        void getTodosProdutos()
        {
            try
            {
                using (con = new SqlConnection(Utils.getConnection()))
                {
                    con.Open();
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

        //Class de modelo personalizada para adicionar controles ao item de cabeçalho do repetidor e à seção de rodapé
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
            if (ddlSOrdernarPor.SelectedIndex != 0)
            {
                dt = (DataTable)Session["produto"];
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dv = new DataView(dt);
                        if (ddlSOrdernarPor.SelectedIndex == 1)
                        {
                            dv.Sort = "DataCriacao ASC";
                        }
                        else if (ddlSOrdernarPor.SelectedIndex == 2)
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
            ddlSOrdernarPor.ClearSelection();
        }
    }
}