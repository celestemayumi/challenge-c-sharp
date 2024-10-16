using Microsoft.AspNetCore.Mvc;
using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;

namespace challenge_c_sharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadeController : Controller
    {
        private readonly CidadeService _cidadeService;

        public CidadeController(CidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCidades()
        {
            var cidades = await _cidadeService.GetCidadesAsync();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCidadeById(int id)
        {
            var cidade = await _cidadeService.GetCidadeByIdAsync(id);
            if (cidade == null)
            {
                return NotFound($"Cidade com ID {id} não encontrada.");
            }
            return Ok(cidade);
        }

        [HttpPost]
        public async Task<IActionResult> AddCidade([FromBody] CidadeDto cidadeDto)
        {
            if (cidadeDto == null)
            {
                return BadRequest("Cidade não pode ser nula.");
            }

            await _cidadeService.AddCidadeAsync(cidadeDto);
            return CreatedAtAction(nameof(GetCidadeById), new { id = cidadeDto.Id }, cidadeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCidade(int id, [FromBody] CidadeDto cidadeDto)
        {
            if (cidadeDto == null)
            {
                return BadRequest("Cidade não pode ser nula.");
            }

            try
            {
                await _cidadeService.UpdateCidadeAsync(id, cidadeDto);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest("O ID da cidade fornecido não corresponde ao ID do objeto a ser atualizado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar cidade: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade(int id)
        {
            try
            {
                await _cidadeService.DeleteCidadeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir cidade: {ex.Message}");
            }
        }
    }
}
