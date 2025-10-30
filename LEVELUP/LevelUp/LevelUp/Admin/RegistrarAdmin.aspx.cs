using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Admin
{
    public partial class RegistrarAdmin : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Registrar Usuário";
            Session["breadCumbPage"] = "Registrar Usuário";
            if (!IsPostBack)
            {

            }
        }

        protected void btnRegistrarOuAtualizar_Click(object sender, EventArgs e)
        {
            string actionNome = string.Empty, imagemCaminho = string.Empty, arquivoExtensao = string.Empty;
            bool isValidacaoExecucao = false;

            int usuarioId = 0;
            if (Request.QueryString["id"] != null)
                int.TryParse(Request.QueryString["id"], out usuarioId);

            string action = usuarioId == 0 ? "INSERT" : "UPDATE";

            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Usuario_Crud", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", action);
            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
            cmd.Parameters.AddWithValue("@Nome", txtNome.Text.Trim());
            cmd.Parameters.AddWithValue("@NomeDeUsuario", txtNomeUser.Text.Trim());
            cmd.Parameters.AddWithValue("@Celular", txtCelular.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Senha", txtSenha.Text.Trim());
            cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text.Trim());
            cmd.Parameters.AddWithValue("@CodigoPostal", txtCodigoPostal.Text.Trim());

            cmd.Parameters.AddWithValue("@NivelId", 1);

            if (fuimgAdmin.HasFile)
            {
                if (Utils.isValidExtension(fuimgAdmin.FileName))
                {
                    string novoImagemNome = Utils.getUniqueId();
                    arquivoExtensao = Path.GetExtension(fuimgAdmin.FileName);
                    imagemCaminho = "Imagem/Usuario/" + novoImagemNome + arquivoExtensao;

                    string pastaUsuario = Server.MapPath("~/Imagem/Usuario/");
                    if (!Directory.Exists(pastaUsuario))
                        Directory.CreateDirectory(pastaUsuario);

                    fuimgAdmin.PostedFile.SaveAs(pastaUsuario + novoImagemNome + arquivoExtensao);
                    cmd.Parameters.AddWithValue("@ImagemUrl", imagemCaminho);
                    isValidacaoExecucao = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Por favor, selecione uma imagem válida (.jpg, .jpeg, .png)";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            else
            {
                isValidacaoExecucao = true;
            }

            if (isValidacaoExecucao)
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    actionNome = usuarioId == 0
                        ? "Registrado com sucesso! <b><a href='Login.aspx'>Clique aqui</a></b> para fazer login."
                        : "Atualização bem-sucedida! <b><a href='Perfil.aspx'>Verifique aqui</a></b>";

                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtNomeUser.Text.Trim() + "</b> " + actionNome;
                    lblMsg.CssClass = "alert alert-success";

                    if (usuarioId != 0)
                        Response.AddHeader("REFRESH", "3;URL=Perfil.aspx");

                    clear();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("UNIQUE KEY"))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtNomeUser.Text.Trim() + "</b> já existe. Escolha outro nome de usuário ou e-mail.";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Erro: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        void clear()
        {
            txtNome.Text = string.Empty;
            txtNomeUser.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtCodigoPostal.Text = string.Empty;
        }
    }
}