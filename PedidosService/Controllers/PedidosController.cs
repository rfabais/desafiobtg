using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidosService.Data;
using PedidosService.Model;

namespace PedidosService.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosDbContext _context;

        public PedidosController(PedidosDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido == null || pedido.Itens == null || !pedido.Itens.Any())
                {
                    return BadRequest("O pedido deve conter itens.");
                }

                if (await _context.Pedidos.AnyAsync(p => p.CodigoPedido == pedido.CodigoPedido))
                {
                    return BadRequest($"Já existe um pedido com o código {pedido.CodigoPedido}.");
                }

                await _context.Pedidos.AddAsync(pedido);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

            return CreatedAtAction(nameof(ConsultarPedido), new { codigoPedido = pedido.CodigoPedido }, pedido);
        }

        /// <summary>
        /// Consulta um pedido por código
        /// </summary>
        /// <param name="codigoPedido"></param>
        /// <returns></returns>
        [HttpGet("{codigoPedido}")]
        public async Task<IActionResult> ConsultarPedido(int codigoPedido)
        {
            try
                {
                var pedido = await _context.Pedidos
                    .Include(p => p.Itens)
                    .FirstOrDefaultAsync(p => p.CodigoPedido == codigoPedido);

                if (pedido == null)
                {
                    return NotFound("Pedido não encontrado.");
                }

                return Ok(pedido);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Lista pedidos de um cliente
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <returns></returns>
        [HttpGet("cliente/{codigoCliente}")]
        public async Task<IActionResult> ListarPedidosPorCliente(int codigoCliente)
        {
            try
            {
                var pedidos = await _context.Pedidos
                    .Include(p => p.Itens)
                    .Where(p => p.CodigoCliente == codigoCliente)
                    .ToListAsync();

                if (!pedidos.Any())
                {
                    return NotFound("Nenhum pedido encontrado para este cliente.");
                }

                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Obtem o valor total do pedido
        /// </summary>
        /// <param name="codigoPedido"></param>
        /// <returns></returns>
        [HttpGet("{codigoPedido}/valor-total")]
        public async Task<IActionResult> ObterValorTotal(int codigoPedido)
        {
            try
            {
                var pedido = await _context.Pedidos
                    .Include(p => p.Itens)
                    .FirstOrDefaultAsync(p => p.CodigoPedido == codigoPedido);

                if (pedido == null)
                {
                    return NotFound("Pedido não encontrado.");
                }

                var valorTotal = pedido.Itens.Sum(i => i.Preco * i.Quantidade);

                return Ok(new { CodigoPedido = codigoPedido, ValorTotal = valorTotal });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
