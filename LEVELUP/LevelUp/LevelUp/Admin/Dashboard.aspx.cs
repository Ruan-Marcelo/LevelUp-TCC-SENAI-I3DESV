using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Dashboard";
            Session["breadCumbPage"] = "";

            if (!IsPostBack)
            {
                Session["breadCrum"] = "";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../Usuario/Login.aspx");
                }
                else
                {
                    DashboardCount dashboard = new DashboardCount();
                    Session["categoria"] = dashboard.Count("CATEGORIAS");
                    Session["produto"] = dashboard.Count("PRODUTO");
                    Session["subCategoria"] = dashboard.Count("SUBCATEGORIA");
                    Session["usuario"] = dashboard.Count("USUARIO");
                    Session["contato"] = dashboard.Count("CONTATO");
                    //Session["valorTotal"] = dashboard.Count("VALORTOTAL");
                }
            }
        }        
    }
}
