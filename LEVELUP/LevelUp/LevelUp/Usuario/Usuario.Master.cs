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
    public partial class Usuario : System.Web.UI.MasterPage
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsoluteUri.ToString().Contains("Padrao.aspx"))
            {
                Control slideUsuarioControle = (Control)Page.LoadControl("SlideUsuarioControle.ascx");
                pnlSliderUC.Controls.Add(slideUsuarioControle);
            }

            if (!IsPostBack)
            {
                getNestedCategorias();
                AtualizarBadgeCarrinho();
                VerificarLogin();
            }
        }

        private void VerificarLogin()
        {
            if (Session["usuarioId"] != null)
            {
                pnlNaoLogado.Visible = false;
                pnlLogado.Visible = true;

                lblNomeUsuario.Text = Session["nomeDeUsuario"]?.ToString();

                string imagem = Session["imagemUrl"]?.ToString();
                string caminhoFisico = Server.MapPath("~/Imagem/Usuario/" + imagem);

                if (!string.IsNullOrEmpty(imagem) && System.IO.File.Exists(caminhoFisico))
                    imgPerfil.ImageUrl = "~/Imagem/Usuario/" + imagem;
                else
                    imgPerfil.ImageUrl = "~/Imagem/Usuario/usuario-padrao.png";
             }
            else
            {
                pnlNaoLogado.Visible = true;
                pnlLogado.Visible = false;
            }
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }


        private void AtualizarBadgeCarrinho()
        {
            int usuarioId = 1;
            int totalItens = 0;

            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Carrinho_Crud", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "TOTAL");
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    con.Open();
                    object result = cmd.ExecuteScalar();
                    totalItens = result != null ? Convert.ToInt32(result) : 0;
                }
            }

            lblCarrinhoCount.Text = totalItens.ToString();
        }

        private void getNestedCategorias()
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

        protected void rCategoria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField categoriaId = e.Item.FindControl("hfCategoriaId") as HiddenField;
                Repeater repsubCategoriaId = e.Item.FindControl("rSubcategoria") as Repeater;
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("SubCategoria_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "ACTIVEBYID");
                cmd.Parameters.AddWithValue("@CategoriaId", Convert.ToInt32(categoriaId.Value));
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    repsubCategoriaId.DataSource = dt;
                    repsubCategoriaId.DataBind();
                }
            }

        }

        protected void lblLoginOuLogout_Click(object sender, EventArgs e)
        {
            if (Session["usuarioId"] != null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }
    }
}