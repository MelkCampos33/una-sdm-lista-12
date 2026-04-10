using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NikeStoreApi.Models;
using NikeStoreApi.src;

namespace NikeStoreApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly NikeContext _context;

        public AdminController(NikeContext context)
        {
            _context = context;
        }

        [HttpGet("balanco")]
        public async Task<IActionResult> GetBalanco()
        {
            
            var pedidos = await _context.Pedidos.Include(p => p.Produto).ToListAsync();
            
            var faturamentoTotal = pedidos.Sum(p => p.QuantidadeItens * p.Produto.Preco);

            var produtosEsgotados = await _context.Produtos
                .CountAsync(p => p.QuantidadeEstoque == 0);

            return Ok(new
            {
                FaturamentoTotal = faturamentoTotal,
                ProdutosComEstoqueZerado = produtosEsgotados,
                DataRelatorio = DateTime.Now
            });
        }
    }
}