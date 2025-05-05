using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Tests.Application
{
    public class ProdutoServiceTests
    {
        private readonly ProdutoService _produtoService;
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;

        public ProdutoServiceTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarOuAtualizarProduto_DeveCadastrarNovoProduto_QuandoCodigoNaoExiste()
        {
            // Arrange
            var codigo = "123";
            var nome = "Produto Teste";
            var descricao = "Descrição do Produto Teste";
            var preco = 100.0m;

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorCodigo(codigo))
                .ReturnsAsync((Produto?)null);

            // Act
            var produto = await _produtoService.CadastrarOuAtualizarProduto(codigo, nome, descricao, preco);

            // Assert
            Assert.NotNull(produto);
            Assert.Equal(codigo, produto.Codigo);
            Assert.Equal(nome, produto.Nome);
            Assert.Equal(descricao, produto.Descricao);
            Assert.Equal(preco, produto.Preco);

            _produtoRepositoryMock.Verify(repo => repo.Adicionar(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task CadastrarOuAtualizarProduto_DeveAtualizarProduto_QuandoCodigoJaExiste()
        {
            // Arrange
            var codigo = "123";
            var nomeAtualizado = "Produto Atualizado";
            var descricaoAtualizada = "Descrição Atualizada";
            var precoAtualizado = 200.0m;

            var produtoExistente = new Produto(codigo, "Produto Antigo", "Descrição Antiga", 100.0m);

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorCodigo(codigo))
                .ReturnsAsync(produtoExistente);

            // Act
            var produto = await _produtoService.CadastrarOuAtualizarProduto(codigo, nomeAtualizado, descricaoAtualizada, precoAtualizado);

            // Assert
            Assert.NotNull(produto);
            Assert.Equal(nomeAtualizado, produto.Nome);
            Assert.Equal(descricaoAtualizada, produto.Descricao);
            Assert.Equal(precoAtualizado, produto.Preco);

            _produtoRepositoryMock.Verify(repo => repo.Atualizar(produtoExistente), Times.Once);
        }

        [Fact]
        public async Task EditarProduto_DeveRetornarNull_QuandoProdutoNaoExiste()
        {
            // Arrange
            var codigo = "123";

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorCodigo(codigo))
                .ReturnsAsync((Produto?)null);

            // Act
            var produto = await _produtoService.EditarProduto(codigo, "Novo Nome", "Nova Descrição", 150.0m);

            // Assert
            Assert.Null(produto);
        }

        [Fact]
        public async Task EditarProduto_DeveAtualizarProduto_QuandoProdutoExiste()
        {
            // Arrange
            var codigo = "123";
            var produtoExistente = new Produto(codigo, "Produto Antigo", "Descrição Antiga", 100.0m);

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorCodigo(codigo))
                .ReturnsAsync(produtoExistente);

            // Act
            var produto = await _produtoService.EditarProduto(codigo, "Novo Nome", "Nova Descrição", 150.0m);

            // Assert
            Assert.NotNull(produto);
            Assert.Equal("Novo Nome", produto.Nome);
            Assert.Equal("Nova Descrição", produto.Descricao);
            Assert.Equal(150.0m, produto.Preco);

            _produtoRepositoryMock.Verify(repo => repo.Atualizar(produtoExistente), Times.Once);
        }

        [Fact]
        public async Task PesquisarProduto_DeveRetornarProdutos_QueContenhamTexto()
        {
            // Arrange
            var texto = "Teste";
            var produtos = new List<Produto>
            {
                new Produto("123", "Produto Teste 1", "Descrição 1", 100.0m),
                new Produto("456", "Produto Teste 2", "Descrição 2", 200.0m)
            };

            _produtoRepositoryMock
                .Setup(repo => repo.PesquisarPorTexto(texto))
                .ReturnsAsync(produtos);

            // Act
            var resultado = await _produtoService.PesquisarProduto(texto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task RemoverProduto_DeveRetornarFalse_QuandoProdutoNaoExiste()
        {
            // Arrange
            var codigo = "123";

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorCodigo(codigo))
                .ReturnsAsync((Produto?)null);

            // Act
            var resultado = await _produtoService.RemoverProduto(codigo);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task RemoverProduto_DeveDesativarProduto_QuandoProdutoExiste()
        {
            // Arrange
            var codigo = "123";
            var produtoExistente = new Produto(codigo, "Produto Teste", "Descrição", 100.0m);

            _produtoRepositoryMock
                .Setup(repo => repo.ObterPorCodigo(codigo))
                .ReturnsAsync(produtoExistente);

            // Act
            var resultado = await _produtoService.RemoverProduto(codigo);

            // Assert
            Assert.True(resultado);
            Assert.False(produtoExistente.Ativo);

            _produtoRepositoryMock.Verify(repo => repo.Atualizar(produtoExistente), Times.Once);
        }
    }
}