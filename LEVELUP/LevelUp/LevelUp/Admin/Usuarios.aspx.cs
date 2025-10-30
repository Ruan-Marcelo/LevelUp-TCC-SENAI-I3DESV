using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Admin
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Lista Usuários";
            Session["breadCumbPage"] = "Lista Usuários";
        }

        protected void rUsuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}