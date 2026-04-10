using NikeStoreApi.Models;

namespace NikeStoreApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public DateTime DataPedido { get; set; }
        public int QuantidadeItens { get; set; }
    }
}