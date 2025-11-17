<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario/Usuario.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="LevelUp.Usuario.Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tag-categoria {
            display: inline-block;
            padding: 4px 12px;
            background: rgba(0, 255, 255, 0.1);
            color: #00eaff;
            border: 1px solid rgba(0, 255, 255, 0.4);
            border-radius: 6px;
            font-size: 13px;
            font-weight: 700;
            text-transform: uppercase;
            box-shadow: 0 0 6px #00eaff66;
        }

        .profile-card {
            background: #fff;
            border-radius: 18px;
            box-shadow: 0 4px 18px rgba(0,0,0,0.12);
            padding: 25px 10px;
            text-align: center;
            transition: .3s;
        }

            .profile-card:hover {
                transform: translateY(-4px);
                box-shadow: 0 6px 28px rgba(0,0,0,0.18);
            }

        .profile-pic {
            width: 110px !important;
            height: 110px !important;
            border-radius: 50%;
            border: 4px solid #e1e1e1;
            object-fit: cover;
        }

        .profile-name {
            font-size: 20px;
            font-weight: 700;
            margin-top: 10px;
        }

        .profile-username {
            font-size: 14px;
            color: #777;
        }

        .profile-card .btn {
            border-radius: 10px;
            font-weight: 600;
        }

        .profile-stats {
            display: flex;
            justify-content: space-around;
            padding: 15px 0;
            border-top: 1px solid #eee;
            background: #f8f8f8;
        }

            .profile-stats div {
                text-align: center;
            }

        .stat-number {
            display: block;
            font-size: 18px;
            font-weight: 700;
        }

        .search-box {
            position: relative;
            width: 90%;
            margin-bottom: 25px;
        }

            .search-box input {
                width: 100%;
                padding: 10px 15px;
                border: 1px solid #ccc;
                border-radius: 25px;
                outline: none;
                font-size: 14px;
            }

                .search-box input:focus {
                    border-color: #007bff;
                    box-shadow: 0px 0px 6px rgba(0, 123, 255, 0.4);
                }

        #btnPesquisar {
            padding: 6px 12px;
            font-size: 14px;
            height: 38px;
            border-radius: 20px;
        }

        #lblMensagemPesquisa {
            display: block;
            margin-top: 8px;
            text-align: center;
            color: #555;
            font-style: italic;
            font-size: 14px;
        }


        .search-btn {
            position: absolute;
            right: 15px;
            top: 50%;
            transform: translateY(-50%);
            border: none;
            background: none;
            font-size: 18px;
            cursor: pointer;
            color: #007bff;
        }

            .search-btn:hover {
                color: #0056b3;
            }

        .create-post-card {
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 12px;
            border: 1px solid #ddd;
            background: #fff;
            box-shadow: 0 3px 10px rgba(0,0,0,0.15);
        }

            .create-post-card input,
            .create-post-card textarea,
            .create-post-card select {
                width: 100%;
                margin-bottom: 10px;
                border-radius: 6px;
                border: 1px solid #ccc;
                padding: 10px;
            }

        #btnCriarPost {
            width: 100%;
            height: 42px;
            margin-bottom: 15px;
            border-radius: 10px;
        }

        .post-card {
            background: #fff;
            padding: 15px;
            border-radius: 12px;
            margin-bottom: 18px;
            box-shadow: 0 4px 16px rgba(0,0,0,0.12);
        }

        .post-header img {
            border-radius: 50%;
            object-fit: cover;
        }

        .post-author {
            font-size: 15px;
            font-weight: bold;
        }

        .post-content h5 {
            font-size: 18px;
            font-weight: bold;
        }

        .post-content p {
            font-size: 15px;
        }

        .post-main-img {
            width: 100%;
            height: 380px;
            border-radius: 12px;
            object-fit: cover;
            margin-top: 12px;
            box-shadow: 0 3px 10px rgba(0,0,0,0.15);
        }

        .btnCurtir {
            padding: 5px 10px;
            border: 1px solid #007bff;
            background: transparent;
            color: #007bff;
            cursor: pointer;
            border-radius: 5px;
            transition: 0.2s;
        }

            .btnCurtir.curtido {
                background: #007bff;
                color: #fff;
            }

        .comment-section {
            margin-top: 10px;
            padding-top: 8px;
            border-top: 1px solid #eee;
        }

            .comment-section .comments-list div {
                padding: 5px 0;
                font-size: 14px;
            }

        .comment-input {
            width: 100%;
            padding: 8px 10px;
            margin-top: 5px;
            border-radius: 6px;
            border: 1px solid #ccc;
        }

        .btn-send-comment {
            margin-top: 4px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" CssClass="h3"></asp:Label>
    </div>

    <div class="search-top mb-3">
        <div class="search-box w-100 d-flex justify-content-center">
            <div class="d-flex flex-column" style="width: 60%; max-width: 700px;">
                <div class="d-flex">
                    <asp:TextBox ID="txtPesquisar" runat="server" CssClass="form-control" placeholder="Pesquisar..." />
                    <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-primary ms-2" OnClick="btnPesquisar_Click" />
                </div>
                <asp:Label ID="lblMensagemPesquisa" runat="server" CssClass="text-muted" Visible="false"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            <!-- Perfil -->
            <div class="card-reactor profile-card text-center">
                <asp:Panel ID="pnlLogado" runat="server" Visible="false">
                    <asp:Image ID="imgPerfil" runat="server" CssClass="profile-pic mt-3" Width="100px" Height="100px" />
                    <div class="card-body">
                        <h5 class="profile-name">
                            <asp:Label ID="lblNome" runat="server" />
                        </h5>
                        <span class="profile-username">@<asp:Label ID="lblNomeUsuario" runat="server" /></span>
                    </div>
                    <div class="profile-stats">
                        <div>
                            <span class="stat-number">
                                <asp:Label ID="lblPosts" runat="server" Text="0"></asp:Label></span>
                            Posts
                        </div>
                        <div>
                            <span class="stat-number">
                                <asp:Label ID="lblSeguidores" runat="server" Text="0"></asp:Label></span> Seguidores
                        </div>
                        <div>
                            <span class="stat-number">
                                <asp:Label ID="lblSeguindo" runat="server" Text="0"></asp:Label></span> Seguindo
                        </div>
                    </div>
                    <asp:Button ID="lblResgitrarOrPerfil" runat="server" Text="Perfil" CssClass="btn btn-sm btn-outline-primary mt-2 w-100" OnClick="lblResgitrarOrPerfil_Click" />
                </asp:Panel>
            </div>
        </div>

        <div class="col-md-6">
            <!-- Botão para abrir/fechar o formulário -->
            <button type="button" id="btnCriarPost" class="btn btn-primary mb-2">Criar Post</button>

            <asp:Panel ID="formCriarPost" runat="server" CssClass="card-reactor create-post-card" Style="display: none;">
                <asp:HiddenField ID="hfEditPostId" runat="server" />
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control mb-2" placeholder="Título" />
                <asp:TextBox ID="txtConteudo" runat="server" TextMode="MultiLine" CssClass="form-control mb-2" placeholder="Escreva algo..." />

                <label class="btn btn-light mb-2" for="<%= FileUpload1.ClientID %>">
                    <i class="fas fa-image"></i>Selecionar Foto
                </label>
                <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                    <asp:ListItem Text="Selecione uma categoria" Value="" />
                </asp:DropDownList>
                <asp:FileUpload ID="FileUpload1" runat="server" Style="display: none;" />

                <asp:Image ID="imgPreviewPost" runat="server" Width="100%" Height="250px" Style="display: none; object-fit: cover; margin-bottom: 10px; border-radius: 6px;" />

                <asp:Button ID="btnCriar" runat="server" Text="Postar" CssClass="btn btn-primary mt-2 w-100" OnClick="btnCriar_Click" />
            </asp:Panel>


            <asp:Repeater ID="rptPosts" runat="server" OnItemDataBound="rptPosts_ItemDataBound" OnItemCommand="rptPosts_ItemCommand">
                <ItemTemplate>
                    <div class="post-card">
                        <asp:PlaceHolder ID="phOwnerButtons" runat="server">
                            <div class="mt-2">
                                <asp:Button ID="btnExcluirPost" runat="server" Text="Excluir" CssClass="btn btn-sm btn-danger"
                                    CommandName="DeletePost" CommandArgument='<%# Eval("PostId") %>'
                                    OnClientClick="return confirm('Tem certeza que deseja excluir este post?');" />
                            </div>
                        </asp:PlaceHolder>

                        <div class="post-header d-flex justify-content-between">
                            <div class="post-author"><%# Eval("NomeDeUsuario") %></div>
                        </div>

                        <div class="post-content mt-2">
                            <h5><%# Eval("Titulo") %></h5>
                            <p><%# Eval("Conteudo") %></p>
                            <div class="post-category mb-1">
                                <span class="tag-categoria">
                                    <%# Eval("NomeCategoria") %>
                                </span>
                            </div>
                            <asp:Image ID="imgPost" runat="server"
                                ImageUrl='<%# Eval("ImagemUrlPost") %>'
                                Visible='<%# Eval("ImagemUrlPost") != null %>'
                                CssClass="post-main-img" />

                            <div class="mt-2">
                                <span id="lblCurtidas<%# Eval("PostId") %>" class="curtidas">
                                    <%# Eval("Curtidas", "{0}") %>
                                </span>curtidas
                                &nbsp;
                                <button type="button" class="btnCurtir <%# Convert.ToBoolean(Eval("Curtiu")) ? "curtido" : "" %>"
                                    data-postid='<%# Eval("PostId") %>'>
                                    <%# Convert.ToBoolean(Eval("Curtiu")) ? "Descurtir" : "Curtir" %>
                                </button>
                            </div>                   
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="col-md-3">
            <div class="card-reactor suggestions-card">
                <h6>Sugestões de amigos</h6>
                <asp:Repeater ID="rptSugestoes" runat="server">
                    <ItemTemplate>
                        <div class="d-flex align-items-center mb-2">
                            <div class="ms-2"><%# Eval("Nome") %></div>
                            <div class="ms-2 seguidores-count"><%# Eval("TotalSeguidores") %></div>
                            <button class="btn btn-sm btn-outline-primary ms-auto btn-follow" data-userid='<%# Eval("UsuarioId") %>'>
                                Seguir
                            </button>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <script>
        document.addEventListener('click', function (e) {
            if (e.target.classList.contains('btn-follow')) {
                const btn = e.target;
                const userId = parseInt(btn.getAttribute('data-userid'));

                fetch('Blog.aspx/SeguirUsuario', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=UTF-8' },
                    body: JSON.stringify({ userId })
                })
                    .then(res => res.json())
                    .then(data => {
                        const res = data.d || data; // compatibilidade WebMethod
                        if (res.success) {
                            btn.innerText = res.seguindo ? 'Deixar de seguir' : 'Seguir';

                            // Atualiza contador de seguidores do usuário clicado
                            const contador = btn.parentElement.querySelector('.seguidores-count');
                            if (contador) {
                                contador.innerText = res.totalSeguidores;
                            }
                        } else {
                            alert(res.message || 'Erro ao atualizar');
                        }
                    });
            }
        });
        document.addEventListener('DOMContentLoaded', () => {

            // Toggle da seção de comentários
            document.querySelectorAll('.btn-toggle-comments').forEach(btn => {
                btn.addEventListener('click', () => {
                    const postId = btn.getAttribute('data-postid');
                    const commentSection = document.getElementById('comments-' + postId);
                    commentSection.style.display = (commentSection.style.display === 'none' || commentSection.style.display === '') ? 'block' : 'none';
                });
            });

            // Curtidas
            document.querySelectorAll('.btnCurtir').forEach(btn => {
                btn.addEventListener('click', () => {
                    const postId = btn.getAttribute('data-postid');

                    fetch('Blog.aspx/CurtirPost', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json;charset=UTF-8' },
                        body: JSON.stringify({ postId: parseInt(postId) })
                    })
                        .then(res => res.json())
                        .then(data => {
                            if (data.d.success) {
                                const span = document.getElementById('lblCurtidas' + postId);
                                span.innerText = data.d.total;

                                if (data.d.curtiu) {
                                    btn.classList.add('curtido');
                                    btn.innerText = 'Descurtir';
                                } else {
                                    btn.classList.remove('curtido');
                                    btn.innerText = 'Curtir';
                                }
                            } else {
                                alert(data.d.message || 'Erro ao curtir o post.');
                            }
                        });
                });
            });

            // Enviar comentário
            document.querySelectorAll('.btn-send-comment').forEach(btn => {
                btn.addEventListener('click', () => {
                    const postId = parseInt(btn.getAttribute('data-postid'));
                    const input = document.querySelector(`.comment-input[data-postid='${postId}']`);
                    const conteudo = input.value.trim();

                    if (!conteudo) {
                        alert('Escreva algo antes de enviar!');
                        return;
                    }

                    fetch('Blog.aspx/ComentarPost', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json;charset=UTF-8' },
                        body: JSON.stringify({ postId, conteudo })
                    })
                        .then(res => res.json())
                        .then(data => {

                            if (data.d.redirect) {
                                window.location.href = data.d.redirect;
                                return;
                            }

                            if (data.d.success) {
                                const commentsList = document.querySelector(`#comments-${postId} .comments-list`);
                                const commentSection = document.getElementById('comments-' + postId);

                                // Garantir que a seção de comentários esteja visível
                                commentSection.style.display = 'block';

                                // Criar novo comentário
                                const novoComentario = document.createElement('div');
                                novoComentario.innerHTML = `
                        <strong>${data.d.comentario.nome} (@${data.d.comentario.nomeUsuario})</strong> 
                        <span style="font-size:12px;color:#777;">${data.d.comentario.data}</span><br/>
                        ${data.d.comentario.conteudo}
                    `;
                                commentsList.appendChild(novoComentario);

                                // Limpar input
                                input.value = '';

                                // Atualizar contador
                                const commentCount = document.querySelector(`.btn-toggle-comments[data-postid='${postId}'] .comment-count`);
                                commentCount.innerText = parseInt(commentCount.innerText) + 1;
                            } else {
                                alert(data.d.message || 'Erro ao enviar comentário');
                            }
                        });
                });
            });
        });
    </script>

    <script>
        document.addEventListener('DOMContentLoaded', () => {
            const btnCriarPost = document.getElementById('btnCriarPost');
            const formCriarPost = document.getElementById('formCriarPost');

            btnCriarPost.addEventListener('click', () => {
                if (formCriarPost.style.display === 'none' || formCriarPost.style.display === '') {
                    formCriarPost.style.display = 'block';
                    btnCriarPost.innerText = 'Cancelar';
                } else {
                    formCriarPost.style.display = 'none';
                    btnCriarPost.innerText = 'Criar Post';
                }
            });
        });
    </script>

    <script>
        document.addEventListener('DOMContentLoaded', () => {
            const fileInput = document.getElementById('<%= FileUpload1.ClientID %>');
            const imgPreview = document.getElementById('imgPreviewPost');

            fileInput.addEventListener('change', (event) => {
                const file = event.target.files[0];
                if (file) {
                    const reader = new FileReader();
                    reader.onload = (e) => {
                        imgPreview.src = e.target.result;
                        imgPreview.style.display = 'block';
                    };
                    reader.readAsDataURL(file);
                } else {
                    imgPreview.src = '#';
                    imgPreview.style.display = 'none';
                }
            });
        });
    </script>

    <script>
        document.addEventListener('DOMContentLoaded', () => {
            const btnCriarPost = document.getElementById('btnCriarPost');
            const formCriarPost = document.getElementById('<%= formCriarPost.ClientID %>');
            const txtTitulo = document.getElementById('<%= txtTitulo.ClientID %>');
            const txtConteudo = document.getElementById('<%= txtConteudo.ClientID %>');
            const hfEditPostId = document.getElementById('<%= hfEditPostId.ClientID %>');
            const fileInput = document.getElementById('<%= FileUpload1.ClientID %>');
            const imgPreview = document.getElementById('<%= imgPreviewPost.ClientID %>');

            // Toggle criar post
            btnCriarPost.addEventListener('click', () => {
                if (formCriarPost.style.display === 'none' || formCriarPost.style.display === '') {
                    formCriarPost.style.display = 'block';
                    btnCriarPost.innerText = 'Cancelar';
                } else {
                    formCriarPost.style.display = 'none';
                    btnCriarPost.innerText = 'Criar Post';
                    // Limpar campos
                    txtTitulo.value = '';
                    txtConteudo.value = '';
                    hfEditPostId.value = '';
                    imgPreview.src = '#';
                    imgPreview.style.display = 'none';
                    fileInput.value = '';
                }
            });

            // Preview da imagem
            fileInput.addEventListener('change', (event) => {
                const file = event.target.files[0];
                if (file) {
                    const reader = new FileReader();
                    reader.onload = (e) => {
                        imgPreview.src = e.target.result;
                        imgPreview.style.display = 'block';
                    };
                    reader.readAsDataURL(file);
                } else {
                    imgPreview.src = '#';
                    imgPreview.style.display = 'none';
                }
            });
        });
    </script>
</asp:Content>
