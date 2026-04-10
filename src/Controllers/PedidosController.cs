using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NikeStoreApi.src;
using NikeStoreApi.Models;

namespace NikeStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly NikeContext _context;

        public PedidosController(NikeContext context) => _context = context;

        [HttpPost]
        public async Task<ActionResult> RealizarPedido(Pedido pedido)
        {
            var produto = await _context.Produtos.FindAsync(pedido.ProdutoId);

            if (produto == null) return NotFound("Produto não encontrado.");

            if (produto.QuantidadeEstoque < pedido.QuantidadeItens)
            {
                return Conflict("Estoque insuficiente para este modelo.");
            }

            produto.QuantidadeEstoque -= pedido.QuantidadeItens;

            if (produto.Nome != null && produto.Nome.Contains("Air Jordan", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Alerta de Hype: Um Air Jordan acaba de ser vendido!");
            }

            pedido.DataPedido = DateTime.Now;
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedidos), new { id = pedido.Id }, pedido);
        }

        [HttpGet]
        public async Task<ActionResult> GetPedidos()
        {
            return Ok(await _context.Pedidos
                .Include(p => p.Produto)
                .Include(p => p.Cliente)
                .Select(p => new {
                    p.Id,
                    Cliente = p.Cliente.NomeCompleto,
                    Produto = p.Produto.Nome,
                    p.QuantidadeItens,
                    Total = p.QuantidadeItens * p.Produto.Preco
                }).ToListAsync());
        }
    }
}