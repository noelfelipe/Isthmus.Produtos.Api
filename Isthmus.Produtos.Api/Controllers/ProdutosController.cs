using Application.Service.Interfaces;
using Isthmus.Produtos.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Isthmus.Produtos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController(IProdutoService produtoService) : ControllerBase
    {
        private readonly IProdutoService _produtoService = produtoService;

        /// <summary>
        /// Cadastra ou atualiza um produto com base no código fornecido.
        /// Se o código já existir, o produto será atualizado; caso contrário, será criado um novo.
        /// </summary>
        /// <param name="produtoDto">Dados do produto a serem cadastrados ou atualizados.</param>
        /// <returns>O produto cadastrado ou atualizado.</returns>
        [HttpPost]
        public async Task<IActionResult> CadastrarOuAtualizarProduto([FromBody] ProdutoDto produtoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var produto = await _produtoService.CadastrarOuAtualizarProduto(
                produtoDto.Codigo,
                produtoDto.Nome,
                produtoDto.Descricao,
                produtoDto.Preco
            );

            return Ok(produto);
        }

        /// <summary>
        /// Edita os dados de um produto existente com base no código fornecido.
        /// </summary>
        /// <param name="codigo">Código do produto a ser editado.</param>
        /// <param name="produtoDto">Novos dados do produto.</param>
        /// <returns>O produto atualizado ou um erro caso não seja encontrado.</returns>
        [HttpPut("{codigo}")]
        public async Task<IActionResult> EditarProduto(string codigo, [FromBody] ProdutoDto produtoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var produto = await _produtoService.EditarProduto(
                codigo,
                produtoDto.Nome,
                produtoDto.Descricao,
                produtoDto.Preco
            );

            if (produto == null)
                return NotFound(new { Message = "Produto não encontrado." });

            return Ok(produto);
        }

        /// <summary>
        /// Pesquisa produtos cujo código ou nome contenham o texto fornecido.
        /// </summary>
        /// <param name="texto">Texto a ser pesquisado.</param>
        /// <returns>Lista de produtos que correspondem à pesquisa.</returns>
        [HttpGet("pesquisar")]
        public async Task<IActionResult> PesquisarProduto([FromQuery] string texto)
        {
            var produtos = await _produtoService.PesquisarProduto(texto);
            return Ok(produtos);
        }

        /// <summary>
        /// Remove logicamente um produto, desativando-o (campo Ativo = false).
        /// </summary>
        /// <param name="codigo">Código do produto a ser removido.</param>
        /// <returns>NoContent se a remoção for bem-sucedida ou NotFound se o produto não for encontrado.</returns>
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> RemoverProduto(string codigo)
        {
            var sucesso = await _produtoService.RemoverProduto(codigo);

            if (!sucesso)
                return NotFound(new { Message = "Produto não encontrado." });

            return NoContent();
        }
    }
}