<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="LevelUp.Usuario.Perfil" %>

<%@ Import Namespace="LevelUp" %>

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

    <%
        string imagemUrl = Session["imagemUrl"].ToString();
    %>

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <h2>Informações do usuário</h2>
            </div>

            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <div class="card-title mb-4">
                            <div class="d-flex justify-content-start">
                                <div class="image-container">
                                    <img src="<%= Utils.getImagemUrl(imagemUrl) %>" id="imgPerfil" style="width: 150px; height: 150px;"
                                        class="img-thumbnail" />
                                    <div class="middle pt-2">
                                        <a href="RegistrarUser.aspx?id=<% Response.Write(Session["usuarioId"]);%>"
                                            class="btn btn-warning">
                                            <i class="fas fa-pencil-alt"></i>Editar informações
                                        </a>
                                    </div>
                                </div>

                                <div class="userData ml-3">
                                    <h2 class="d-block" style="font-size: 1.5rem; font-weight: bold;">
                                        <a href="javascript:void(0)"> <%Response.Write(Session["nome"]); %>
                                        </a>
                                    </h2>
                                    <h6 class="d-block">
                                        <a href="javascript:void(0)">
                                            <asp:Label ID="lblNomeDeUsuario" runat="server" TollTip="Nomde de usuário">
                                                @<%Response.Write(Session["nomeDeUsuario"]); %>
                                            </asp:Label>
                                        </a>
                                    </h6>
                                    <h6 class="d-block">
                                        <a href="javascript:void(0)">
                                            <asp:Label ID="lblEmail" runat="server" TollTip="Email do usuário">
                                                <%Response.Write(Session["email"]); %>
                                            </asp:Label>
                                        </a>
                                    </h6>
                                    <h6 class="d-block">
                                        <a href="javascript:void(0)">
                                            <asp:Label ID="lblDataCriacao" runat="server" TollTip="Data de criação">
                                                <%Response.Write(Session["dataCriacao"]); %>
                                            </asp:Label>
                                        </a>
                                    </h6>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-12">
                                <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active text-info" id="simplesInformacao-tab" data-toggle="tab" href="#simplesInformacao-tab" role="tab" aria-controls="simplesInformacao" aria-selected="true">
                                            <i class="fa fa-id-badge mr-2"></i>Informações básicas
                                        </a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="myTabContent">
                                    <div class="tab-pane fade show active" id="simplesInformacao" role="tabpanel" aria-labelledby="simplesInformacao-tab">
                                        <asp:Repeater ID="rUsuarioPerfil" runat="server">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Nome completo</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("Nome") %>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Nome de usuário</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("NomeDeUsuario") %>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Email</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("Email") %>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Celular</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("Celular") %>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">Código postal</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("codigoPostal") %>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-3 col-md-2 col-5">
                                                        <label style="font-weight: bold;">endereço</label>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <%# Eval("endereco") %>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>

</asp:Content>
