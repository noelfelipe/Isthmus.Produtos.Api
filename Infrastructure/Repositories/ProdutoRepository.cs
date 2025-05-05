using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Produto?> ObterPorCodigo(string codigo)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<IEnumerable<Produto>> PesquisarPorTexto(string texto)
        {
            return await _context.Produtos
                .Where(p => p.Codigo.Contains(texto) || p.Nome.Contains(texto))
                .ToListAsync();
        }

        public async Task Adicionar(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }
    }
}