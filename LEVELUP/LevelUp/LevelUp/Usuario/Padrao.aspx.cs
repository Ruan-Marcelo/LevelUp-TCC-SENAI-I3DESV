using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Usuario
{
    public partial class Padrao : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategorias();
            }
        }
        private void getCategorias()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Categoria_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "ACTIVECATEGORIA");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCategoria.DataSource = dt;
            rCategoria.DataBind();
        }
    }
}