<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Contato.aspx.cs" Inherits="LevelUp.Usuario.Contato" %>

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
            <h1 class="font-weight-semi-bold text-uppercase mb-3">Entre me contato</h1>
            <div class="d-inline-flex">
                <p class="m-0"><a href="Padrao.aspx">Início</a></p>
                <p class="m-0 px-2">-</p>
                <p class="m-0">Contato</p>
            </div>
        </div>
    </div>
    <!-- Page Header End -->

    <div class="align-self-end mb-3 text-center">
        <asp:Label ID="lblMsg" runat="server" CssClass="text-danger font-weight-bold"></asp:Label>
    </div>
    <!-- Contact Start -->
    <div class="container-fluid pt-5">
        <div class="text-center mb-4">
            <h2 class="section-title px-5"><span class="px-2">Entre em contato conosco</span></h2>
        </div>
        <div class="row px-xl-5">
            <div class="col-lg-7 mb-5">
                <div class="contact-form">
                    <div id="success"></div>

                    <div class="control-group mb-3">
                        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Seu nome"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNome" runat="server" ErrorMessage="O nome é obrigatório"
                            ControlToValidate="txtNome" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>

                    <div class="control-group mb-3">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Seu melhor E-mail" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="O E-mail é obrigatório"
                            ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>

                    <div class="control-group mb-3">
                        <asp:TextBox ID="txtAssunto" runat="server" CssClass="form-control" placeholder="Assunto"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAssunto" runat="server" ErrorMessage="O Assunto é obrigatório"
                            ControlToValidate="txtAssunto" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>

                    <div class="control-group mb-3">
                        <asp:TextBox ID="txtMensagem" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Sua mensagem"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMensagem" runat="server" ErrorMessage="A mensagem é obrigatória"
                            ControlToValidate="txtMensagem" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>

                    <div class="text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Enviar" CssClass="btn btn-warning rounded-pill px-4 py-2"
                            OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>

            <div class="col-lg-5 mb-5">
                <h5 class="font-weight-semi-bold mb-3">Sobre nós</h5>
                <p>Somos uma equipe apaixonada por inovação, tecnologia e soluções criativas. Nosso objetivo é transformar ideias em projetos reais que facilitem a vida das pessoas e gerem resultados de verdade.</p>

                <div class="d-flex flex-column mb-3">
                    <h5 class="font-weight-semi-bold mb-3">Contato 1</h5>
                    <p class="mb-2"><i class="fa fa-map-marker-alt text-primary mr-3"></i>Rua Exemplo, 123 — São Paulo, SP — Brasil</p>
                    <p class="mb-2"><i class="fa fa-envelope text-primary mr-3"></i>contato@empresa.com.br</p>
                    <p class="mb-2"><i class="fa fa-phone-alt text-primary mr-3"></i>(11) 91234-5678</p>
                </div>

                <div class="d-flex flex-column">
                    <h5 class="font-weight-semi-bold mb-3">Contato 2</h5>
                    <p class="mb-2"><i class="fa fa-map-marker-alt text-primary mr-3"></i>Avenida Central, 456 — Rio de Janeiro, RJ — Brasil</p>
                    <p class="mb-2"><i class="fa fa-envelope text-primary mr-3"></i>suporte@empresa.com.br</p>
                    <p class="mb-0"><i class="fa fa-phone-alt text-primary mr-3"></i>(21) 3344-5566</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Contact End -->


</asp:Content>
