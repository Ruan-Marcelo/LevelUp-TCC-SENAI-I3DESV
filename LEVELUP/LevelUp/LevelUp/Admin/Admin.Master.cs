using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarPerfilUsuario();
            }
        }

        private void CarregarPerfilUsuario()
        {
            string nome = Session["nomeDeUsuario"]?.ToString() ?? "Administrador";
            string imagem = Session["imagemUrl"]?.ToString();

            string caminhoImagem = "~/Imagem/Usuario/usuario-padrao.png"; // padrão
            if (!string.IsNullOrEmpty(imagem))
            {
                string fisico = Server.MapPath("~/Imagem/Usuario/" + imagem);
                if (System.IO.File.Exists(fisico))
                {
                    caminhoImagem = "~/Imagem/Usuario/" + imagem;
                }
            }

            lblNomeUsuario.Text = nome;
            imgUsuario.ImageUrl = caminhoImagem;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../Usuario/Padrao.aspx");
        }

    }
}