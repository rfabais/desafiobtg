using System.Text.Json.Serialization;

namespace PedidosService.Model
{
    public class ItemPedido
    {
        public int PedidoId { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        [JsonIgnore]
        public Pedido? Pedido { get; set; }
    }
}
