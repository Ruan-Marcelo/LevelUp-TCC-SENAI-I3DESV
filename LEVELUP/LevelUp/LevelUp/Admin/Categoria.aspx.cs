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
    public partial class Categoria : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlConnection sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;

        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionNome = string.Empty, imagemPath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int categioriaId   = Convert.ToInt32(hfCategoriaId.Value);
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Categoria_Crud", con);
            cmd.Parameters.AddWithValue("@Action", categioriaId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@CategoriaId", categioriaId);
            cmd.Parameters.AddWithValue("@CategoriaNome", txtCategoriaNome.Text.Trim());
            cmd.Parameters.AddWithValue("@EstaAtivo", cbIsActive.Checked);
            if (fuCategoriaImagem.HasFile)
            {
                if(Utils.isValidExtension(fuCategoriaImagem.FileName))
                {
                    string newImagemNome = Utils.getUniqueId();
                    fileExtension = Path.GetExtension(fuCategoriaImagem.FileName);
                    imagemPath = "Imagem/Categoria/" + newImagemNome.ToString() + fileExtension;
                    fuCategoriaImagem.PostedFile.SaveAs(Server.MapPath("~/Imagem/Categoria/") + newImagemNome.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@CategotiaImgUrl ", imagemPath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = false;
                    lblMsg.Text = "Apenas arquivos .jpg, .jpeg e .png são permitidos.";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute = false;

                }
            }
            else
            {
                isValidToExecute = true;
            }
            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionNome = categioriaId == 0 ? "adicionada" : "atualizada";
                    lblMsg.Visible = true;
                    lblMsg.Text = "Categoria " + actionNome + " com sucesso!.";
                    lblMsg.CssClass = "alert alert-success";
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Erro ao " + (categioriaId == 0 ? "adicionar" : "atualizar") + " categoria: " + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            txtCategoriaNome.Text = string.Empty;
            cbIsActive.Checked = true;
            hfCategoriaId.Value = "0";
            btnAddOrUpdate.Text = "Adicionar";
            imagemPreview.ImageUrl = string.Empty;
        }       
    }
}