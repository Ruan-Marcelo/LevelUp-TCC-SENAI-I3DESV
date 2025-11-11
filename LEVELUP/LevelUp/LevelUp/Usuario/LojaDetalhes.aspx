<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="LojaDetalhes.aspx.cs" Inherits="LevelUp.Usuario.LojaDetalhes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var msg = document.getElementById("<%=lblMsg.ClientID %>");
            if (msg && msg.innerText.trim() !== "") {
                setTimeout(function () {
                    msg.style.display = "none";
                }, 5000);
            }
        };
    </script>
    <script>
        function selecionarVariacao(tipo, valor, botao) {
            var containerClass = tipo === 'cor' ? '#<%=divCores.ClientID%>' : '#<%=divTamanhos.ClientID%>';
            var botoes = document.querySelectorAll(containerClass + ' .variacao-btn');

            botoes.forEach(b => b.classList.remove('active'));
            botao.classList.add('active');

            if (tipo === 'cor') {
                document.getElementById('<%=hfCorSelecionada.ClientID%>').value = valor;
            } else {
                document.getElementById('<%=hfTamanhoSelecionado.ClientID%>').value = valor;
            }
        }
    </script>

    <style>
        .variacao-btn {
            padding: 6px 14px;
            margin: 3px;
            border: 1px solid #ccc;
            background: #f8f9fa;
            cursor: pointer;
            border-radius: 4px;
            transition: 0.2s;
            font-size: 14px;
        }

            .variacao-btn:hover {
                border-color: #007bff;
            }

            .variacao-btn.active {
                background: #007bff;
                color: #fff;
                border-color: #0056b3;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
    </div>

    <!-- Shop Detail Start -->
    <div class="container-fluid py-5">
        <div class="row px-xl-5">
            <!-- Images / Carousel -->
            <div class="col-lg-5 pb-5">
                <div id="product-carousel" class="carousel slide border" data-ride="carousel">
                    <div class="carousel-inner" runat="server">
                        <asp:Repeater ID="rImagens" runat="server">
                            <ItemTemplate>
                                <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>'>
                                    <img class="w-100 h-100" src='<%# Eval("ImagemUrl") %>' alt="Produto" style="object-fit: contain; max-height: 500px;" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <a class="carousel-control-prev" href="#product-carousel" role="button" data-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true" style="background-color: gray;"></span>
                        <span class="sr-only">Anterior</span>
                    </a>
                    <a class="carousel-control-next" href="#product-carousel" role="button" data-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true" style="background-color: gray;"></span>
                        <span class="sr-only">Próximo</span>
                    </a>
                </div>
            </div>

            <!-- Product info -->
            <div class="col-lg-7 pb-5">
                <h3 class="font-weight-semi-bold">
                    <asp:Label ID="lblNome" runat="server"></asp:Label>
                </h3>

                <div class="d-flex mb-3">
                    <div class="text-primary mr-2">
                        <small class="fas fa-star"></small>
                        <small class="fas fa-star"></small>
                        <small class="fas fa-star"></small>
                        <small class="fas fa-star-half-alt"></small>
                        <small class="far fa-star"></small>
                    </div>
                    <small class="pt-1">(<asp:Label ID="lblReviewsCount" runat="server" Text="0"></asp:Label>
                        Reviews)</small>
                </div>

                <h3 class="font-weight-semi-bold mb-4">R$
                    <asp:Label ID="lblPreco" runat="server"></asp:Label>
                </h3>

                <p class="mb-2">
                    <asp:Label ID="lblDescricaoCurta" runat="server"></asp:Label>
                </p>
                <p class="mb-4">
                    <asp:Label ID="lblDescricaoLonga" runat="server"></asp:Label>
                </p>

                <asp:Panel ID="pnlAdicional" runat="server" Visible="false">
                    <h6>Informações Adicionais</h6>
                    <p>
                        <asp:Label ID="lblAdicional" runat="server"></asp:Label>
                    </p>
                </asp:Panel>

                <div class="mb-3">
                    <span class="font-weight-medium">Categoria:</span>
                    <asp:Label ID="lblCategoria" runat="server"></asp:Label>
                </div>

                <div class="mb-3">
                    <span class="font-weight-medium">Estoque:</span>
                    <asp:Label ID="lblEstoque" runat="server" CssClass="badge"></asp:Label>

                </div>

                <div id="pnlCor" runat="server" visible="false" class="mb-3">
                    <span class="font-weight-medium">Cor:</span>
                    <div id="divCores" runat="server" class="d-flex flex-wrap mt-2"></div>
                    <asp:HiddenField ID="hfCorSelecionada" runat="server" />
                </div>


                <div id="pnlTamanho" runat="server" visible="false" class="mb-3">
                    <span class="font-weight-medium">Tamanho:</span>
                    <div id="divTamanhos" runat="server" class="d-flex flex-wrap mt-2"></div>
                    <asp:HiddenField ID="hfTamanhoSelecionado" runat="server" />
                </div>

                <div class="d-flex align-items-center mb-4 pt-2">
                    <div class="input-group quantity mr-3" style="width: 130px;">
                        <div class="input-group-btn">
                            <button type="button" class="btn btn-primary btn-minus">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                        <asp:TextBox ID="txtQuantidade" runat="server" CssClass="form-control bg-secondary text-center" Text="1"></asp:TextBox>
                        <div class="input-group-btn">
                            <button type="button" class="btn btn-primary btn-plus">
                                <i class="fa fa-plus"></i>
                            </button>
                        </div>
                    </div>
                    <asp:LinkButton ID="lbAdicionarCart" runat="server"
                        CssClass="btn btn-primary"
                        OnClick="lbAdicionarCart_Click">
                     <i class="fas fa-shopping-cart"></i> Adicionar ao carrinho
                    </asp:LinkButton>

                </div>
            </div>
        </div>

        <!-- Related products -->
        <div class="row px-xl-5 mt-4">
            <div class="col">
                <h4>Produtos relacionados</h4>
                <div class="row pb-3">
                    <asp:Repeater ID="rProdutosRelacionados" runat="server" OnItemCommand="rProdutos_ItemCommand">
                        <ItemTemplate>
                            <div class="col-lg-2 col-md-3 col-sm-6 col-12 pb-1">
                                <div class="card product-item border-0 mb-3">
                                    <div class="card-header product-img position-relative overflow-hidden bg-transparent border p-0">
                                        <img class="img-fluid w-100 produto-img" src='<%# Eval("ImagemUrl") %>' alt="" />
                                    </div>
                                    <div class="card-body border-left border-right text-center p-0 pt-3 pb-3">
                                        <h6 class="text-truncate mb-2"><%# Eval("ProdutoNome") %></h6>
                                        <div class="d-flex justify-content-center">
                                            <h6>R$ <%# String.Format("{0:N2}", Eval("Preco")) %></h6>
                                        </div>
                                    </div>
                                    <div class="card-footer d-flex justify-content-center bg-light border">
                                        <a href='<%# "LojaDetalhes.aspx?id=" + Eval("ProdutoId") %>' class="btn btn-sm text-dark p-0">
                                            <i class="fas fa-eye text-primary mr-1"></i>Detalhes
                                        </a>
                                    </div>
                                    <asp:LinkButton ID="lbAdicionarCart" runat="server"
                                        CssClass="btn btn-sm text-dark p-0"
                                        CommandName="addToCart"
                                        CommandArgument='<%# Eval("ProdutoId") %>'>
                                       <i class="fas fa-shopping-cart text-primary mr-1"></i> Adicionar ao carrinho
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <!-- Shop Detail End -->
</asp:Content>
