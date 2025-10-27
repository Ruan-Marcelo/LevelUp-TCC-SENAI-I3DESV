<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LevelUp.Admin.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background: linear-gradient(135deg, #6e8efb, #a777e3);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        section {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }
        .login-card {
            background: #fff;
            border-radius: 15px;
            box-shadow: 0 8px 25px rgba(0,0,0,0.2);
            padding: 40px 30px;
        }
        .login-card h2 {
            text-align: center;
            font-weight: bold;
            margin-bottom: 25px;
            color: #4b4b4b;
        }
        .form-outline input {
            border-radius: 10px;
            border: 1px solid #ccc;
            padding: 12px;
        }
        .form-outline label {
            font-weight: 500;
            color: #555;
        }
        .btn-login {
            background: linear-gradient(135deg, #6e8efb, #a777e3);
            border: none;
            color: #fff;
            font-weight: bold;
            padding: 12px;
            border-radius: 10px;
            width: 100%;
            transition: 0.3s;
        }
        .btn-login:hover {
            background: linear-gradient(135deg, #5b76e1, #9262d9);
        }
        .login-img {
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.15);
        }
        .text-center a {
            color: #6e8efb;
            font-weight: 500;
        }
        .text-center a:hover {
            color: #a777e3;
            text-decoration: none;
        }
        @media (max-width: 768px) {
            .login-card {
                padding: 30px 20px;
            }
            .login-img {
                margin-bottom: 20px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="container">
            <div class="row justify-content-center align-items-center">
                <div class="col-lg-6 col-md-8">
                    <div class="login-card">
                        <h2>Login Administrativo</h2>

                        <div class="form-outline mb-3">
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control form-control-lg"
                                         placeholder="Seu nome de usuário registrado"></asp:TextBox>
                            <label class="form-label" for="txtUsuario">Nome de Usuário</label>
                        </div>

                        <div class="form-outline mb-4">
                            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control form-control-lg"
                                         placeholder="Sua senha"></asp:TextBox>
                            <label class="form-label" for="txtSenha">Senha</label>
                        </div>

                        <%--<asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn-login" OnClick="btnLogin_Click" />--%>

                        <div class="text-center mt-3">
                            <p class="small fw-bold mt-2">
                                Não possui conta?
                                <a href="RegistrarAdmin.aspx">Registrar</a>
                            </p>
                        </div>

                        <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
                    </div>
                </div>

                <div class="col-lg-6 d-none d-lg-block text-center">
                    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.webp"
                         class="login-img img-fluid" alt="Imagem de login" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
