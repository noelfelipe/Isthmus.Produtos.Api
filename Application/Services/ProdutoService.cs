using Application.Service.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<Produto> CadastrarOuAtualizarProduto(string codigo, string nome, string descricao, decimal preco)
        {
            var produtoExistente = await _produtoRepository.ObterPorCodigo(codigo);

            if (produtoExistente != null)
            {
                produtoExistente.Atualizar(nome, descricao, preco);
                await _produtoRepository.Atualizar(produtoExistente);
                return produtoExistente;
            }

            var novoProduto = new Produto(codigo, nome, descricao, preco);

            await _produtoRepository.Adicionar(novoProduto);
            return novoProduto;
        }

        public async Task<Produto?> EditarProduto(string codigo, string nome, string descricao, decimal preco)
        {
            var produto = await _produtoRepository.ObterPorCodigo(codigo);
            if (produto == null) return null;

            produto.Atualizar(nome, descricao, preco);
            await _produtoRepository.Atualizar(produto);

            return produto;
        }

        public async Task<IEnumerable<Produto>> PesquisarProduto(string texto)
        {
            return await _produtoRepository.PesquisarPorTexto(texto);
        }

        public async Task<bool> RemoverProduto(string codigo)
        {
            var produto = await _produtoRepository.ObterPorCodigo(codigo);
            if (produto == null) return false;

            produto.Desativar();
            await _produtoRepository.Atualizar(produto);

            return true;
        }
    }
}