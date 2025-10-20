using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LevelUp.Usuario
{
    public partial class Usuario : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Url.AbsoluteUri.ToString().Contains("Padrao.aspx"))
            {
                //Carregar o controle
                Control slideUsuarioControle = (Control)Page.LoadControl("SlideUsuarioControle.ascx");
                pnlSliderUC.Controls.Add(slideUsuarioControle);
            }
        }

        protected void rCategoria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}