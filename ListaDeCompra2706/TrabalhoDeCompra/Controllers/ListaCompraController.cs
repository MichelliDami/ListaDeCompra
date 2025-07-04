using TrabalhoDeCompra.Models.Dtos;
using TrabalhoDeCompra.Models;
using TrabalhoDeCompra.Service;
using Microsoft.AspNetCore.Mvc;
using TrabalhoDeCompra.Models.View;

namespace TrabalhoDeCompra.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ListaCompraController : ControllerBase
    {

        private readonly ListaCompraService service = new ListaCompraService();
        [HttpPost("inserir")]
        public IActionResult InserirItem([FromBody] ItemCompraDto dto)
        {
            service.Inserir(dto);
            return NoContent();
        }


        [HttpGet]
        [Route("ordenados")]
        public ActionResult<List<ItemCompraView>> ObterItensOrdenados()
        {
            var itens = service.ObterTodosOrdenadosPorPrioridade();
            return Ok(itens);
        }


        [HttpDelete("deletar/{id}")]
        public IActionResult DeletarItem(Guid id)
        {
            bool sucesso = service.DeletarPorId(id);

            if (sucesso)
                return Ok("Item deletado com sucesso.");
            else
                return BadRequest("Item não encontrado.");
        }
    }
}
