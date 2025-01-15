using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PedidosService.Model;
using System.Text;

namespace PedidosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RabbitMqController : ControllerBase
    {
        private readonly RabbitMQConsumer _rabbitMqConsumer;

        public RabbitMqController(RabbitMQConsumer rabbitMqConsumer)
        {
            _rabbitMqConsumer = rabbitMqConsumer;
        }

        /// <summary>
        /// Publica mensagem na fila do Rabbit MQ
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        [HttpPost("publicar")]
        public IActionResult PublicarMensagem([FromBody] Pedido mensagem)
        {
            if (mensagem == null)
            {
                return BadRequest("A mensagem não pode ser nula.");
            }

            try
            {
                var mensagemJson = JsonConvert.SerializeObject(mensagem);

                _rabbitMqConsumer.PublicarMensagem(mensagemJson);

                return Ok($"Mensagem publicada na fila 'pedidos'.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao publicar mensagem: {ex.Message}");
            }
        }
    }
}
