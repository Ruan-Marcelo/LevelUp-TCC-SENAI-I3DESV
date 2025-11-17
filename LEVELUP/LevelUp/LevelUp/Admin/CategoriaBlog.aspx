<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CategoriaBlog.aspx.cs" Inherits="LevelUp.Admin.CategoriaBlog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
    </div>

    <div class="row">
        <!-- Formulário de Categoria do Blog -->
        <div class="col-sm-12 col-md-4">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Categoria do Blog</h4>
                    <hr />

                    <div class="form-body">

                        <label>Nome da Categoria:</label>
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

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; Está Ativo" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-adction pb-5">

                        <asp:HiddenField ID="hfCategoriaPostId" runat="server" Value="0" />

                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Adicionar" OnClick="btnAddOrUpdate_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Limpar" OnClick="btnClear_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <!-- Listagem de Categorias do Blog -->
        <div class="col-sm-12 col-md-8">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Lista de Categorias do Blog</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rCategoriaBlog" runat="server" OnItemCommand="rCategoriaBlog_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th>Nome</th>
                                            <th>Está Ativo</th>
                                            <th>Data de Criação</th>
                                            <th>Ação</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("NomeCategoria") %> </td>
                                    <td>
                                        <asp:Label ID="lblEstaAtivo" runat="server"
                                            Text='<%# (bool)Eval("EstaAtivo") ? "Ativo" : "Inativo" %>'
                                            CssClass='<%# (bool)Eval("EstaAtivo") ? "badge badge-success" : "badge badge-danger" %>'>                                           
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("DataCriacao") %>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Text="Editar" runat="server" CssClass="badge badge-primary"
                                            CommandArgument='<%# Eval("CategoriaPostId") %>' CommandName="editar" CausesValidation="false">
                                            <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDeletar" Text="Deletar" runat="server" CssClass="badge badge-danger"
                                            CommandArgument='<%# Eval("CategoriaPostId") %>' CommandName="deletar" CausesValidation="false"
                                            OnClientClick="return confirm('Você tem certeza que deseja excluir essa categoria?');">
                                            <i class="fas fa-trash-alt"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
