using LojadoManoelAPI.DAL;
using LojadoManoelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojadoManoelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _context.ProdutosDb.Include(p => p.Dimensoes).ToListAsync();
            return Ok(produtos);       
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarProduto(string id, [FromBody] Produto produtoAtualizado)
        {
            if (id != produtoAtualizado.ProdutoId)
                return BadRequest();

            var produto = await _context.ProdutosDb.FindAsync(id);

            if (produto == null)
                return NotFound();

            produto.Dimensoes = produtoAtualizado.Dimensoes;
            produto.ProdutoId = produtoAtualizado.ProdutoId;
            produto.Pedido = produtoAtualizado.Pedido;

            _context.ProdutosDb.Update(produto);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto(string id) 
        {
            var produto = await _context.ProdutosDb.FindAsync(id);

            if (produto == null)
                return NotFound();
            
            _context.ProdutosDb.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
