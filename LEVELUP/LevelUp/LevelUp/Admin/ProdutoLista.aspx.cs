using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Admin
{
    public partial class ProdutoLista : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        ProdutoDAL produtoDAL;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Lista de Produto";
            Session["breadCumbPage"] = "Lista de Produto";
            if (!IsPostBack)
            {
                CarregarProdutos();             
            }
            lblMsg.Visible = false;
        }

        private void CarregarProdutos()
        {
           produtoDAL = new ProdutoDAL();
           dt = new DataTable();
           dt = produtoDAL.ProdutoComImgPadrao();
           rProdutoLista.DataSource = dt;
           rProdutoLista.DataBind();
        }

        protected void rProdutoLista_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            lblMsg.Visible = false;

            if (e.CommandName == "editar")
            {
                Response.Redirect("Produto.aspx?id=" + e.CommandArgument);
            }
            else if (e.CommandName == "deletar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Produto_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ProdutoId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Produto deletado com sucesso.";
                    lblMsg.CssClass = "alert alert-success";    
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }

        }
    }
}