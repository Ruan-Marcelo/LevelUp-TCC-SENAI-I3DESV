create database LevelUp

USE LevelUp;

CREATE TABLE Categoria
(
  CategoriaId INT PRIMARY KEY IDENTITY(1,1),
  CategoriaNome VARCHAR(100) NOT NULL,
  CategoriaImgUrl VARCHAR(MAX) NOT NULL,
  EstaAtivo BIT NOT NULL,
  DataCriacao DATETIME NOT NULL
);

CREATE TABLE SubCategoria
(
  SubCategoriaId INT PRIMARY KEY IDENTITY(1,1),
  SubCategoriaNome VARCHAR(100) NOT NULL,
  CategoriaId INT NOT NULL FOREIGN KEY REFERENCES Categoria(CategoriaId) ON DELETE CASCADE,
  EstaAtivo BIT NOT NULL,
  DataCriacao DATETIME NOT NULL
);

CREATE TABLE Produto
(
  ProdutoId INT PRIMARY KEY IDENTITY(1,1),
  ProdutoNome VARCHAR(100) NOT NULL,
  DescricaoCurta VARCHAR(200) NULL,
  DescricaoLonga VARCHAR(MAX) NULL,
  AdicionalDescricao VARCHAR(MAX) NULL,
  Preco DECIMAL(18,2) NULL,
  Quantidade INT NOT NULL,
  Tamanho VARCHAR(30) NULL,
  Cor VARCHAR(50) NULL,
  NomeEmpresa VARCHAR(100) NULL,
  CategoriaId INT NOT NULL FOREIGN KEY REFERENCES Categoria(CategoriaId) ON DELETE CASCADE,
  SubCategoriaId INT NOT NULL,
  Vendido INT NULL,
  Personalizado BIT NOT NULL,
  EstaAtivo BIT NOT NULL,
  DataCriacao DATETIME NOT NULL
);

CREATE TABLE ProdutoImg
(
  ImagemId INT PRIMARY KEY IDENTITY(1,1),
  ImagemUrl VARCHAR(MAX) NULL,
  ProdutoId INT NOT NULL FOREIGN KEY REFERENCES Produto(ProdutoId) ON DELETE CASCADE,
  ImagemPadrao BIT NOT NULL DEFAULT 0
);

CREATE TABLE Nivel
(
  NivelId INT PRIMARY KEY,
  NivelNome VARCHAR(50) NOT NULL
);

INSERT INTO Nivel VALUES(1,'Admin');
INSERT INTO Nivel VALUES(2,'Usuario');

CREATE TABLE Usuarios
(
  UsuarioId INT PRIMARY KEY IDENTITY(1,1),
  Nome VARCHAR(50) NULL,
  NomeDeUsuario VARCHAR(50) NULL UNIQUE,
  Celular VARCHAR(20) NULL,
  Email VARCHAR(50) NULL,
  Endereco VARCHAR(MAX) NULL,
  CodigoPostal VARCHAR(50) NULL,
  Senha VARCHAR(50) NULL,
  ImagemUrl VARCHAR(MAX) NULL,
  NivelId INT NOT NULL FOREIGN KEY REFERENCES Nivel(NivelId) ON DELETE CASCADE,
  DataCriacao DATETIME NOT NULL
);

CREATE TABLE ProdutoReview
(
  ReviewID INT PRIMARY KEY IDENTITY(1,1),
  Avaliacao INT NOT NULL,
  Comentario VARCHAR(MAX) NULL,
  ProdutoId INT NOT NULL FOREIGN KEY REFERENCES Produto(ProdutoId) ON DELETE CASCADE,
  UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE,
  DataCriacao DATETIME NOT NULL
);

CREATE TABLE ListaDeDesejos
(
  ListaDeDesejosID INT PRIMARY KEY IDENTITY(1,1),
  ProdutoId INT NOT NULL FOREIGN KEY REFERENCES Produto(ProdutoId) ON DELETE CASCADE,
  UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE,
  DataCriacao DATETIME NOT NULL
);

CREATE TABLE Carrinho
(
  CarrinhoId INT PRIMARY KEY IDENTITY(1,1),
  ProdutoId INT NOT NULL FOREIGN KEY REFERENCES Produto(ProdutoId) ON DELETE CASCADE,
  Quantidade INT NULL,
  PrecoUnitario DECIMAL(18,2) NOT NULL, 
  UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE,
  DataCriacao DATETIME NOT NULL
);

CREATE TABLE Contato
(
  ContatoId INT PRIMARY KEY IDENTITY(1,1),
  Nome VARCHAR(50) NULL,
  Email VARCHAR(50) NULL,
  Assunto VARCHAR(30) NULL,
  Mensagem VARCHAR(MAX) NULL,
  DataCriacao DATETIME NOT NULL
);

CREATE TABLE Pagamento
(
  PagamentoId INT PRIMARY KEY IDENTITY(1,1),
  Nome VARCHAR(50) NULL,
  CartaoNao VARCHAR(50) NULL,
  DataExpiracao VARCHAR(50) NULL,
  CvvNao INT NULL,
  Endereco VARCHAR(MAX) NULL,
  PagamentoModo VARCHAR(50) NULL
);

CREATE TABLE Pedidos
(
  ListaDetalheDePedidoId INT PRIMARY KEY IDENTITY(1,1),
  PedidoNao VARCHAR(50) NULL,
  UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE,
  Status VARCHAR(50) NULL,
  PagamentoId INT NOT NULL FOREIGN KEY REFERENCES Pagamento(PagamentoId) ON DELETE CASCADE,
  DataPedido DATETIME NOT NULL,
  Cancelar BIT NOT NULL DEFAULT 0,
  ValorTotal DECIMAL(18,2) NULL -- novo campo para total do pedido
);

CREATE TABLE PedidoItem
(
  PedidoItemId INT PRIMARY KEY IDENTITY(1,1),
  PedidoId INT NOT NULL FOREIGN KEY REFERENCES Pedidos(ListaDetalheDePedidoId) ON DELETE CASCADE,
  ProdutoId INT NOT NULL FOREIGN KEY REFERENCES Produto(ProdutoId) ON DELETE CASCADE,
  Quantidade INT NOT NULL,
  PrecoUnitario DECIMAL(18,2) NOT NULL -- preço do produto no momento do pedido
);

-- ================================================
-- DATABASE BLOG
-- ================================================

CREATE TABLE CategoriaPost (
    CategoriaPostId INT PRIMARY KEY IDENTITY(1,1),
    NomeCategoria VARCHAR(100) NOT NULL,
    Descricao VARCHAR(200) NULL,
    EstaAtivo BIT NOT NULL,
    DataCriacao DATETIME NOT NULL
);

CREATE TABLE Post (
    PostId INT PRIMARY KEY IDENTITY(1,1),
    Titulo VARCHAR(200) NOT NULL,
    Conteudo VARCHAR(MAX) NOT NULL,
    ImagemUrl VARCHAR(MAX) NULL,
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE,
    CategoriaPostId INT NULL FOREIGN KEY REFERENCES CategoriaPost(CategoriaPostId) ON DELETE SET NULL,
    EstaAtivo BIT NOT NULL,
    DataCriacao DATETIME NOT NULL
);

CREATE TABLE ComentarioPost (
    ComentarioId INT PRIMARY KEY IDENTITY(1,1),
    PostId INT NOT NULL FOREIGN KEY REFERENCES Post(PostId) ON DELETE CASCADE,
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioId) ON DELETE NO ACTION,
    Conteudo VARCHAR(MAX) NOT NULL,
    DataCriacao DATETIME NOT NULL
);

CREATE TABLE CurtidaPost (
    CurtidaId INT PRIMARY KEY IDENTITY(1,1),
    PostId INT NOT NULL FOREIGN KEY REFERENCES Post(PostId) ON DELETE CASCADE,
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuarios(UsuarioId) ON DELETE NO ACTION,
    DataCriacao DATETIME NOT NULL
);

CREATE TABLE Tag (
    TagId INT PRIMARY KEY IDENTITY(1,1),
    NomeTag VARCHAR(50) NOT NULL
);

CREATE TABLE PostTag (
    PostTagId INT PRIMARY KEY IDENTITY(1,1),
    PostId INT NOT NULL FOREIGN KEY REFERENCES Post(PostId) ON DELETE CASCADE,
    TagId INT NOT NULL FOREIGN KEY REFERENCES Tag(TagId) ON DELETE CASCADE
);
