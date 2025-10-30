<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Loja.aspx.cs" Inherits="LevelUp.Usuario.Loja" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>

    <%-- Padronização da img --%>
    <style>
        .product-img img {
            width: 100%;
            height: 350px;
            object-fit: contain;
            border-radius: 8px;
            transition: transform 0.3s ease;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Todos os Produtos</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="Padrao.aspx">Início</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Loja</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->


    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
    </div>

    <!-- Shop Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <!-- Shop Product Start -->
            <div class="col-lg-12 col-md-12">
                <div class="row pb-3">
                    <div class="col-12 pb-1">
                        <div class="d-flex align-items-center justify-content-between mb-4">
                            <div action="">
                                <div class="input-group">
                                    <input type="text" id="txtSearchInput" runat="server" class="form-control" placeholder="Pesquisar" autocomplete="off">
                                    <div class="input-group-append">
                                        <%--<span class="input-group-text bg-transparent text-primary">
                                            <i class="fa fa-search"></i>
                                        </span>--%>
                                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="input-group-text bg-transparent text-primary" OnClick="btnSearch_Click"> 
                                             <i class="fa fa-search"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnReset" runat="server" CssClass="input-group-text bg-transparent text-primary" OnClick="btnReset_Click">
                                             <i class="fas fa-sync-alt"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="dropdown ml-4">
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlOrdernarPor" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSOrdernarPor_SelectedIndexChanged">
                                        <asp:ListItem Value="0"> Classificar por </asp:ListItem>
                                        <asp:ListItem Value="1"> Mais Recentes </asp:ListItem>
                                        <asp:ListItem Value="2"> A-Z </asp:ListItem>
                                        <asp:ListItem Value="3"> Preço </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="btnOrdernarReset" runat="server" CssClass="input-group-text bg-transparent text-primary" OnClick="btnOrdernarReset_Click">
                                               <i class="fas fa-sync-alt"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:Repeater ID="rProdutos" runat="server" OnItemCommand="rProdutos_ItemCommand">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6 col-sm-12 pb-1">
                                <div class="card product-item border-0 mb-4">
                                    <div class="card-header product-img position-relative overflow-hidden bg-transparent border p-0">
                                        <img class="img-fluid w-100" src='<%# LevelUp.Utils.getImagemUrl( Eval("ImagemUrl")) %>' alt="">
                                    </div>
                                    <div class="card-body border-left border-right text-center p-0 pt-4 pb-3">
                                        <h6 class="text-truncate mb-3"><%# Eval("ProdutoNome") %></h6>
                                        <div class="d-flex justify-content-center">
                                            <h6>R$<%# Eval("Preco") %> </h6>
                                        </div>
                                    </div>
                                    <div class="card-footer d-flex justify-content-between bg-light border">
                                        <a href='<%# "LogaDetalhes.aspx?id=" + Eval("ProdutoId") %>' class="btn btn-sm text-dark p-0"><i class="fas fa-eye text-primary mr-1"></i>Veja mais detalhes</a>
                                        <asp:LinkButton ID="lbAdicionarCart" runat="server"
                                            CssClass="btn btn-sm text-dark p-0"
                                            CommandName="addToCart"
                                            CommandArgument='<%# Eval("ProdutoId") %>'>
                                            <i class="fas fa-shopping-cart text-primary mr-1"></i> Adicionar ao carrinho
                                        </asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <!-- Shop Product End -->
        </div>
    </div>
    <!-- Shop End -->


</asp:Content>
