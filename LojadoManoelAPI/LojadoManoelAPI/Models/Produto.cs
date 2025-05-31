using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojadoManoelAPI.Models
{
    public class Produto
    {
        public string? ProdutoId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public Dimensoes Dimensoes { get; set; }

        [JsonIgnore]
        public int PedidoId { get; set; }

        [ForeignKey(nameof(PedidoId))]
        public Pedido? Pedido { get; set; }
    }
}
