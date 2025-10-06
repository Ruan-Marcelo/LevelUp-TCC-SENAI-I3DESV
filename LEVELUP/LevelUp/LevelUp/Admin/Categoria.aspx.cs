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
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Manage Categoria";
            Session["breadCumbPage"] = "Categoria";
            lblMsg.Visible = false;
            getCategorias();
        }

        void getCategorias()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Categoria_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCategoria.DataSource = dt;
            rCategoria.DataBind();
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
                    cmd.Parameters.AddWithValue("@CategoriaImgUrl", imagemPath);
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
                    getCategorias();
                    clear();
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

        protected void rCategoria_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "editar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Categoria_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@CategoriaId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtCategoriaNome.Text = dt.Rows[0]["CategoriaNome"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["EstaAtivo"]);
                imagemPreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["CategoriaImgUrl"].ToString()) ? "../Imagem/No_image.png" : "../" + dt.Rows[0]["CategoriaImgUrl"].ToString();
                imagemPreview.Height = 200;
                imagemPreview.Width = 200;
                hfCategoriaId.Value = dt.Rows[0]["CategoriaId"].ToString();
                btnAddOrUpdate.Text = "Alterar";

            }
            else if (e.CommandName == "deletar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Categoria_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@CategoriaId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Categoria deletada com sucesso!.";
                    lblMsg.CssClass = "alert alert-success";
                    getCategorias();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Erro ao "  + ex.Message;
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