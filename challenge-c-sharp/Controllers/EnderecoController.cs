using Microsoft.AspNetCore.Mvc;
using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services;

namespace challenge_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : Controller
    {
        private readonly EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        // GET: api/endereco
        [HttpGet]
        public async Task<IActionResult> GetAllEnderecos()
        {
            try
            {
                var enderecos = await _enderecoService.GetEnderecosAsync();
                return Ok(enderecos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter endereços: {ex.Message}");
            }
        }

        // GET: api/endereco/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnderecoById(int id)
        {
            try
            {
                var endereco = await _enderecoService.GetEnderecoByIdAsync(id);
                if (endereco == null)
                {
                    return NotFound($"Endereço com ID {id} não encontrado.");
                }
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter endereço: {ex.Message}");
            }
        }

        // POST: api/endereco
        [HttpPost]
        public async Task<IActionResult> AddEndereco([FromBody] EnderecoDto enderecoDto)
        {
            if (enderecoDto == null)
            {
                return BadRequest("Dados do endereço são obrigatórios.");
            }

            try
            {
                await _enderecoService.AddEnderecoAsync(enderecoDto);
                return CreatedAtAction(nameof(GetEnderecoById), new { id = enderecoDto.Id }, enderecoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar endereço: {ex.Message}");
            }
        }

        // PUT: api/endereco/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEndereco(int id, [FromBody] EnderecoDto enderecoDto)
        {
            if (enderecoDto == null || enderecoDto.Id != id)
            {
                return BadRequest("Dados do endereço são obrigatórios e o ID deve corresponder.");
            }

            try
            {
                await _enderecoService.UpdateEnderecoAsync(enderecoDto);
                return NoContent(); // Sucesso, mas não retorna nada
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar endereço: {ex.Message}");
            }
        }

        // DELETE: api/endereco/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            try
            {
                await _enderecoService.DeleteEnderecoAsync(id);
                return NoContent(); // Sucesso, mas não retorna nada
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir endereço: {ex.Message}");
            }
        }
    }
}
