using LojadoManoelAPI.DAL;
using LojadoManoelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojadoManoelAPI.Services;


namespace LojadoManoelAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EmpacotamentoProcess _empacotamento;

        public PedidosController(AppDbContext context)
        {
            _context = context;
            _empacotamento = new EmpacotamentoProcess();
        }

        [HttpGet]
        public async Task<ActionResult> GetPedidosComCaixa()
        {
            var pedidos = await _context.PedidosDb.Include(p => p.Produtos).ThenInclude(prod => prod.Dimensoes).ToListAsync();

            var response = pedidos.Select(Pedido => new
            {
                pedido_id = Pedido.PedidoId,
                caixas = _empacotamento.EmpacotarProduto(Pedido.Produtos)
            });

            return Ok(new { pedidos = response });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.PedidosDb.Include(p => p.Produtos).ThenInclude(prod => prod.Dimensoes).FirstOrDefaultAsync(p => p.PedidoId == id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);

        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> CriarPedido([FromBody] Pedido pedido)
        {
            if (pedido == null)
                return BadRequest();

            await _context.PedidosDb.AddAsync(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.PedidoId }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedido(int id, [FromBody] Pedido pedidoAtualizado)
        {
            if (id != pedidoAtualizado.PedidoId)
            {
                return BadRequest();
            }

            var pedido = await _context.PedidosDb.FindAsync(id);

            if (pedido == null)
                return NotFound();

            var pedidoExistente = await _context.PedidosDb.Include(p => p.Produtos).FirstOrDefaultAsync(p => p.PedidoId == id);
            
            if (pedidoExistente == null)
                return NotFound();

            _context.ProdutosDb.RemoveRange(pedidoExistente.Produtos);
            pedidoExistente.Produtos = pedidoAtualizado.Produtos;

            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPedido(int id)
        {
            var pedido = await _context.PedidosDb.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.PedidosDb.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
