using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace LevelUp.Admin
{
    public partial class CategoriaBlog : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategorias();
                lblMsg.Visible = false;
            }
        }

        void getCategorias()
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("CategoriaPost_Crud", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GETALL");
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            rCategoriaBlog.DataSource = dt;
            rCategoriaBlog.DataBind();
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfCategoriaPostId.Value);

            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("CategoriaPost_Crud", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", id == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@CategoriaPostId", id);
            cmd.Parameters.AddWithValue("@NomeCategoria", txtCategoriaNome.Text.Trim());
            cmd.Parameters.AddWithValue("@EstaAtivo", cbIsActive.Checked);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                lblMsg.Visible = true;
                lblMsg.CssClass = "alert alert-success";
                lblMsg.Text = id == 0 ?
                    "Categoria adicionada com sucesso!" :
                    "Categoria atualizada com sucesso!";

                clear();
                getCategorias();
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.CssClass = "alert alert-danger";
                lblMsg.Text = "Erro: " + ex.Message;
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
            txtCategoriaNome.Text = "";
            cbIsActive.Checked = true;
            hfCategoriaPostId.Value = "0";
            btnAddOrUpdate.Text = "Adicionar";
        }

        protected void rCategoriaBlog_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("CategoriaPost_Crud", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@CategoriaPostId", e.CommandArgument);

                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                txtCategoriaNome.Text = dt.Rows[0]["NomeCategoria"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["EstaAtivo"]);
                hfCategoriaPostId.Value = dt.Rows[0]["CategoriaPostId"].ToString();

                btnAddOrUpdate.Text = "Alterar";
            }

            else if (e.CommandName == "deletar")
            {
                con = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("CategoriaPost_Crud", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@CategoriaPostId", e.CommandArgument);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Visible = true;
                    lblMsg.CssClass = "alert alert-success";
                    lblMsg.Text = "Categoria deletada com sucesso!";

                    getCategorias();
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.CssClass = "alert alert-danger";
                    lblMsg.Text = "Erro: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
