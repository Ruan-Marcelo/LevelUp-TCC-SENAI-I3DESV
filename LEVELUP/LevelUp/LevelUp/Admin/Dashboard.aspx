<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="LevelUp.Admin.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- *************************************************************** -->
 <!-- Start First Cards -->
 <!-- *************************************************************** -->
 <div class="card-group">
     <div class="card border-right">
         <div class="card-body">
             <div class="d-flex d-lg-flex d-md-block align-items-center">
                 <div>
                     <div class="d-inline-flex align-items-center">
                         <h2 id="lblTotalUsuarios" runat="server" class="text-dark mb-1 font-weight-medium">10</h2>
                     </div>
                     <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Novos Clientes</h6>
                 </div>
                 <div class="ml-auto mt-md-3 mt-lg-0">
                     <span class="opacity-7 text-muted"><i data-feather="user-plus"></i></span>
                 </div>
             </div>
         </div>
     </div>
     <div class="card border-right">
         <div class="card-body">
             <div class="d-flex d-lg-flex d-md-block align-items-center">
                 <div>
                     <h2 class="text-dark mb-1 w-100 text-truncate font-weight-medium"><sup
                             class="set-doller">R$</sup>100,00</h2>
                     <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Ganho Bruto
                     </h6>
                 </div>
                 <div class="ml-auto mt-md-3 mt-lg-0">
                     <span class="opacity-7 text-muted"><i data-feather="dollar-sign"></i></span>
                 </div>
             </div>
         </div>
     </div>
     <div class="card border-right">
         <div class="card-body">
             <div class="d-flex d-lg-flex d-md-block align-items-center">
                 <div>
                     <div class="d-inline-flex align-items-center">
                         <h2 class="text-dark mb-1 font-weight-medium">10</h2>
                     </div>
                     <h6 class="text-muted font-weight-normal mb-0 w-100 text-truncate">Produtos</h6>
                 </div>
                 <div class="ml-auto mt-md-3 mt-lg-0">
                     <span class="opacity-7 text-muted"><i data-feather="file-plus"></i></span>
                 </div>
             </div>
         </div>
     </div>
 </div>
 <!-- *************************************************************** -->
 <!-- End First Cards -->
 <!-- *************************************************************** -->
 <!-- *************************************************************** -->
 <!-- Start Sales Charts Section -->
 <!-- *************************************************************** -->
 <div class="row">
     <div class="col-lg-4 col-md-12">
         <div class="card">
             <div class="card-body">
                 <h4 class="card-title">Gasto por categoria</h4>
                 <div id="campaign-v2" class="mt-2" style="height:283px; width:100%;"></div>
                 <ul class="list-style-none mb-0">
                     <li>
                         <i class="fas fa-circle text-primary font-10 mr-2"></i>
                         <span class="text-muted">Direct Sales</span>
                         <span class="text-dark float-right font-weight-medium">Categoria 1</span>
                     </li>
                     <li class="mt-3">
                         <i class="fas fa-circle text-danger font-10 mr-2"></i>
                         <span class="text-muted">Referral Sales</span>
                         <span class="text-dark float-right font-weight-medium">Categoria 2</span>
                     </li>
                     <li class="mt-3">
                         <i class="fas fa-circle text-cyan font-10 mr-2"></i>
                         <span class="text-muted">Affiliate Sales</span>
                         <span class="text-dark float-right font-weight-medium">Categoria 3</span>
                     </li>
                 </ul>
             </div>
         </div>
     </div>
     <div class="col-lg-4 col-md-12">
         <div class="card">
             <div class="card-body">
                 <h4 class="card-title">Net Income</h4>
                 <div class="net-income mt-4 position-relative" style="height:294px;"></div>
                 <ul class="list-inline text-center mt-5 mb-2">
                     <li class="list-inline-item text-muted font-italic">Gasto por mês</li>
                 </ul>
             </div>
         </div>
     </div>
 </div>
</asp:Content>
