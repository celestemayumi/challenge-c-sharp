using Microsoft.AspNetCore.Mvc;
using challenge_c_sharp.Services;
using challenge_c_sharp.Dtos;

namespace challenge_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : Controller
    {
        private readonly PacienteService _pacienteService;
        private readonly ILogger<PacientesController> _logger;

        public PacientesController(PacienteService pacienteService, ILogger<PacientesController> logger)
        {
            _pacienteService = pacienteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> Get()
        {
            try
            {
                var pacientes = await _pacienteService.GetPacientesAsync();
                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter pacientes: {ex.Message}");
                return StatusCode(500, "Erro ao obter pacientes. Tente novamente mais tarde.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDto>> Get(int id)
        {
            try
            {
                var paciente = await _pacienteService.GetPacienteByIdAsync(id);
                if (paciente == null) return NotFound();

                return Ok(paciente);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter paciente com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao obter paciente. Tente novamente mais tarde.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PacienteDto pacienteDto)
        {
            try
            {
                await _pacienteService.AddPacienteAsync(pacienteDto);
                return CreatedAtAction(nameof(Get), new { id = pacienteDto.Id }, pacienteDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar paciente: {ex.Message}");
                return StatusCode(500, "Erro ao adicionar paciente. Tente novamente mais tarde.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PacienteDto pacienteDto)
        {
            if (id != pacienteDto.Id) return BadRequest();

            try
            {
                await _pacienteService.UpdatePacienteAsync(pacienteDto);
                return NoContent();
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx.Message);
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar paciente: {ex.Message}");
                return StatusCode(500, "Erro ao atualizar paciente. Tente novamente mais tarde.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _pacienteService.DeletePacienteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogWarning(knfEx.Message);
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir paciente com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao excluir paciente. Tente novamente mais tarde.");
            }
        }
    }
}
