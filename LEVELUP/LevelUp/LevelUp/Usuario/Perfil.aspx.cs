using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace LevelUp.Usuario
{
    public partial class Perfil : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    getUsuarioDetalhes();
                }
            }
        }

        void getUsuarioDetalhes()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Usuario_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4PERFIL");
            cmd.Parameters.AddWithValue("@UsuarioId", Session["usuarioId"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rUsuarioPerfil.DataSource = dt;
            rUsuarioPerfil.DataBind();
            if (dt.Rows.Count == 1)
            {
                Session["nome"] = dt.Rows[0]["Nome"].ToString();
                Session["email"] = dt.Rows[0]["Email"].ToString();
                Session["imagemUrl"] = dt.Rows[0]["ImagemUrl"].ToString();
                Session["dataCriacao"] = dt.Rows[0]["DataCriacao"].ToString();
               
            }
        }

    }
}
