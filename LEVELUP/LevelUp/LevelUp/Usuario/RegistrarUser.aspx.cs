using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Usuario
{
    public partial class RegistrarUser : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnRegistrarOuAtualizar_Click(object sender, EventArgs e)
        {
            string actionNome = string.Empty, imagemCaminho = string.Empty, arquivoExtencao = string.Empty;
            bool isValidacaoExecucao = false;
            int usuarioId = Convert.ToInt32(Request.QueryString["id"]);
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Usuario_Crud", con);
            cmd.Parameters.AddWithValue("@Action", usuarioId);
            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
            cmd.Parameters.AddWithValue("@Nome", txtNome.Text.Trim());
            cmd.Parameters.AddWithValue("@NomeDeUsuario", txtNome.Text.Trim());
            cmd.Parameters.AddWithValue("@Celular", txtCelular.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Senha", txtSenha.Text.Trim());
            cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text.Trim());
            cmd.Parameters.AddWithValue("@CodigoPostal", txtCodigoPostal.Text.Trim());
            if (fuImgUsuario.HasFile)
            {
                if (Utils.isValidExtension(fuImgUsuario.FileName))
                {
                    string novoImagemNome = Utils.getUniqueId();
                    arquivoExtencao = Path.GetExtension(fuImgUsuario.FileName);
                    imagemCaminho = "Imagem/Usuario/" + novoImagemNome.ToString() + arquivoExtencao;
                    fuImgUsuario.PostedFile.SaveAs(Server.MapPath("~/Imagem/Usuario/") + novoImagemNome.ToString() + arquivoExtencao);
                    cmd.Parameters.AddWithValue("@ImgUrl", imagemCaminho);
                    isValidacaoExecucao = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Por favor, selecione uma imagem com extensão válida (.jpg, .jpeg, .png)";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            else
            {
                isValidacaoExecucao = true;
            }
            if (isValidacaoExecucao)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionNome = usuarioId == 0 ? 
                        "Registrado com sucesso! <b><a href='Login.aspx'>Clique aqui</a></b> para o login" :
                        "Atualização de detalhes! bem-sucedida <b><a href='Login.aspx'>Verifique aqui</a></b>";
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Ocorreu um erro: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            
        }
    }
}