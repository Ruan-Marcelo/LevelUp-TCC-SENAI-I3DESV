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
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuarioId"] != null)
            {
                Response.Redirect("Padrao.aspx");
            }
        }

        protected void BtnLogar_Click(object sender, EventArgs e)
        {
            if(txtNomedeUsuario.Text.Trim() == "Admin" && txtSenha.Text.Trim() == "123")
            {
                Session["admin"] = txtNomedeUsuario.Text.Trim();

                Session["nomeDeUsuario"] = txtNomedeUsuario.Text.Trim();

                Session["imagemUrl"] = "admin-padrao.png";

                Response.Redirect("../Admin/Dashboard.aspx");
            }
            else
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Usuario_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
                cmd.Parameters.AddWithValue("@NomeDeUsuario", txtNomedeUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@Senha", txtSenha.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    Session["nomeDeUsuario"] = txtNomedeUsuario.Text.Trim();
                    Session["usuarioId"] = dt.Rows[0]["UsuarioId"];

                    string caminhoCompleto = dt.Rows[0]["ImagemUrl"].ToString();
                    string nomeArquivo = System.IO.Path.GetFileName(caminhoCompleto);
                    Session["imagemUrl"] = nomeArquivo;
                    Response.Redirect("Padrao.aspx");
                }

                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Credenciais Invalidas..!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
        }
    }
}