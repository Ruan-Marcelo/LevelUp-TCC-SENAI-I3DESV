<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="LevelUp.Admin.Categoria" %>

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
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%= imagemPreview.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
    </div>

    <div class="row">
        <div class="col-sm-12 col-md-4">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Categoria</h4>
                    <hr />

                    <div class="form-body">

                        <label>Categoria Nome:</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCategoriaNome" runat="server" CssClass="form-control" placeholder="Digite o nome da categoria"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCategoriaNome" runat="server" ForeColor="Red" FontSize="Small"
                                        display="Dynamic" SetFocusOnError="True" ControlToValidate="txtCategoriaNome"
                                        ErrorMessage="O nome da categoria é obrigatório." CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <label>Categoria Image:</label>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:FileUpload ID="fuCategoriaImagem" runat="server" CssClass="form-control" 
                                        onchange ="ImagemPreview(this);"/>
                                    <asp:HiddenField ID="hfCategoriaId" runat="server" Value="0" />
                                </div>
                            </div>
                        </div>

                       <div class="row">
                          <div class="col-md-12">
                             <div class="form-group">
                                 <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp;  Está Ativo" />
                             </div>                              
                        </div>   
                     </div> 

                </div>

                    <div class="form-adction pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Adicionar" OnClick="btnAddOrUpdate_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Limpar" OnClick="btnClear_Click" />
                        </div>
                    </div>

                    <div>
                        <asp:Image ID="imagemPreview" runat="server" CssClass="img-thumbail" AlternateText="" />
                    </div>

            </div>
        </div>
 </div>
        <div class="col-sm-12 col-md-8">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Listas de Categoria</h4>
                    <hr />
                    <div class="table-responsive">
                        <asp:Repeater ID="rCategoria" runat="server" onItemCommand="rCategoria_ItemCommand">
                            <HeaderTemplate>
                                <table class="table data-table-export table-hover nowrap">
                                    <thead>
                                        <tr>
                                            <th class="table-plus">Nome</th>
                                            <th>Imagem</th>
                                            <th>Está ativo</th>
                                            <th>Data de criação</th>
                                            <th class="datatable-nosort"> Ação</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="table-plus"> <%# Eval("CategoriaNome") %> </td>
                                    <td>
                                        <img width="40" src="<%# LevelUp.Utils.getImagemUrl( Eval("CategoriaImgUrl")) %>" alt="imagem" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEstaAtivo" runat="server"
                                           Text='<%# (bool)Eval("EstaAtivo") ? "Ativo" : "Inativo" %>'
                                            CssClass='<%# (bool)Eval("EstaAtivo") ? "badge badge-success" : "badge badge-danger" %>'>                                           
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("DataCriacao") %>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbEdit" Text="Editar" runat="server" CssClass="badge badge-primary"
                                            CommandArgument='<%# Eval("CategoriaId") %>' CommandName="editar" CausesValidation="false">
                                            <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lbDeletar" Text="Deletar" runat="server" CssClass="badge badge-danger" 
                                             CommandArgument='<%# Eval("CategoriaId") %>' CommandName="deletar" CausesValidation="false"
                                             OnClientClick="return confirm('Você tem certeza qu deseja excluir essa categoria?');">
                                            <i class="fas fa-trash-alt"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
