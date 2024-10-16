using Microsoft.AspNetCore.Mvc;
using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;

namespace challenge_c_sharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : Controller
    {
        private readonly EstadoService _estadoService;

        public EstadoController(EstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstados()
        {
            var estados = await _estadoService.GetEstadosAsync();
            return Ok(estados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadoById(int id)
        {
            var estado = await _estadoService.GetEstadoByIdAsync(id);
            if (estado == null)
            {
                return NotFound($"Estado com ID {id} não encontrado.");
            }
            return Ok(estado);
        }

        [HttpPost]
        public async Task<IActionResult> AddEstado([FromBody] EstadoDto estadoDto)
        {
            if (estadoDto == null)
            {
                return BadRequest("Estado não pode ser nulo.");
            }

            await _estadoService.AddEstadoAsync(estadoDto);
            return CreatedAtAction(nameof(GetEstadoById), new { id = estadoDto.Id }, estadoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] EstadoDto estadoDto)
        {
            if (estadoDto == null)
            {
                return BadRequest("Estado não pode ser nulo.");
            }

            try
            {
                await _estadoService.UpdateEstadoAsync(id, estadoDto);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest("O ID do estado fornecido não corresponde ao ID do objeto a ser atualizado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar estado: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            try
            {
                await _estadoService.DeleteEstadoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir estado: {ex.Message}");
            }
        }
    }
}
