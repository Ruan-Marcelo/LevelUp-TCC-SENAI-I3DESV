<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Contato.aspx.cs" Inherits="LevelUp.Admin.Contato" %>
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

      <div class="mb-4">
      <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
  </div>
  <div class="col-sm-12 col-md-12">
      <div class="card">
          <div class="card-body">
              <hr />
              <div class="table-responsive">

                  <asp:Repeater ID="rContato" runat="server" OnItemCommand="rContato_ItemCommand">
                      <HeaderTemplate>
                          <table class="table data-table-export table-hover nowrap">
                              <thead>
                                  <tr>
                                      <th class="table-plus">SrNo</th>
                                      <th>Nome de de usuário</th>
                                      <th>Email </th>
                                      <th>Assunto</th>
                                      <th>Mensagem</th>
                                      <th>Data de criação</th>                                       
                                      <th class="datatable-nosort">Ação</th>
                                  </tr>
                              </thead>
                              <tbody>
                      </HeaderTemplate>
                      <ItemTemplate>
                          <tr>
                              <td class="table-plus"><%# Eval("SrNo") %> </td>
                              <td><%# Eval("Nome") %> </td>
                              <td><%# Eval("Email") %> </td>
                              <td><%# Eval("Assunto") %> </td>
                              <td><%# Eval("Mensagem") %> </td>
                              <td><%# Eval("DataCriacao") %> </td>
                              <td>
                                  <asp:LinkButton ID="lbDeletar" Text="Deletar" runat="server" CssClass="badge badge-danger"
                                      CommandArgument='<%# Eval("ContatoId") %>' CommandName="deletar" CausesValidation="false"
                                      OnClientClick="return confirm('Você tem certeza que deseja excluir esse Contato?');">
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

</asp:Content>
