namespace LojadoManoelAPI.Models
{
    public class Produto
    {
        public string ProdutoId {  get; set; }
        public Dimensoes Dimensoes { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
