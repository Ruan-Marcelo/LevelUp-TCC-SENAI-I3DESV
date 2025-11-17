using LevelUp.Admin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Usuario
{
    public partial class Blog : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificarLogin();
                CarregarPosts();
                CarregarContagemPosts();
                CarregarCategorias();
                CarregarSugestoes();
            }
        }

        private void CarregarCategorias()
        {
            using (SqlConnection con = new SqlConnection(Utils.getConnection()))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoriaPostId, NomeCategoria FROM CategoriaPost WHERE EstaAtivo = 1", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlCategoria.DataSource = dr;
                ddlCategoria.DataTextField = "NomeCategoria";
                ddlCategoria.DataValueField = "CategoriaPostId";
                ddlCategoria.DataBind();
                dr.Close();
            }
            ddlCategoria.Items.Insert(0, new ListItem("--Selecione a Categoria--", "0"));
        }

        private void CarregarContagemPosts()
        {
            if (Session["usuarioId"] == null) return;
            int usuarioId = Convert.ToInt32(Session["usuarioId"]);

            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SELECT COUNT(*) AS TotalPosts FROM Posts WHERE UsuarioId=@UsuarioId", con);
            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
                lblPosts.Text = dt.Rows[0]["TotalPosts"].ToString();
        }

        private void VerificarLogin()
        {
            if (Session["usuarioId"] != null)
            {
                pnlLogado.Visible = true;
                lblNome.Text = Session["nome"]?.ToString();
                lblNomeUsuario.Text = Session["nomeDeUsuario"]?.ToString();

                string imagem = Session["imagemUrl"]?.ToString();
                string caminho = Server.MapPath("~/Imagem/Usuario/" + imagem);
                imgPerfil.ImageUrl = (!string.IsNullOrEmpty(imagem) && File.Exists(caminho))
                    ? "~/Imagem/Usuario/" + imagem
                    : "~/Imagem/Usuario/usuario-padrao.png";
            }
            else
            {
                pnlLogado.Visible = false;
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string termo = txtPesquisar.Text.Trim();
            int categoriaId = Convert.ToInt32(ddlCategoria.SelectedValue);

            CarregarPosts(termo, categoriaId);
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string termo = txtPesquisar.Text.Trim();
            int categoriaId = Convert.ToInt32(ddlCategoria.SelectedValue);

            CarregarPosts(termo, categoriaId);
        }

        protected void btnCriar_Click(object sender, EventArgs e)
        {
            if (Session["usuarioId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int usuarioId = Convert.ToInt32(Session["usuarioId"]);
            string titulo = txtTitulo.Text.Trim();
            string conteudo = txtConteudo.Text.Trim();
            string imagemUrl = null;

            int categoriaId = Convert.ToInt32(ddlCategoria.SelectedValue);
            if (categoriaId == 0)
            {
                lblMsg.Text = "Selecione uma categoria!";
                return;
            }

            if (FileUpload1.HasFile)
            {
                string folder = Server.MapPath("~/Imagem/Post/");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string ext = Path.GetExtension(FileUpload1.FileName);
                string fileName = $"{Guid.NewGuid()}{ext}";
                string path = Path.Combine(folder, fileName);
                FileUpload1.SaveAs(path);
                imagemUrl = "/Imagem/Post/" + fileName;
            }

            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Post_Crud", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (!string.IsNullOrEmpty(hfEditPostId.Value))
            {
                int postId = Convert.ToInt32(hfEditPostId.Value);
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@PostId", postId);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
            }

            cmd.Parameters.AddWithValue("@Titulo", titulo);
            cmd.Parameters.AddWithValue("@Conteudo", conteudo);
            cmd.Parameters.AddWithValue("@ImagemUrl", (object)imagemUrl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CategoriaPostId", categoriaId);

            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            // Limpar campos
            txtTitulo.Text = "";
            txtConteudo.Text = "";
            ddlCategoria.SelectedIndex = 0;
            hfEditPostId.Value = "";
            imgPreviewPost.Style["display"] = "none";
            btnCriar.Text = "Postar";
            formCriarPost.Style["display"] = "none";

            CarregarPosts();
        }

        private void CarregarPosts(string pesquisa = "", int categoriaId = 0)
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Post_Crud", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (!string.IsNullOrEmpty(pesquisa))
            {
                cmd.Parameters.AddWithValue("@Action", "SEARCH");
                cmd.Parameters.AddWithValue("@Pesquisa", pesquisa);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Action", "GETALL");
            }

            if (categoriaId > 0)
                cmd.Parameters.AddWithValue("@CategoriaPostId", categoriaId);

            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                lblMensagemPesquisa.Text = string.IsNullOrEmpty(pesquisa)
                    ? "Nenhum post encontrado."
                    : $"Nenhum post encontrado para: \"{pesquisa}\"";
                lblMensagemPesquisa.Visible = true;
                rptPosts.DataSource = null;
                rptPosts.DataBind();
                return;
            }
            else
            {
                lblMensagemPesquisa.Visible = false;
            }

            // Adiciona colunas extras
            if (!dt.Columns.Contains("ImagemUrlPost"))
                dt.Columns.Add("ImagemUrlPost", typeof(string));
            if (!dt.Columns.Contains("IsOwner"))
                dt.Columns.Add("IsOwner", typeof(bool));
            if (!dt.Columns.Contains("Curtiu"))
                dt.Columns.Add("Curtiu", typeof(bool));
            if (!dt.Columns.Contains("Curtidas"))
                dt.Columns.Add("Curtidas", typeof(int));

            int? usuarioAtual = Session["usuarioId"] as int?;

            con.Open();
            foreach (DataRow r in dt.Rows)
            {
                if (r["ImagemUrlPost"] == DBNull.Value || string.IsNullOrEmpty(r["ImagemUrlPost"].ToString()))
                    r["ImagemUrlPost"] = "/Imagem/Post/post-padrao.png";

                int dono = Convert.ToInt32(r["UsuarioId"]);
                r["IsOwner"] = usuarioAtual != null && dono == usuarioAtual;

                int postId = Convert.ToInt32(r["PostId"]);

                cmd = new SqlCommand("SELECT COUNT(*) AS TotalCurtidas FROM CurtidaPost WHERE PostId=@PostId", con);
                cmd.Parameters.AddWithValue("@PostId", postId);
                sda = new SqlDataAdapter(cmd);
                DataTable dtCurtidas = new DataTable();
                sda.Fill(dtCurtidas);
                r["Curtidas"] = dtCurtidas.Rows.Count > 0 ? Convert.ToInt32(dtCurtidas.Rows[0]["TotalCurtidas"]) : 0;

                if (usuarioAtual != null)
                {
                    cmd = new SqlCommand("SELECT COUNT(*) AS CurtiuPost FROM CurtidaPost WHERE PostId=@PostId AND UsuarioId=@UsuarioId", con);
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioAtual.Value);
                    sda = new SqlDataAdapter(cmd);
                    DataTable dtCurtiu = new DataTable();
                    sda.Fill(dtCurtiu);
                    r["Curtiu"] = dtCurtiu.Rows.Count > 0 && Convert.ToInt32(dtCurtiu.Rows[0]["CurtiuPost"]) > 0;
                }
                else
                {
                    r["Curtiu"] = false;
                }
            }
            con.Close();

            rptPosts.DataSource = dt;
            rptPosts.DataBind();
        }

        private void CarregarSugestoes()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand(@"
        SELECT TOP 5 
            u.UsuarioId, 
            u.Nome, 
            u.ImagemUrl,
            (SELECT COUNT(*) FROM Seguindo s WHERE s.SeguindoId = u.UsuarioId) AS TotalSeguidores
        FROM Usuarios u
        WHERE u.UsuarioId <> @me", con);
            cmd.Parameters.AddWithValue("@me", Session["usuarioId"] ?? 0);

            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            rptSugestoes.DataSource = dt;
            rptSugestoes.DataBind();
        }

        protected void rptPosts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Session["usuarioId"] == null) return;
            int usuarioId = Convert.ToInt32(Session["usuarioId"]);

            if (e.CommandName == "DeletePost")
            {
                int postId = Convert.ToInt32(e.CommandArgument);

                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Post_Crud", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@PostId", postId);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                CarregarPosts();
            }
        }

        protected void rptPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PlaceHolder ph = (PlaceHolder)e.Item.FindControl("phOwnerButtons");
                DataRowView row = (DataRowView)e.Item.DataItem;

                bool isOwner = Convert.ToBoolean(row["IsOwner"]);
                ph.Visible = isOwner;
            }
        }

        [WebMethod(EnableSession = true)]
        public static object CurtirPost(int postId)
        {
            var usuarioIdObj = System.Web.HttpContext.Current.Session["usuarioId"];
            if (usuarioIdObj == null) return new { success = false, message = "Usuário não logado" };
            int usuarioId = Convert.ToInt32(usuarioIdObj);

            SqlConnection con = new SqlConnection(Utils.getConnection());
            SqlCommand cmd = new SqlCommand("Like_Toggle", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                int total = Convert.ToInt32(dt.Rows[0]["Curtidas"]);
                bool curtiu = Convert.ToBoolean(dt.Rows[0]["Curtiu"]);
                return new { success = true, total, curtiu };
            }

            return new { success = false, message = "Erro ao curtir" };
        }

        protected void lblResgitrarOrPerfil_Click(object sender, EventArgs e)
        {
            if (Session["usuarioId"] != null)
                Response.Redirect("Perfil.aspx");
            else
                Response.Redirect("Registrar.aspx");
        }

        [WebMethod(EnableSession = true)]
        public static object ComentarPost(int postId, string conteudo)
        {
            var usuarioIdObj = System.Web.HttpContext.Current.Session["usuarioId"];
            if (usuarioIdObj == null) return new { redirect = "Login.aspx" };

            int usuarioId = Convert.ToInt32(usuarioIdObj);

            if (string.IsNullOrWhiteSpace(conteudo))
                return new { success = false, message = "Comentário vazio" };

            string connStr = Utils.getConnection();

            string nome = "";
            string nomeUsuario = "";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("Comentario_Insert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@Conteudo", conteudo);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Nome, NomeDeUsuario FROM Usuarios WHERE UsuarioId=@UsuarioId", con))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            nome = dr["Nome"].ToString();
                            nomeUsuario = dr["NomeDeUsuario"].ToString();
                        }
                    }
                }
            }

            return new
            {
                success = true,
                comentario = new
                {
                    nome,
                    nomeUsuario,
                    conteudo,
                    data = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                }
            };
        }
    }  
}
