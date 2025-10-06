using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace LevelUp
{
    public class Utils
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlConnection sda;
        SqlDataAdapter sdr;
        DataTable dt;

        public static string getConnection()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        public static bool isValidExtension(string fileName)
        {
            bool isValid = false;
            string[] fileExtension = { ".jpg", ".jpeg", ".png" };
            foreach (string file in fileExtension)
            {
                if (fileName.Contains(file))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        public static string getUniqueId()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public static string getImagemUrl(Object url)
        {
            string url1 = string.Empty;
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value )
            {
                url1 = "../Imagem/No_image.png";
            }
            else
            {
                url1 = string.Format("../{0}", url);
            }
            return url1;
        }

        public static string[] getImagemCaminho(string[] imagens)
        {
            List<string> lista = new List<string>();
            string fileExtension = string.Empty;
            for(int r = 0; r <= imagens.Length - 1; r++)
            {
                fileExtension = Path.GetExtension(imagens[r]);
                lista.Add(getUniqueId() + fileExtension);  
            }
            return lista.ToArray();
        }

        public static string getItemWithCommaSeparator(ListBox listBox)
        {
            string selectedItem = string.Empty;
            foreach (var item in listBox.GetSelectedIndices())
            {
                selectedItem += listBox.Items[item].Text + ",";
            }
            return selectedItem;
        }
    }
}