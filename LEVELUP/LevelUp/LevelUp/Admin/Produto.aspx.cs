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
        int padraoImgDepoisDaEdicao = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breadCumbTitle"] = "Produto";
            Session["breadCumbPage"] = "Produto";
            if (!IsPostBack)
            {
                getCategorias();
                if (Request.QueryString["id"] != null)
                {
                    GetProdutoDetalhes();
                }
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

        void GetProdutoDetalhes()
        {
            if (Request.QueryString["id"] != null)
            {
                int produtoId = Convert.ToInt32(Request.QueryString["id"]);
                produtoDAL = new ProdutoDAL();
                dt = produtoDAL.ProdutoComImgs(produtoId);
                if (dt.Rows.Count > 0)
                {
                    txtProdutoNome.Text = dt.Rows[0]["ProdutoNome"].ToString();
                    txtPreco.Text = dt.Rows[0]["Preco"].ToString();
                    txtQuantidade.Text = dt.Rows[0]["Quantidade"].ToString();
                    txtPequenadescricao.Text = dt.Rows[0]["DescricaoCurta"].ToString();
                    txtLongaDescricao.Text = dt.Rows[0]["DescricaoLonga"].ToString();
                    txtAdicionalDescricao.Text = dt.Rows[0]["AdicionalDescricao"].ToString();
                    string[] cor = dt.Rows[0]["Cor"].ToString().Split('\u002c');
                    string[] tamanho = dt.Rows[0]["Tamanho"].ToString().Split('\u002c');
                    for (int i = 0; i < cor.Length -1; i++)
                    {
                        lBoxCor.Items.FindByText(cor[i]).Selected = true;
                    }
                    for (int i = 0; i < tamanho.Length -1; i++)
                    {
                        lBoxTamanho.Items.FindByText(tamanho[i]).Selected = true;
                    }
                    txtMarcaNome.Text = dt.Rows[0]["NomeEmpresa"].ToString();
                    ddlCategoria.SelectedValue = dt.Rows[0]["CategoriaId"].ToString();
                    getSubCategorias(Convert.ToInt32(dt.Rows[0]["CategoriaId"]));
                    ddlSubCategoria.SelectedValue = dt.Rows[0]["SubCategoriaId"].ToString();
                    cbIsCustomizado.Checked = Convert.ToBoolean(dt.Rows[0]["Personalizado"]);
                    cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["EstaAtivo"]);
                    rblPadraoImagem.SelectedIndex = Convert.ToInt32(dt.Rows[0]["ImagemPadrao"]);
                    hfPadraoimagem.Value = (Convert.ToInt32(dt.Rows[0]["ImagemPadrao"]) + 1).ToString();
                    imagemProduct1.ImageUrl = "../" + dt.Rows[0]["Imagem1"].ToString().Substring(0, dt.Rows[0]["Imagem1"].ToString().IndexOf(":"));
                    imagemProduct2.ImageUrl = "../" + dt.Rows[0]["Imagem2"].ToString().Substring(0, dt.Rows[0]["Imagem2"].ToString().IndexOf(":"));
                    imagemProduct3.ImageUrl = "../" + dt.Rows[0]["Imagem3"].ToString().Substring(0, dt.Rows[0]["Imagem3"].ToString().IndexOf(":"));
                    imagemProduct4.ImageUrl = "../" + dt.Rows[0]["Imagem4"].ToString().Substring(0, dt.Rows[0]["Imagem4"].ToString().IndexOf(":"));
                    imagemProduct1.Width = 200;
                    imagemProduct2.Width = 200;
                    imagemProduct3.Width = 200;
                    imagemProduct4.Width = 200;
                    imagemProduct1.Style.Remove("Display");
                    imagemProduct2.Style.Remove("Display");
                    imagemProduct3.Style.Remove("Display");
                    imagemProduct4.Style.Remove("Display");
                    btnAddOrUpdate.Text = "upadte";

                }
            }
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedColor = string.Empty;
                string selctedTamanho = string.Empty;
                bool valido = false;
                bool execucaoValida = false;
                List<string> list = new List<string>();
                bool imagemSalva = false;
                if (Request.QueryString["id"] == null)
                {
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
                                ProdutoId = Request.QueryString["id"] == null ? 0 : Convert.ToInt32(Request.QueryString["id"]),
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
                                SubCategoriaId = Convert.ToInt32(ddlSubCategoria.SelectedValue), 
                                Personalizado = cbIsCustomizado.Checked,
                                EstaAtivo = cbIsActive.Checked,
                                ProdutosImagens = ProdutoImagem
                            };

                            int p = produtoDAL.AddUpdateProduto(produtoObj);
                            if (p > 0)
                            {
                                DisplayMessage("✅ Produto salvo com sucesso.", "success");
                                Response.AddHeader("REFRESH", "2;URL=ProdutoLista.aspx");
                            }
                            else
                            {
                                DisplayMessage("⚠️ Erro ao salvar o produto. Tente novamente.", "warning");
                            }
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
                else
                {
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
                            execucaoValida = true;
                        }
                        else
                        {
                            DeletarArqiuvo(imageLocal);                         
                        }                  
                    }
                    else if (fuPrimeiraImagem.HasFile || fuSegundaImagem.HasFile || fuTerceiraImagem.HasFile || fuQuartaImagem.HasFile)
                    {
                        DisplayMessage("Por favor, selecione todas as quatro imagens e se quiser atualizar.", "warning");
                    }
                    else
                    {
                        //atualizar  sem img
                        if(Convert.ToInt32(hfPadraoimagem.Value) !=  Convert.ToInt32(rblPadraoImagem.SelectedValue))
                        {
                            padraoImgDepoisDaEdicao = Convert.ToInt32(rblPadraoImagem.SelectedValue);
                        }
                        execucaoValida = true;
                    }                  

                    if (execucaoValida)
                    {
                        selectedColor = Utils.getItemWithCommaSeparator(lBoxCor);
                        selctedTamanho = Utils.getItemWithCommaSeparator(lBoxTamanho);

                        produtoDAL = new ProdutoDAL();
                        produtoObj = new ProdutoObj()
                        {
                            ProdutoId = Convert.ToInt32(Request.QueryString["id"]),
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
                            SubCategoriaId = Convert.ToInt32(ddlSubCategoria.SelectedValue),
                            Personalizado = cbIsCustomizado.Checked,
                            EstaAtivo = cbIsActive.Checked,
                            ProdutosImagens = ProdutoImagem,
                            PosicaoPadraoImg = padraoImgDepoisDaEdicao
                        };

                        int p = produtoDAL.AddUpdateProduto(produtoObj);
                        if (p > 0)
                        {
                            DisplayMessage("✅ Produto atualizado com sucesso.", "success");
                            Response.AddHeader("REFRESH", "2;URL=ProdutoLista.aspx");
                        }
                        else
                        {
                            DeletarArqiuvo(imageLocal);
                            DisplayMessage("não é possível atualizar o registro neste momento.", "warning");
                        }
                    }
                    else
                    {
                        DisplayMessage("Algo deu errado", "danger");
                    }
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