using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Usuario
{  
    public partial class Contato : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("ContatoSp", con);
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Assunto", txtAssunto.Text.Trim());
                cmd.Parameters.AddWithValue("@Mensagem", txtMensagem.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                lblMsg.Visible = true;
                lblMsg.Text = "Mensagem enviada com sucesso, obrigado pelo contato!.";
                lblMsg.CssClass = "alert alert-success";
                clear();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert(' " + ex.Message + " ');</script>");
            }
            finally
            {
                con.Close();
            }
        }
        private void clear()
        {
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAssunto.Text = string.Empty;
            txtMensagem.Text = string.Empty;
        }
    }
}