using LevelUp.Usuario;
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
    public partial class Contato : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Contato";
            Session["breadCumbPage"] = "Contato";

            if (!IsPostBack)
            {
                //não deixa entrar na parte de adm sem estar logado com login de adm
                Session["breadCrum"] = "Contato dos Usuário";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../Usuario/Login.aspx");
                }
                else
                {
                    getContatos();
                }
            }
        }

        private void getContatos()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("ContatoSp", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rContato.DataSource = dt;
            rContato.DataBind();
        }

        protected void rContato_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "deletar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("ContatoSp", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ContatoId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Contato deletada com sucesso!.";
                    lblMsg.CssClass = "alert alert-success";
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Erro ao " + ex.Message;
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