using Microsoft.AspNetCore.Mvc;
using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;
using Microsoft.Extensions.Logging;

namespace challenge_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeController : Controller
    {
        private readonly UnidadeService _unidadeService;
        private readonly ILogger<UnidadeController> _logger;

        public UnidadeController(UnidadeService unidadeService, ILogger<UnidadeController> logger)
        {
            _unidadeService = unidadeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadeDto>>> GetUnidades()
        {
            try
            {
                var unidades = await _unidadeService.GetUnidadesAsync();
                return Ok(unidades);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter unidades: {ex.Message}");
                return StatusCode(500, "Erro ao obter unidades. Tente novamente mais tarde.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadeDto>> GetUnidade(int id)
        {
            try
            {
                var unidade = await _unidadeService.GetUnidadeByIdAsync(id);
                if (unidade == null) return NotFound($"Unidade com ID {id} não encontrada.");

                return Ok(unidade);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter unidade com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao obter unidade. Tente novamente mais tarde.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UnidadeDto unidadeDto)
        {
            try
            {
                await _unidadeService.AddUnidadeAsync(unidadeDto);
                return CreatedAtAction(nameof(GetUnidade), new { id = unidadeDto.Id }, unidadeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar unidade: {ex.Message}");
                return StatusCode(500, "Erro ao adicionar unidade. Tente novamente mais tarde.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UnidadeDto unidadeDto)
        {
            if (id != unidadeDto.Id) return BadRequest("O ID na URL não corresponde ao ID do objeto.");

            try
            {
                await _unidadeService.UpdateUnidadeAsync(unidadeDto);
                return NoContent();
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx.Message);
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar unidade: {ex.Message}");
                return StatusCode(500, "Erro ao atualizar unidade. Tente novamente mais tarde.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _unidadeService.DeleteUnidadeAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx.Message);
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir unidade com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao excluir unidade. Tente novamente mais tarde.");
            }
        }
    }
}
