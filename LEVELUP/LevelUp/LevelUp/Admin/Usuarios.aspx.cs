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

namespace LevelUp.Admin
{
    public partial class Usuarios : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Lista Usuários";
            Session["breadCumbPage"] = "Lista Usuários";

            if (!IsPostBack)
            {
                getUsuarios();
            }
        }

        private void getUsuarios()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Usuario_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4ADMIN");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rUsuarios.DataSource = dt;
            rUsuarios.DataBind();
        }

        protected void rUsuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deletar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Usuario_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@CategoriaId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Usuário deletada com sucesso!.";
                    lblMsg.CssClass = "alert alert-success";
                    getUsuarios();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Erro ao "  + ex.Message;
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