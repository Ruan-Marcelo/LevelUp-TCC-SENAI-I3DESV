<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="LevelUp.Admin.Produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
    </div>

    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Categoria</h4>
                    <hr />

                    <div class="form-body">

                        <label>Categoria Nome:</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCategoriaNome" runat="server" CssClass="form-control" placeholder="Digite o nome da categoria"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCategoriaNome" runat="server" ForeColor="Red" FontSize="Small"
                                        Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtCategoriaNome"
                                        ErrorMessage="O nome da categoria é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <label>Categoria Image:</label>
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
                        </div>

                    </div>

                    <div class="form-adction pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Adicionar" OnClick="btnAddOrUpdate_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Limpar" OnClick="btnClear_Click" />
                        </div>
                    </div>

                    <div>
                        <asp:Image ID="imagemPreview" runat="server" CssClass="img-thumbail" AlternateText="" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
