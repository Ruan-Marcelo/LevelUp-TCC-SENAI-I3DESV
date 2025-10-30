<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Carrinho.aspx.cs" Inherits="LevelUp.Usuario.Carrinho" %>

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

    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Carrinho</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="Padrao.aspx">Início</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Carrinho de compras</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
    </div>


    <!-- Cart Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <div class="col-lg-8 table-responsive mb-5">
                <table class="table table-bordered text-center mb-0">
                    <thead class="bg-secondary text-dark">
                        <tr>
                            <th>Nome</th>
                            <th>Preço</th>
                            <th>Quantidade</th>
                            <th>Total</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody class="align-middle">
                        <asp:Repeater ID="rCarrinho" runat="server" OnItemCommand="rCarrinho_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="align-middle">
                                        <img src='<%# LevelUp.Utils.getImagemUrl(Eval("ImagemUrl")) %>' alt="" style="width: 50px;">
                                        <%# Eval("ProdutoNome") %>
                                    </td>
                                    <td class="align-middle">R$ <%# Eval("Preco") %></td>
                                    <td class="align-middle">
                                        <div class="input-group quantity mx-auto" style="width: 100px;">
                                            <asp:LinkButton ID="btnMinus" runat="server" CommandName="Decrease" CommandArgument='<%# Eval("ProdutoId") %>' CssClass="btn btn-sm btn-primary btn-minus">
                            <i class="fa fa-minus"></i>
                                            </asp:LinkButton>

                                            <input type="text" class="form-control form-control-sm bg-secondary text-center"
                                                value='<%# Eval("Qtd") %>' readonly>

                                            <asp:LinkButton ID="btnPlus" runat="server" CommandName="Increase" CommandArgument='<%# Eval("ProdutoId") %>' CssClass="btn btn-sm btn-primary btn-plus">
                            <i class="fa fa-plus"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </td>
                                    <td class="align-middle">R$ <%# Convert.ToDecimal(Eval("Preco")) * Convert.ToInt32(Eval("Qtd")) %></td>
                                    <td class="align-middle">
                                        <asp:LinkButton ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ProdutoId") %>' CssClass="btn btn-sm btn-primary">
                        <i class="fa fa-times"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>

                </table>
            </div>
            <div class="col-lg-4">
                <div class="card border-secondary mb-5">
                    <div class="card-header bg-secondary border-0">
                        <h4 class="font-weight-semi-bold m-0">Resumo do Carrinho</h4>
                    </div>
                    <div class="card-body">
                        <div class="d-flex justify-content-between mb-3 pt-1">
                            <h6 class="font-weight-medium">Subtotal</h6>
                            <h6 class="font-weight-medium">R$
                                <asp:Label ID="lblSubtotal" runat="server">0</asp:Label></h6>
                        </div>
                        <div class="d-flex justify-content-between">
                            <h6 class="font-weight-medium">Frete</h6>
                            <h6 class="font-weight-medium">Grátis</h6>
                        </div>
                    </div>
                    <div class="card-footer border-secondary bg-transparent">
                        <div class="d-flex justify-content-between mt-2">
                            <h5 class="font-weight-bold">Total</h5>
                            <h5 class="font-weight-bold">R$
                                <asp:Label ID="lblTotal" runat="server">0</asp:Label></h5>
                        </div>
                        <asp:Button ID="btnCheckout" runat="server" CssClass="btn btn-block btn-primary my-3 py-3" Text="Finalizar Compra" OnClick="btnCheckout_Click" />
                    </div>
                </div>
            </div>

        </div>
    </div>
    <!-- Cart End -->

</asp:Content>
