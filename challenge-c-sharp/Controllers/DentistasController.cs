using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace challenge_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistaController : Controller
    {
        private readonly DentistaService _dentistaService;

        public DentistaController(DentistaService dentistaService)
        {
            _dentistaService = dentistaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DentistaDto>>> Get()
        {
            try
            {
                var dentistas = await _dentistaService.GetDentistasAsync();
                return Ok(dentistas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar dentistas: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DentistaDto>> Get(int id)
        {
            try
            {
                var dentista = await _dentistaService.GetDentistaByIdAsync(id);
                if (dentista == null) return NotFound("Dentista não encontrado");
                return Ok(dentista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar dentista com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DentistaDto dentistaDto)
        {
            try
            {
                await _dentistaService.AddDentistaAsync(dentistaDto);
                return CreatedAtAction(nameof(Get), new { id = dentistaDto.Id }, dentistaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar dentista: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DentistaDto dentistaDto)
        {
            if (id != dentistaDto.Id) return BadRequest("ID do dentista incorreto");

            try
            {
                await _dentistaService.UpdateDentistaAsync(dentistaDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar dentista: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _dentistaService.DeleteDentistaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir dentista com ID {id}: {ex.Message}");
            }
        }
    }
}
