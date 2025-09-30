using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Admin
{
    public partial class Produto : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt, dt1;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Produto";
            Session["breadCumbPage"] = "Produto";
            if (!IsPostBack)
            {
                getCategorias();
            }
            lblMsg.Visible = false;
        }
        void getCategorias()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Categoria_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            ddlCategoria.DataSource = dt;
            ddlCategoria.DataTextField = "CategoriaNome";
            ddlCategoria.DataValueField = "CategoriaId";
            ddlCategoria.DataBind();
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {         
                getSubCategorias(Convert.ToInt32(ddlCategoria.SelectedValue));             
        }
        void getSubCategorias(int categoriaId)
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SubCategoria_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SUBCATEGORIABYID");
            cmd.Parameters.AddWithValue("@CategoriaId", categoriaId);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            sda.Fill(dt1);
            ddlSubCategoria.Items.Clear();
            ddlSubCategoria.DataSource = dt1;
            ddlSubCategoria.DataTextField = "SubCategoriaNome";
            ddlSubCategoria.DataValueField = "SubCategoriaId";
            ddlSubCategoria.DataBind();
            ddlSubCategoria.Items.Insert(0, "--Selecione a Sub-Categoria--");
        }

    }
}