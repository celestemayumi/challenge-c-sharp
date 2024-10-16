using challenge_c_sharp.Models;
using challenge_c_sharp.Dtos;
using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Config;

namespace challenge_c_sharp.Repositories
{
    public class UnidadeRepository : IGenericRepository<UnidadeDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnidadeRepository> _logger;

        public UnidadeRepository(ApplicationDbContext context, ILogger<UnidadeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<UnidadeDto>> GetAllAsync()
        {
            try
            {
                return await _context.Unidades
                    .Select(u => new UnidadeDto
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Telefone = u.Telefone,
                        Endereco = u.Endereco
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter unidades: {ex.Message}");
                throw;
            }
        }

        public async Task<UnidadeDto> GetByIdAsync(int id)
        {
            try
            {
                var unidade = await _context.Unidades.FindAsync(id);
                if (unidade == null) return null;

                return new UnidadeDto
                {
                    Id = unidade.Id,
                    Nome = unidade.Nome,
                    Telefone = unidade.Telefone,
                    Endereco = unidade.Endereco
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter unidade com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task AddAsync(UnidadeDto unidadeDto)
        {
            try
            {
                var unidade = new Unidade
                {
                    Nome = unidadeDto.Nome,
                    Telefone = unidadeDto.Telefone,
                    Endereco = unidadeDto.Endereco
                };

                _context.Unidades.Add(unidade);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar unidade: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(UnidadeDto unidadeDto)
        {
            try
            {
                var unidade = await _context.Unidades.FindAsync(unidadeDto.Id);
                if (unidade == null) throw new KeyNotFoundException($"Unidade com ID {unidadeDto.Id} não encontrada.");

                unidade.Nome = unidadeDto.Nome;
                unidade.Telefone = unidadeDto.Telefone;
                unidade.Endereco = unidadeDto.Endereco;

                _context.Unidades.Update(unidade);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar unidade: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var unidade = await _context.Unidades.FindAsync(id);
                if (unidade == null) throw new KeyNotFoundException($"Unidade com ID {id} não encontrada.");

                _context.Unidades.Remove(unidade);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir unidade com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
