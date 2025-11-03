using System;
using System.Data;
using System.Data.SqlClient;

namespace LevelUp.Usuario
{
    public partial class LojaDetalhes : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    getProdutoDetalhes();
                }
                else
                {
                    Response.Redirect("Index.aspx");
                }
            }
        }

        void getProdutoDetalhes()
        {
            int produtoId = Convert.ToInt32(Request.QueryString["id"]);
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Produto_Crud", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@ProdutoId", produtoId);

            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
        }
    }
}
