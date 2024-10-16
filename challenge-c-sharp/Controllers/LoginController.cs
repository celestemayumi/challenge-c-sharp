using challenge_c_sharp.Dtos;
using challenge_c_sharp.Services; // Alterado para usar LoginService
using Microsoft.AspNetCore.Mvc;

namespace challenge_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService; // Alterado para usar LoginService

        public LoginController(LoginService loginService) // Alterado para injetar LoginService
        {
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginDto>>> Get()
        {
            try
            {
                var logins = await _loginService.GetLoginsAsync();
                return Ok(logins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar logins: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoginDto>> Get(int id)
        {
            try
            {
                var login = await _loginService.GetLoginByIdAsync(id);
                if (login == null) return NotFound("Login não encontrado");
                return Ok(login);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar login com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginDto loginDto)
        {
            try
            {
                await _loginService.AddLoginAsync(loginDto);
                return CreatedAtAction(nameof(Get), new { id = loginDto.Id }, loginDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar login: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] LoginDto loginDto)
        {
            if (id != loginDto.Id) return BadRequest("ID do login incorreto");

            try
            {
                await _loginService.UpdateLoginAsync(loginDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar login: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _loginService.DeleteLoginAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir login com ID {id}: {ex.Message}");
            }
        }
    }
}
