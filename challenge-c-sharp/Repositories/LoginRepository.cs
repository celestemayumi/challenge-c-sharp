using challenge_c_sharp.Config;
using challenge_c_sharp.Dtos;
using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Models;

namespace challenge_c_sharp.Repositories
{
    public class LoginRepository : IGenericRepository<LoginDto>
    {
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoginDto>> GetAllAsync()
        {
            try
            {
                return await _context.Logins
                    .Select(l => new LoginDto
                    {
                        Id = l.Id,
                        Email = l.Email,
                        Senha = l.Senha
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar logins", ex);
            }
        }

        public async Task<LoginDto> GetByIdAsync(int id)
        {
            try
            {
                var login = await _context.Logins.FindAsync(id);
                if (login == null) return null;

                return new LoginDto
                {
                    Id = login.Id,
                    Email = login.Email,
                    Senha = login.Senha
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar login com ID {id}", ex);
            }
        }

        public async Task AddAsync(LoginDto loginDto)
        {
            try
            {
                var login = new Login
                {
                    Email = loginDto.Email,
                    Senha = loginDto.Senha
                };
                _context.Logins.Add(login);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar login", ex);
            }
        }

        public async Task UpdateAsync(LoginDto loginDto)
        {
            try
            {
                var login = await _context.Logins.FindAsync(loginDto.Id);
                if (login == null) throw new Exception("Login não encontrado");

                login.Email = loginDto.Email;
                login.Senha = loginDto.Senha;

                _context.Logins.Update(login);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar login", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var login = await _context.Logins.FindAsync(id);
                if (login != null)
                {
                    _context.Logins.Remove(login);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Login não encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir login com ID {id}", ex);
            }
        }
    }
}
