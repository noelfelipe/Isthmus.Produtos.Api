using Domain.Entities;

namespace Application.Service.Interfaces
{
    public interface IProdutoService
    {
        Task<Produto> CadastrarOuAtualizarProduto(string codigo, string nome, string descricao, decimal preco);
        Task<Produto?> EditarProduto(string codigo, string nome, string descricao, decimal preco);
        Task<IEnumerable<Produto>> PesquisarProduto(string texto);
        Task<bool> RemoverProduto(string codigo);
    }
}