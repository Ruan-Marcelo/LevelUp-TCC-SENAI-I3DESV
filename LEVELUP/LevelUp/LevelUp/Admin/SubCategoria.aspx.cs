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
    public partial class SubCategoria : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Manage Sub-Categoria";
            Session["breadCumbPage"] = "Sub-Categoria";
            if (!IsPostBack)
            {
                getCategorias();
                getSubCategorias();
            }
            lblMsg.Visible = false;
            
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
            ddlCategoria.DataSource = dt;
            ddlCategoria.DataTextField = "CategoriaNome";
            ddlCategoria.DataValueField = "CategoriaId";
            ddlCategoria.DataBind();
        }

        void getSubCategorias()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SubCategoria_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rSubCategoria.DataSource = dt;
            rSubCategoria.DataBind();

            ddlCategoria.Items.Add(new ListItem("--Selecione a Categoria--", "0"));
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionNome = string.Empty;
            int subCategoriaId = Convert.ToInt32(hfSubCategoriaId.Value);
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SubCategoria_Crud", con);
            cmd.Parameters.AddWithValue("@Action", subCategoriaId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@SubCategoriaId", subCategoriaId);            
            cmd.Parameters.AddWithValue("@SubCategoriaNome", txtSubCategoriaNome.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoriaId", Convert.ToInt32( ddlCategoria.SelectedValue));
            cmd.Parameters.AddWithValue("@EstaAtivo", cbIsActive.Checked);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                actionNome = subCategoriaId == 0 ? "adicionada" : "atualizada";
                lblMsg.Visible = true;
                lblMsg.Text = "Sub-Categoria " + actionNome + " com sucesso!.";
                lblMsg.CssClass = "alert alert-success";
                getSubCategorias();
                clear();
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Erro ao " + (subCategoriaId == 0 ? "adicionar" : "atualizar") + " categoria: " + ex.Message;
                lblMsg.CssClass = "alert alert-danger";
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            txtSubCategoriaNome.Text = string.Empty;
            cbIsActive.Checked = true;
            hfSubCategoriaId.Value = "0";
            btnAddOrUpdate.Text = "Adicionar";
            ddlCategoria.ClearSelection();
        }

        protected void rSubCategoria_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMsg.Visible = false;
            if (e.CommandName == "editar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("SubCategoria_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@SubCategoriaId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtSubCategoriaNome.Text = dt.Rows[0]["SubCategoriaNome"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["EstaAtivo"]);       
                ddlCategoria.SelectedValue = dt.Rows[0]["CategoriaId"].ToString();
                hfSubCategoriaId.Value = dt.Rows[0]["SubCategoriaId"].ToString();
                btnAddOrUpdate.Text = "Alterar";

            }
            else if (e.CommandName == "deletar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("SubCategoria_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@SubCategoriaId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMsg.Visible = true;
                    lblMsg.Text = "Sub-Categoria deletada com sucesso!.";
                    lblMsg.CssClass = "alert alert-success";
                    getSubCategorias();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Erro ao " + ex.Message;
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