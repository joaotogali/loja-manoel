using System.ComponentModel.DataAnnotations;

namespace LojadoManoelAPI.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public List<Produto> Produtos { get; set; } = new();
    }
}
