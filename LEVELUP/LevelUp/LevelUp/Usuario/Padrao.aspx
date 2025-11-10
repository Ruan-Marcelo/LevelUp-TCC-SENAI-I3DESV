<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Padrao.aspx.cs" Inherits="LevelUp.Usuario.Padrao" %>
<%@ Import Namespace="LevelUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
     
      .produto-img {
          height: 220px;
          
          width: 100%;
      }
      .product-item {
          min-height: 360px;
          display: flex;
          flex-direction: column;
          justify-content: space-between;
      }
        
          .product-item h6 {
              font-size: 14px;
              line-height: 1.3em;
          }
  </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Featured Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5 pb-3">
            <div class="col-lg-3 col-md-6 col-sm-12 pb-1">
                <div class="d-flex align-items-center border mb-4" style="padding: 30px;">
                    <h1 class="fa fa-check text-primary m-0 mr-3"></h1>
                    <h5 class="font-weight-semi-bold m-0">Qualidade</h5>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 pb-1">
                <div class="d-flex align-items-center border mb-4" style="padding: 30px;">
                    <h1 class="fa fa-shipping-fast text-primary m-0 mr-2"></h1>
                    <h5 class="font-weight-semi-bold m-0">Frete grátis</h5>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 pb-1">
                <div class="d-flex align-items-center border mb-4" style="padding: 30px;">
                    <h1 class="fas fa-exchange-alt text-primary m-0 mr-3"></h1>
                    <h5 class="font-weight-semi-bold m-0">7 dias ou seu dinheiro de volta</h5>
                </div>
            </div>
            <div class="col-lg-3 col-md-6 col-sm-12 pb-1">
                <div class="d-flex align-items-center border mb-4" style="padding: 30px;">
                    <h1 class="fa fa-phone-volume text-primary m-0 mr-3"></h1>
                    <h5 class="font-weight-semi-bold m-0">24/7 Suporte</h5>
                </div>
            </div>
        </div>
    </div>
    <!-- Featured End -->


    <!-- Categories Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5 pb-3">
            <asp:Repeater ID="rCategoria" runat="server">
                <ItemTemplate>
                    <div class="col-lg-4 col-md-6 pb-1">
                        <div class="cat-item d-flex flex-column border mb-4" style="padding: 30px;">
                            <p class="text-right"><%# Eval("ProdutoCount") %> Produtos</p>
                            <a href="Loja.aspx?cid=<%# Eval("CategoriaId") %>" class="cat-img position-relative overflow-hidden mb-3">
                                <img class="img-fluid" src="<%# Utils.getImagemUrl( Eval("CategoriaImgUrl") )  %>"" alt="">
                            </a>
                            <h5 class="font-weight-semi-bold m-0"><%# Eval("CategoriaNome") %></h5>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>  
            </div>
    </div>
    <!-- Categories End -->

    
                      <!-- Shop Product Start -->
  <div class="col-lg-12 col-md-12">
      <div class="row pb-3">
          <div class="col-12 pb-1">
              <div class="d-flex align-items-center justify-content-between mb-4">
                  <div action="">
                      <div class="input-group">
                          <input type="text" id="txtSearchInput" runat="server" class="form-control" placeholder="Pesquisar por nome" autocomplete="off">
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
                          <%-- <button class="btn border dropdown-toggle" type="button" id="triggerId" data-toggle="dropdown" aria-haspopup="true"
                                   aria-expanded="false">
                                   Sort by
                               </button>
                               <div class="dropdown-menu dropdown-menu-right" aria-labelledby="triggerId">
                                   <a class="dropdown-item" href="#">Latest</a>
                                   <a class="dropdown-item" href="#">Popularity</a>
                                   <a class="dropdown-item" href="#">Best Rating</a>
                              </div>--%>
                          <asp:DropDownList ID="ddlSOrdernarPor" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
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

   <asp:Repeater ID="rProdutos" runat="server">
    <ItemTemplate>
        <div class="col-lg-2 col-md-3 col-sm-6 col-12 pb-1">
            <div class="card product-item border-0 mb-3">
                <div class="card-header product-img position-relative overflow-hidden bg-transparent border p-0">
                    <img class="img-fluid w-100 produto-img" 
                         src='<%# LevelUp.Utils.getImagemUrl(Eval("ImagemUrl")) %>' alt="">
                </div>
                <div class="card-body border-left border-right text-center p-0 pt-3 pb-3">
                    <h6 class="text-truncate mb-2"><%# Eval("ProdutoNome") %></h6>
                    <div class="d-flex justify-content-center">
                        <h6>R$ <%# Eval("Preco") %></h6>
                    </div>                                       
                </div>
                <div class="card-footer d-flex justify-content-center bg-light border">
                    <a href='<%# "LojaDetalhes.aspx?id=" + Eval("ProdutoId") %>' class="btn btn-sm text-dark p-0">
                        <i class="fas fa-eye text-primary mr-1"></i>Detalhes
                    </a>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

      </div>
  </div>
  <!-- Shop Product End -->

    <!-- Offer Start -->
    <div class="container-fluid offer pt-5">
        <div class="row px-xl-5">
            <div class="col-md-6 pb-4">
                <div class="position-relative bg-secondary text-center text-md-right text-white mb-2 py-5 px-5">
                    <img src="../UsuarioTemplate/img/ca.png" alt="">
                    <div class="position-relative" style="z-index: 1;">
                        <h5 class="text-uppercase text-primary mb-3">20% off em toda loja</h5>
                        <h1 class="mb-4 font-weight-semi-bold">Veja e saiba mais</h1>
                        <a href="Loja.aspx" class="btn btn-outline-primary py-md-2 px-md-3">VEJA</a>
                    </div>
                </div>
            </div>
            <div class="col-md-6 pb-4">
                <div class="position-relative bg-secondary text-center text-md-left text-white mb-2 py-5 px-5">
                    <img src="../UsuarioTemplate/img/img.jpg" alt="">
                    <div class="position-relative" style="z-index: 1;">
                        <h5 class="text-uppercase text-primary mb-3">20% off nos produtos da categoria Lucas Borges</h5>
                        <h1 class="mb-4 font-weight-semi-bold">Vejas os produtos</h1>
                        <a href="Loja.aspx" class="btn btn-outline-primary py-md-2 px-md-3">VEJA</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Offer End -->


    <!-- Subscribe Start -->
    <div class="container-fluid bg-secondary my-5">
        <div class="row justify-content-md-center py-5 px-xl-5">
            <div class="col-md-6 col-12 py-5">
                <div class="text-center mb-2 pb-2">
                    <h2 class="section-title px-5 mb-3"><span class="bg-secondary px-2">Fique por dentro</span></h2>
                    <p>Mais atualizações todas as semanas.</p>
                </div>
                <div action="">
                    <div class="input-group">
                        <input type="text" class="form-control border-white p-4" placeholder="exemplo@exemplo.com">
                        <div class="input-group-append">
                            <button class="btn btn-primary px-4">Se-inscrever</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Subscribe End -->



    <!-- Vendor Start -->
    <div class="container-fluid py-5">
        <div class="row px-xl-5">
            <div class="col">
                <div class="owl-carousel vendor-carousel">
                    <div class="vendor-item border p-4">
                        <img src="../UsuarioTemplate/img/vendor-1.jpg" alt="">
                    </div>
                    <div class="vendor-item border p-4">
                        <img src="../UsuarioTemplate/img/vendor-2.jpg" alt="">
                    </div>
                    <div class="vendor-item border p-4">
                        <img src="../UsuarioTemplate/img/vendor-3.jpg" alt="">
                    </div>
                    <div class="vendor-item border p-4">
                        <img src="../UsuarioTemplate/img/vendor-4.jpg" alt="">
                    </div>
                    <div class="vendor-item border p-4">
                        <img src="../UsuarioTemplate/img/vendor-5.jpg" alt="">
                    </div>
                    <div class="vendor-item border p-4">
                        <img src="../UsuarioTemplate/img/vendor-6.jpg" alt="">
                    </div>
                    <div class="vendor-item border p-4">
                        <img src="../UsuarioTemplate/img/vendor-7.jpg" alt="">
                    </div>
                    <div class="vendor-item border p-4">
                        <img src="../UsuarioTemplate/img/vendor-8.jpg" alt="">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Vendor End -->
</asp:Content>
