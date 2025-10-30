﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="LevelUp.Admin.Usuarios" %>

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
    <div class="col-sm-12 col-md-12">
        <div class="card">
            <div class="card-body">
                <hr />
                <div class="table-responsive">

                    <asp:Repeater ID="rUsuarios" runat="server" OnItemCommand="rUsuarios_ItemCommand">
                        <HeaderTemplate>
                            <table class="table data-table-export table-hover nowrap">
                                <thead>
                                    <tr>
                                        <th class="table-plus">SrNo</th>
                                        <th>Nome </th>
                                        <th>Nomde de usuário</th>
                                        <th>Email</th>
                                        <th>Data de criação</th>                                       
                                        <th class="datatable-nosort">Ação</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="table-plus"><%# Eval("Nome") %> </td>
                                <td>
                                    <img width="40" src="<%# LevelUp.Utils.getImagemUrl( Eval("ImagemUrl")) %>" alt="imagem" />
                                    <asp:Label ID="lblDebug" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <%# Eval("Preco") %>
                                </td>
                                <td>
                                    <asp:Label ID="lblQuantidade" runat="server" Text='<%# Eval("Quantidade") %>'></asp:Label>
                                </td>
                                <td>
                                    <%# Eval("Vendido") %>
                                </td>
                                <td>
                                    <%# Eval("CategoriaNome") %>
                                </td>
                                <td>
                                    <%# Eval("SubCategoriaNome") %>
                                </td>
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
                                        CommandArgument='<%# Eval("ProdutoId") %>' CommandName="editar" CausesValidation="false">
                                    <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbDeletar" Text="Deletar" runat="server" CssClass="badge badge-danger"
                                        CommandArgument='<%# Eval("ProdutoId") %>' CommandName="deletar" CausesValidation="false"
                                        OnClientClick="return confirm('Você tem certeza que deseja excluir esse usuário?');">
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

</asp:Content>
