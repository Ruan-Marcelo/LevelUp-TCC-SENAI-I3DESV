<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="LevelUp.Usuario.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Page Header Start -->
    <div class="container-fluid bg-secondary mb-5">
        <div class="d-flex flex-column align-items-center justify-content-center" style="min-height: 300px">
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Finalizar Compra</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="Padrao.aspx">Início</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Finalizar Compra</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->


    <!-- Checkout Start -->
    <div class="container-fluid pt-5">
        <div class="row px-xl-5">
            <div class="col-lg-8">
                <div class="mb-4">
                    <h4 class="font-weight-semi-bold mb-4">Endereço de Cobrança</h4>
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>Nome</label>
                            <input class="form-control" type="text" placeholder="João">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Sobrenome</label>
                            <input class="form-control" type="text" placeholder="Silva">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>E-mail</label>
                            <input class="form-control" type="text" placeholder="exemplo@email.com">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Telefone</label>
                            <input class="form-control" type="text" placeholder="(00) 00000-0000">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Endereço — Linha 1</label>
                            <input class="form-control" type="text" placeholder="Rua Exemplo, 123">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Endereço — Linha 2</label>
                            <input class="form-control" type="text" placeholder="Complemento">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>País</label>
                            <select class="custom-select">
                                <option selected>Brasil</option>
                                <option>Argentina</option>
                                <option>Portugal</option>
                                <option>Estados Unidos</option>
                            </select>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Cidade</label>
                            <input class="form-control" type="text" placeholder="São Paulo">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Estado</label>
                            <input class="form-control" type="text" placeholder="SP">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>CEP</label>
                            <input class="form-control" type="text" placeholder="00000-000">
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="newaccount">
                                <label class="custom-control-label" for="newaccount">Criar uma conta</label>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="shipto">
                                <label class="custom-control-label" for="shipto" data-toggle="collapse" data-target="#shipping-address">Enviar para outro endereço</label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="collapse mb-4" id="shipping-address">
                    <h4 class="font-weight-semi-bold mb-4">Endereço de Entrega</h4>
                    <div class="row">
                        <div class="col-md-6 form-group">
                            <label>Nome</label>
                            <input class="form-control" type="text" placeholder="João">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Sobrenome</label>
                            <input class="form-control" type="text" placeholder="Silva">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>E-mail</label>
                            <input class="form-control" type="text" placeholder="exemplo@email.com">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Telefone</label>
                            <input class="form-control" type="text" placeholder="(00) 00000-0000">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Endereço — Linha 1</label>
                            <input class="form-control" type="text" placeholder="Rua Exemplo, 123">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Endereço — Linha 2</label>
                            <input class="form-control" type="text" placeholder="Complemento">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>País</label>
                            <select class="custom-select">
                                <option selected>Brasil</option>
                                <option>Argentina</option>
                                <option>Portugal</option>
                                <option>Estados Unidos</option>
                            </select>
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Cidade</label>
                            <input class="form-control" type="text" placeholder="São Paulo">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>Estado</label>
                            <input class="form-control" type="text" placeholder="SP">
                        </div>
                        <div class="col-md-6 form-group">
                            <label>CEP</label>
                            <input class="form-control" type="text" placeholder="00000-000">
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card border-secondary mb-5">
                    <div class="card-header bg-secondary border-0">
                        <h4 class="font-weight-semi-bold m-0">Resumo do Pedido</h4>
                    </div>
                    <div class="card-body">
                        <h5 class="font-weight-medium mb-3">Produtos</h5>
                        <div class="d-flex justify-content-between">
                            <p>teste</p>
                            <p>teste</p>
                        </div>                      
                    </div>
                    <div class="card-footer border-secondary bg-transparent">
                        <div class="d-flex justify-content-between mt-2">
                            <h5 class="font-weight-bold">Total</h5>
                            <h5 class="font-weight-bold">R$ 0</h5>
                        </div>
                    </div>
                </div>

                <div class="card border-secondary mb-5">
                    <div class="card-header bg-secondary border-0">
                        <h4 class="font-weight-semi-bold m-0">Pagamento</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <div class="custom-control custom-radio">
                                <input type="radio" class="custom-control-input" name="payment" id="paypal">
                                <label class="custom-control-label" for="paypal">PayPal</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="custom-control custom-radio">
                                <input type="radio" class="custom-control-input" name="payment" id="directcheck">
                                <label class="custom-control-label" for="directcheck">Cheque Direto</label>
                            </div>
                        </div>
                        <div class="">
                            <div class="custom-control custom-radio">
                                <input type="radio" class="custom-control-input" name="payment" id="banktransfer">
                                <label class="custom-control-label" for="banktransfer">Transferência Bancária</label>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer border-secondary bg-transparent">
                        <button class="btn btn-lg btn-block btn-primary font-weight-bold my-3 py-3">Finalizar Pedido</button>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <!-- Checkout End -->

</asp:Content>
