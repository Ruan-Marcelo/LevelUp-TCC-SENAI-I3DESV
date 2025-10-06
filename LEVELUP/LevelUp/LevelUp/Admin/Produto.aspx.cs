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
    public partial class Produto : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt, dt1;
        string[] imageLocal;
        ProdutoObj produtoObj;
        ProdutoDAL produtoDAL;
        List<ProdutoImgObj> ProdutoImagem = new List<ProdutoImgObj>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Produto";
            Session["breadCumbPage"] = "Produto";
            if (!IsPostBack)
            {
                getCategorias();
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

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {         
                getSubCategorias(Convert.ToInt32(ddlCategoria.SelectedValue));             
        }

        void getSubCategorias(int categoriaId)
        {
            con = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("SELECT SubCategoriaId, SubCategoriaNome FROM SubCategoria WHERE CategoriaId = @CategoriaId AND EstaAtivo = 1", con);
            cmd.Parameters.AddWithValue("@CategoriaId", categoriaId);
            sda = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            sda.Fill(dt1);
            ddlSubCategoria.Items.Clear();
            ddlSubCategoria.DataSource = dt1;
            ddlSubCategoria.DataTextField = "SubCategoriaNome";
            ddlSubCategoria.DataValueField = "SubCategoriaId";
            ddlSubCategoria.DataBind();
            ddlSubCategoria.Items.Insert(0, new ListItem("--Selecione a Subcategoria--", "0"));
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedColor = string.Empty;
                string selctedTamanho = string.Empty;
                bool valido = false;
                List<string> list = new List<string>();
                bool imagemSalva = false;

                if (fuPrimeiraImagem.HasFile && fuSegundaImagem.HasFile && fuTerceiraImagem.HasFile && fuQuartaImagem.HasFile)
                {
                    list.Add(fuPrimeiraImagem.FileName);
                    list.Add(fuSegundaImagem.FileName);
                    list.Add(fuTerceiraImagem.FileName);
                    list.Add(fuQuartaImagem.FileName);
                    string[] fu = list.ToArray();

                    // Valida tipo das imagens
                    valido = fu.All(file => Utils.isValidExtension(file));

                    if (!valido)
                    {
                        DisplayMessage("Por favor, selecione apenas arquivos .jpg, .jpeg ou .png.", "danger");
                        return;
                    }

                    imageLocal = Utils.getImagemCaminho(fu);
                    for (int r = 0; r < imageLocal.Length; r++)
                    {
                        ProdutoImagem.Add(new ProdutoImgObj()
                        {
                            ImagemUrl = imageLocal[r],
                            ImagemPadrao = Convert.ToBoolean(rblPadraoImagem.Items[r].Selected)
                        });

                        string savePath = Server.MapPath("~/Imagem/Produto/") + Path.GetFileName(imageLocal[r]);
                        switch (r)
                        {
                            case 0: fuPrimeiraImagem.PostedFile.SaveAs(savePath); break;
                            case 1: fuSegundaImagem.PostedFile.SaveAs(savePath); break;
                            case 2: fuTerceiraImagem.PostedFile.SaveAs(savePath); break;
                            case 3: fuQuartaImagem.PostedFile.SaveAs(savePath); break;
                        }
                        imagemSalva = true;
                    }

                    if (imagemSalva)
                    {
                        selectedColor = Utils.getItemWithCommaSeparator(lBoxCor);
                        selctedTamanho = Utils.getItemWithCommaSeparator(lBoxTamanho);

                        produtoDAL = new ProdutoDAL();
                        produtoObj = new ProdutoObj()
                        {
                            ProdutoId = 0,
                            ProdutoNome = txtProdutoNome.Text.Trim(),
                            DescricaoCurta = txtPequenadescricao.Text.Trim(),
                            DescricaoLonga = txtLongaDescricao.Text.Trim(),
                            AdicionalDescricao = txtAdicionalDescricao.Text.Trim(),
                            Preco = Convert.ToDecimal(txtPreco.Text.Trim()),
                            Quantidade = Convert.ToInt32(txtQuantidade.Text.Trim()),
                            Tamanho = selctedTamanho,
                            Cor = selectedColor,
                            NomeEmpresa = txtMarcaNome.Text.Trim(),
                            CategoriaId = Convert.ToInt32(ddlCategoria.SelectedValue),
                            SubCategoriaId = Convert.ToInt32(ddlSubCategoria.SelectedValue), // ✅ CORRIGIDO
                            Personalizado = cbIsCustomizado.Checked,
                            EstaAtivo = cbIsActive.Checked,
                            ProdutosImagens = ProdutoImagem
                        };

                        int p = produtoDAL.AddUpdateProduto(produtoObj);
                        if (p > 0)
                            DisplayMessage("✅ Produto salvo com sucesso.", "success");
                        else
                            DisplayMessage("⚠️ Erro ao salvar o produto. Tente novamente.", "warning");
                    }
                    else
                    {
                        DeletarArqiuvo(imageLocal);
                        DisplayMessage("Erro ao salvar as imagens. Tente novamente.", "warning");
                    }
                }
                else
                {
                    DisplayMessage("Por favor, selecione todas as quatro imagens.", "warning");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("❌ Erro: " + ex.Message, "danger");
            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void DeletarArqiuvo(string[] caminhoArquivo)
        {
            for (int r = 0; r < caminhoArquivo.Length - 1; r++)
            {
                if (File.Exists(Server.MapPath("~/" + caminhoArquivo[r])))
                {
                    File.Delete(Server.MapPath("~/" + caminhoArquivo[r]));
                }
            }
        }

        void DisplayMessage(string msg, string cssClass)
        {
            lblMsg.Visible = true;
            lblMsg.Text = msg;
            lblMsg.CssClass = "alert alert-" + cssClass;
        }

        private void Clear()
        {
            txtProdutoNome.Text = string.Empty; 
            txtPequenadescricao.Text = string.Empty;
            txtLongaDescricao.Text = string.Empty;
            txtAdicionalDescricao.Text = string.Empty;
            txtPreco.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtMarcaNome.Text = string.Empty;
            lBoxCor.ClearSelection();
            lBoxTamanho.ClearSelection();
            ddlCategoria.ClearSelection();
            ddlSubCategoria.ClearSelection();
            rblPadraoImagem.ClearSelection();
            cbIsActive.Checked = false;
            cbIsCustomizado.Checked = false;
            hfPadraoimagem.Value = "0";
        }

    }
}