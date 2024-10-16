using Microsoft.AspNetCore.Mvc;
using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;

namespace challenge_c_sharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BairroController : Controller
    {
        private readonly BairroService _bairroService;

        public BairroController(BairroService bairroService)
        {
            _bairroService = bairroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBairros()
        {
            var bairros = await _bairroService.GetBairrosAsync();
            return Ok(bairros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBairroById(int id)
        {
            var bairro = await _bairroService.GetBairroByIdAsync(id);
            if (bairro == null)
            {
                return NotFound($"Bairro com ID {id} não encontrado.");
            }
            return Ok(bairro);
        }

        [HttpPost]
        public async Task<IActionResult> AddBairro([FromBody] BairroDto bairroDto)
        {
            if (bairroDto == null)
            {
                return BadRequest("Bairro não pode ser nulo.");
            }

            await _bairroService.AddBairroAsync(bairroDto);
            return CreatedAtAction(nameof(GetBairroById), new { id = bairroDto.Id }, bairroDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBairro(int id, [FromBody] BairroDto bairroDto)
        {
            if (bairroDto == null)
            {
                return BadRequest("Bairro não pode ser nulo.");
            }

            try
            {
                await _bairroService.UpdateBairroAsync(id, bairroDto);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest("O ID do bairro fornecido não corresponde ao ID do objeto a ser atualizado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar bairro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBairro(int id)
        {
            try
            {
                await _bairroService.DeleteBairroAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir bairro: {ex.Message}");
            }
        }
    }
}

