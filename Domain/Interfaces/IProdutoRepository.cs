using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto?> ObterPorCodigo(string codigo); 
        Task<IEnumerable<Produto>> PesquisarPorTexto(string texto); 
        Task Adicionar(Produto produto); 
        Task Atualizar(Produto produto);
    }
}