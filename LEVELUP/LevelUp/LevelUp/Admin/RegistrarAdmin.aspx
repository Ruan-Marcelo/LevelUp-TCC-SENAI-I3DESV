<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RegistrarAdmin.aspx.cs" Inherits="LevelUp.Admin.RegistrarAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
    window.onload = function () {
        var seconds = 5;
        setTimeout(function () {
            document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
      }, seconds * 1000);
    };
</script>
<script>
    function ImagemPreview(input) {
        if (input.files && input.files[0]) {
            $('#<%= imgAdmin.ClientID%>').show();
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#<%= imgAdmin.ClientID%>').prop('src', e.target.result)
                    .width(200)
                    .height(200);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container pt-5">
        <div class="align-self-end mb-4">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>

        </div>
        <div class="text-center mb-5 pt-4">
            <h2 class="section-title px-5">
                <span class="px-2">
                 <asp:Label ID="lblHeaderMsg" runat="server" Text="Registrar Usuário"></asp:Label>
                </span>
            </h2>
        </div>
        <div class="row px-xl-5">
            <div class="col-md-6">
                <div class="contact-form">
                    <div id="success"></div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvNome" runat="server" ErrorMessage="Nome é obrigatorio" ControlToValidate="txtNome"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" placeholder="Digite seu nome completo"
                            ToolTipe="Nome completo"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="reNome" runat="server" ErrorMessage="Apenas Letras" ForeColor="Red"
                            Display="Dynamic" SetFocusOnError="true" Font-Size="small" ControlToValidate="txtNome"
                            ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvNomeUser" runat="server" ErrorMessage="Nome de usuario é obrigatorio" ControlToValidate="txtNomeUser"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtNomeUser" runat="server" CssClass="form-control" placeholder="Digite seu nome de usuario completo"
                            ToolTipe="Nome de usuario"></asp:TextBox>
                    </div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email é obrigatorio" ControlToValidate="txtEmail"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Digite seu Email completo" TextMode="Email"
                            ToolTipe="Email"></asp:TextBox>
                    </div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvCelular" runat="server" ErrorMessage="celular não é obrigatório" ControlToValidate="txtCelular"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control" placeholder="Digite seu celular"
                            ToolTipe="Numero de celular"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="reCelular" runat="server" ErrorMessage="O número de celular deve ter 10 digitos" ForeColor="Red"
                            Display="Dynamic" SetFocusOnError="true" Font-Size="small" ControlToValidate="txtCelular"
                            ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="contact-form">
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvEndereco" runat="server" ErrorMessage="Endereço é obrigatorio" ControlToValidate="txtEndereco"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control" placeholder="Digite seu Endereço" TextMode="MultiLine"
                            ToolTipe="Endereço"></asp:TextBox>
                    </div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvCodigoPostal" runat="server" ErrorMessage="Codigo postal é obrigatorio" ControlToValidate="txtCodigoPostal"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Digite seu codigo postal"
                            ToolTipe="Codigo postal"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Apenas 10 digitos" ForeColor="Red"
                            Display="Dynamic" SetFocusOnError="true" Font-Size="small" ControlToValidate="txtCodigoPostal"
                            ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="pb-3">
                        <asp:FileUpload ID="fuimgAdmin" runat="server" CssClass="form-control" ToolTip="Imagem Usuario" onchange="ImagemPreview(this);" />
                    </div>
                    <div class="pb-3">
                        <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ErrorMessage="Senha é obrigatório" ControlToValidate="txtSenha"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="small"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" placeholder="Digite sua senha" TextMode="Password"
                            ToolTipe="Senha"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row pl-4 mb-5">
                <div class="ml-2">
                    <asp:Button ID="btnRegistrarOuAtualizar" runat="server" Text="Registrar" CssClass="btn btn-primary py-2 px-4"
                        OnClick="btnRegistrarOuAtualizar_Click" />                  
                </div>
                <asp:Image ID="imgAdmin" runat="server" CssClass="img-thumbnail ml-2 mt-2" AlternateText="image" Style="display: none;" />
            </div>

        </div>
    </div>
</asp:Content>
