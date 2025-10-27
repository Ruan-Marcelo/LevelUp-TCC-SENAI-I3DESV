<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LevelUp.Usuario.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>

    <style>
        .contact-form {
            background-color: #fff;
        }

        .contact-form input.form-control {
            border-radius: 8px;
            border: 1px solid #ccc;
            padding: 10px 12px;
            transition: all 0.2s ease-in-out;
        }

        .contact-form input.form-control:focus {
            border-color: #007bff;
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.3);
        }

        .btn-primary {
            border-radius: 8px;
            font-weight: 600;
        }

        .section-title span {
            background-color: #f8f9fa;
            border-radius: 8px;
        }

        .img-thumbnail {
            border: 2px solid #007bff;
        }

        .login-section {
            min-height: 80vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .login-image {
            width: 100%;
            height: 100%;
            object-fit: cover;
            border-radius: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container login-section">
        <div class="row align-items-center justify-content-center">
            
          
            <div class="col-md-6 mb-4 mb-md-0 text-center">
                <img src="https://www.ltvplus.com/wp-content/uploads/2020/12/eCommerce-Trends-featured.png" alt="Login" class="login-image shadow" />
            </div>

        
            <div class="col-md-6 col-lg-4">
                <div class="contact-form p-4 border rounded shadow-sm bg-white">
                    <div class="text-center mb-4">
                        <h2 class="section-title px-5">
                            <span class="px-2">
                                <asp:Label ID="lblHeaderMsg" runat="server" Text="LOGIN"></asp:Label>
                            </span>
                        </h2>
                    </div>

                    <div class="align-self-end mb-3 text-center">
                        <asp:Label ID="lblMsg" runat="server" CssClass="text-danger font-weight-bold"></asp:Label>
                    </div>

                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                            ErrorMessage="Email é obrigatório" ControlToValidate="txtEmail"
                            ForeColor="Red" Display="Dynamic" Font-Size="small" />
                        <asp:TextBox ID="txtEmail" runat="server"
                            CssClass="form-control"
                            placeholder="Digite seu email completo" TextMode="Email" />
                    </div>

                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvSenha" runat="server"
                            ErrorMessage="Senha é obrigatória" ControlToValidate="txtSenha"
                            ForeColor="Red" Display="Dynamic" Font-Size="small" />
                        <asp:TextBox ID="txtSenha" runat="server"
                            CssClass="form-control"
                            placeholder="Digite sua senha" TextMode="Password" />
                    </div>

                    <div class="text-center">
                        <asp:Button ID="BtnLogar" runat="server"
                            Text="Entrar" CssClass="btn btn-primary btn-block py-2" />
                    </div>

                    <div class="text-center mt-3">
                        <asp:Image ID="imgUsuario" runat="server"
                            CssClass="img-thumbnail rounded-circle"
                            Style="display: none; width: 100px; height: 100px; object-fit: cover;" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
