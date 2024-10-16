using Microsoft.AspNetCore.Mvc;
using challenge_c_sharp.Services;
using challenge_c_sharp.Dtos;

namespace challenge_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : Controller
    {
        private readonly ConsultaService _consultaService;
        private readonly ILogger<ConsultasController> _logger; // Logger

        public ConsultasController(ConsultaService consultaService, ILogger<ConsultasController> logger)
        {
            _consultaService = consultaService;
            _logger = logger; // Inicializa o logger
        }

        // Método para obter todas as consultas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultaDto>>> Get()
        {
            try
            {
                var consultas = await _consultaService.GetConsultasAsync();
                return Ok(consultas);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter consultas: {ex.Message}");
                return StatusCode(500, "Erro ao obter consultas. Tente novamente mais tarde.");
            }
        }

        // Método para obter uma consulta específica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultaDto>> Get(int id)
        {
            try
            {
                var consulta = await _consultaService.GetConsultaByIdAsync(id);
                if (consulta == null) return NotFound();

                return Ok(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter consulta com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao obter consulta. Tente novamente mais tarde.");
            }
        }

        // Método para adicionar uma nova consulta
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ConsultaDto consultaDto)
        {
            if (consultaDto.PacienteId == 0 || consultaDto.DentistaId == 0)
            {
                return BadRequest("Paciente ou Dentista não podem ser nulos.");
            }

            try
            {
                await _consultaService.AddConsultaAsync(consultaDto);
                return CreatedAtAction(nameof(Get), new { id = consultaDto.Id }, consultaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar consulta: {ex.Message}");
                return StatusCode(500, "Erro ao adicionar consulta. Tente novamente mais tarde.");
            }
        }

        // Método para atualizar uma consulta existente
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ConsultaDto consultaDto)
        {
            if (id != consultaDto.Id) return BadRequest("ID da URL não corresponde ao ID da consulta.");

            try
            {
                await _consultaService.UpdateConsultaAsync(consultaDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar consulta: {ex.Message}");
                return StatusCode(500, "Erro ao atualizar consulta. Tente novamente mais tarde.");
            }
        }

        // Método para excluir uma consulta
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _consultaService.DeleteConsultaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir consulta com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao excluir consulta. Tente novamente mais tarde.");
            }
        }
    }
}
