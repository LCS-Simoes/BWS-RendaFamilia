using BWS.Application.DTOs;
using BWS.Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace BWS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesUseCase _clientes;

        public ClientesController(ClientesUseCase clientes)
        {
            _clientes = clientes;
        }

        [HttpGet] // Alterar pra cliente
        public async Task<ActionResult> TodosUsuarios()
        {
            var cliente = await _clientes.Handle();
            return Ok(cliente);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarId(int id)
        {
            var usuario = await _clientes.BuscarID(id);
            return Ok(usuario);
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult> Cadastrar([FromBody] CadastrarRequest request)
        {
            try
            {
                var usuario = await _clientes.ExecuteAsync(request);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(int id, [FromBody] AtualizarRequest request)
        {
            var usuario = await _clientes.AtualizarCliente(id, request);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await _clientes.Deletar(id);

            if (resultado)
                return NoContent();

            return NotFound();
        }
    }
}
