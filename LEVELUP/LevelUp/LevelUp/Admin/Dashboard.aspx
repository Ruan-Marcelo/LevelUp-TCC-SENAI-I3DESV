<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="LevelUp.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
          
            // linha
            var ctx2 = document.getElementById('chartMensal').getContext('2d');
            var chartMensal = new Chart(ctx2, {
                type: 'line',
                data: {
                    labels: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun'],
                    datasets: [{
                        label: 'Gastos',
                        data: [500, 800, 600, 780, 950, 1200] //exemplos de gastos por mês , depois futuramente só integrar com um procedimento de armazenamento dos pedidos_CRUD
                    }]
                }
            });

        });
    </script>

    <style>
        .card-icon-bg {
            position: relative;
            overflow: hidden;
            text-align: center;
            padding: 20px;
            border-radius: 15px;
            transition: transform .3s, box-shadow .3s;
            cursor: pointer;
        }

            .card-icon-bg i {
                font-size: 42px;
                margin-bottom: 10px;
                display: block;
                transition: transform .3s;
            }

            .card-icon-bg:hover i {
                transform: scale(1.15);
            }

            .card-icon-bg:hover {
                transform: translateY(-5px);
                box-shadow: 0 15px 25px rgba(0,0,0,0.12);
            }

        .card-icon-bg-primary {
            background: linear-gradient(135deg,#007bff,#0056b3);
            color: #fff;
        }

        .card-icon-bg-success {
            background: linear-gradient(135deg,#28a745,#1e7e34);
            color: #fff;
        }

        .card-icon-bg-warning {
            background: linear-gradient(135deg,#ffc107,#e0a800);
            color: #fff;
        }

        .card-icon-bg-danger {
            background: linear-gradient(135deg,#dc3545,#b21f2d);
            color: #fff;
        }

        .card-icon-bg-info {
            background: linear-gradient(135deg,#17a2b8,#117a8b);
            color: #fff;
        }

        .card-icon-bg h4,
        .card-icon-bg p {
            color: #fff !important;
        }

        .card-details-link {
            margin-top: 12px;
            display: block;
        }

            .card-details-link a {
                color: #ffffff;
                font-size: 14px;
                font-weight: 600;
                text-decoration: none;
                transition: .3s ease;
                display: inline-flex;
                align-items: center;
                gap: 6px;
            }

                .card-details-link a:hover {
                    opacity: 0.85;
                    transform: translateX(4px);
                }

            .card-details-link i {
                transition: .3s ease;
            }

            .card-details-link a:hover i {
                transform: translateX(3px);
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card-group">
        <div class="row">

            <div class="col-md-3 stretch-card grid-margin">
                <div class="card card-icon-bg card-icon-bg-primary">
                    <div class="card-body">
                        <i class="fas fa-tag"></i>
                        <h4 class="mb-0"><% Response.Write(Session["categoria"]); %></h4>
                        <p class="text-muted">Categorias</p>
                        <div class="card-details-link">
                            <a href="Categoria.aspx">Veja os detalhes <i class="fas fa-eye"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-md-3 stretch-card grid-margin">
                <div class="card card-icon-bg card-icon-bg-success">
                    <div class="card-body">
                        <i class="fas fa-tags"></i>
                        <h4 class="mb-0">
                            <% Response.Write(Session["subCategoria"]); %></h4>
                        <p class="text-muted">Sub Categorias</p>
                        <div class="card-details-link">
                            <a href="SubCategoria.aspx">Veja os detalhes <i class="fas fa-eye"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-md-3 stretch-card grid-margin">
                <div class="card card-icon-bg card-icon-bg-warning">
                    <div class="card-body">
                        <i class="fas fa-users"></i>
                        <h4 class="mb-0">
                            <% Response.Write(Session["usuario"]); %></h4>
                        <p class="text-muted">Usuários</p>
                        <div class="card-details-link">
                            <a href="Usuarios.aspx">Veja os detalhes <i class="fas fa-eye"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-md-3 stretch-card grid-margin">
                <div class="card card-icon-bg card-icon-bg-danger">
                    <div class="card-body">
                        <i class="fas fa-phone"></i>
                        <h4 class="mb-0">
                            <% Response.Write(Session["contato"]); %></h4>
                        <p class="text-muted">Contatos</p>
                        <div class="card-details-link">
                            <a href="Contato.aspx">Veja os detalhes <i class="fas fa-eye"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-md-3 stretch-card grid-margin">
                <div class="card card-icon-bg card-icon-bg-info">
                    <div class="card-body">
                        <i class="fas fa-box"></i>
                        <h4 class="mb-0">
                            <% Response.Write(Session["produto"]); %></h4>
                        <p class="text-muted">Produtos</p>
                        <div class="card-details-link">
                            <a href="ProdutoLista.aspx">Veja os detalhes <i class="fas fa-eye"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>

            <%--<div class="col-md-3 stretch-card grid-margin">
                <div class="card card-icon-bg card-icon-bg-info">
                    <div class="card-body">
                        <i class="fas fa-usd"></i>
                        <h4 class="mb-0">
                            <% Response.Write(Session["valorTotal"]); %></h4>
                        <p class="text-muted">Total vendido</p>
                        <div class="card-details-link">
                            <a href="Dashboard.aspx">Veja os detalhes <i class="fas fa-eye"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>--%>
        </div>

    </div>
    <div class="row">
        <div class="col-lg-6 col-md-12">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Vendas Mensais</h4>
                    <canvas id="chartMensal" style="height: 294px;"></canvas>
                    <ul class="list-inline text-center mt-5 mb-2">
                        <li class="list-inline-item text-muted font-italic">Gasto por mês</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
