<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="LevelUp.Admin.Produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
    <script>
        function ImagemPreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%= fuPrimeiraImagem.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
    </div>

    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Produtos</h4>
                    <hr />

                    <div class="form-body">

                        <div class="row">
                            <div class="col-md-6">
                                <label>Produtos Nome:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtProdutoNome" runat="server" CssClass="form-control" placeholder="Digite o nome do Produto"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvProdutoNome" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtProdutoNome"
                                        ErrorMessage="O nome do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Categoria :</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                        <asp:ListItem Value="0">--Selecione--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="ddlCategoria" InitialValue="0"
                                        ErrorMessage="Categoria é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Sub-Categoria :</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlSubCategoria" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">--Selecione--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSubCategoria" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="ddlSubCategoria"
                                        ErrorMessage="SubCategoria é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>Preço :</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtPreco" runat="server" CssClass="form-control" placeholder="Digite o preço do Produto"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPreco" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtPreco"
                                        ErrorMessage="O preço do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revPreco" runat="server" ControlToValidate="txtPreco"
                                        ValidationExpression="\d+(?:.\d{1,2})?" ErrorMessage="O preço do Produto está inválido" ForeColor="Red"
                                        Display="Dynamic" SetFocusOnError="true" Font-Size="smaller"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Cor :</label>
                                <div class="form-group">
                                    <asp:ListBox ID="lBoxCor" runat="server" CssClass="form-control" SelectionMode="Multiple" ToolTip="Use a tecla CTRL para selecionar multiplos itens">
                                        <asp:ListItem Value="1">Azul</asp:ListItem>
                                        <asp:ListItem Value="2">Vermelho</asp:ListItem>
                                        <asp:ListItem Value="3">Rosa</asp:ListItem>
                                        <asp:ListItem Value="4">Roxo</asp:ListItem>
                                        <asp:ListItem Value="5">Marrom</asp:ListItem>
                                        <asp:ListItem Value="6">Cinza</asp:ListItem>
                                        <asp:ListItem Value="7">Verde</asp:ListItem>
                                        <asp:ListItem Value="8">Amarelo</asp:ListItem>
                                        <asp:ListItem Value="9">Branco</asp:ListItem>
                                        <asp:ListItem Value="10">Pretão</asp:ListItem>
                                    </asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Tamanho :</label>
                                <div class="form-group">
                                    <asp:ListBox ID="lBoxTamanho" runat="server" CssClass="form-control" SelectionMode="Multiple" ToolTip="Use a tecla CTRL para selecionar multiplos itens">
                                        <asp:ListItem Value="1">P</asp:ListItem>
                                        <asp:ListItem Value="2">PP</asp:ListItem>
                                        <asp:ListItem Value="3">M</asp:ListItem>
                                        <asp:ListItem Value="4">G</asp:ListItem>
                                        <asp:ListItem Value="5">GG</asp:ListItem>
                                        <asp:ListItem Value="6">XVIDEOS</asp:ListItem>
                                    </asp:ListBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Quantidade:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtQuantidade" runat="server" CssClass="form-control" placeholder="Digite a quantidade do Produto"
                                        TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQuantidade" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtQuantidade"
                                        ErrorMessage="A Quantidade do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Nome da marca:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtMarcaNome" runat="server" CssClass="form-control" placeholder="Digite a marca do Produto">

                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMarcaNome" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtMarcaNome"
                                        ErrorMessage="A marca do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <label>Pequena descrição:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtPequenadescricao" runat="server" CssClass="form-control" placeholder="Digite uma Pequena descrição do Produto"
                                        TextMode="Multiline">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPequenadescricao" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtPequenadescricao"
                                        ErrorMessage="A pequena descrição do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label>Longa descrição:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtLongaDescricao" runat="server" CssClass="form-control" placeholder="Digite uma Longa descrição do Produto"
                                        TextMode="Multiline">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLongaDescricao" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtLongaDescricao"
                                        ErrorMessage="A Longa descrição do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <label>Adicional descrição:</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtAdicionalDescricao" runat="server" CssClass="form-control" placeholder="Digite uma descrição Adicional do Produto"
                                        TextMode="Multiline">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAdicionalDescricao" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtAdicionalDescricao"
                                        ErrorMessage="A descrição Adicional do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <label>Tags(Search keyword):</label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtTags" runat="server" CssClass="form-control" placeholder="Digite uma Tags">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTag" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtTags"
                                        ErrorMessage="As Tags do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Imagens do Produto (1)</label>
                                <div class="form-group">
                                    <asp:FileUpload ID="fuPrimeiraImagem" runat="server" CssClass="form-control" ToolTip=".jpg, .png, .jpeg image only"
                                        onchange="ImagemPreview(this);" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Imagens do Produto (2)</label>
                                <div class="form-group">
                                    <asp:FileUpload ID="fuSegundaImagem" runat="server" CssClass="form-control" ToolTip=".jpg, .png, .jpeg image only"
                                        onchange="ImagemPreview(this);" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Imagens do Produto (3)</label>
                                <div class="form-group">
                                    <asp:FileUpload ID="fuTerceiraImagem" runat="server" CssClass="form-control" ToolTip=".jpg, .png, .jpeg image only"
                                        onchange="ImagemPreview(this);" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Imagens do Produto (4)</label>
                                <div class="form-group">
                                    <asp:FileUpload ID="fuQuartaImagem" runat="server" CssClass="form-control" ToolTip=".jpg, .png, .jpeg image only"
                                        onchange="ImagemPreview(this);" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <label>Imagem Padrão:</label>
                                <div class="form-group">
                                    <asp:RadioButtonList ID="rblPadraoImagem" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1"> &nbsp; Primeiro &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2"> &nbsp; Segundo &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="3"> &nbsp; Terceira &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="4"> &nbsp; Quarto &nbsp;</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="txtPadraoImage" runat="server" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="rblPadraoImagem"
                                        ErrorMessage="Imagem Padrão do Produto é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hfPadraoimagem" runat="server" Value="0" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Customizado:</label>
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsCustomizado" runat="server" Text="&nbsp;  ECustomizado" />
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Customizado:</label>
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp;   Está Ativo" />
                                </div>
                            </div>
                        </div>



                        <%--  <label>Categoria Image:</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:FileUpload ID="fuCategoriaImagem" runat="server" CssClass="form-control"
                                        onchange="ImagemPreview(this);" />
                                    <asp:HiddenField ID="hfCategoriaId" runat="server" Value="0" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp;  Está Ativo" />
                                </div>
                            </div>
                        </div>--%>
                    </div>

                    <%-- <div class="form-adction pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Adicionar" OnClick="btnAddOrUpdate_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Limpar" OnClick="btnClear_Click" />
                        </div>
                    </div>

                    <div>
                        <asp:Image ID="imagemPreview" runat="server" CssClass="img-thumbail" AlternateText="" />
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
