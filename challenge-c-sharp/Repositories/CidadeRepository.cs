using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Models;
using challenge_c_sharp.Config;
using challenge_c_sharp.Dtos;

namespace challenge_c_sharp.Repositories
{
    public class CidadeRepository : IGenericRepository<CidadeDto>
    {
        private readonly ApplicationDbContext _context;

        public CidadeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para obter todas as cidades
        public async Task<IEnumerable<CidadeDto>> GetAllAsync()
        {
            try
            {
                return await _context.Cidades
                    .Include(c => c.Estado)  // Inclui a navegação para o Estado
                    .Select(c => new CidadeDto
                    {
                        Id = c.Id,
                        Nome = c.Nome,
                        EstadoId = c.EstadoId,
                        Estado = new EstadoDto // Mapeia o Estado
                        {
                            Id = c.Estado.Id,
                            Nome = c.Estado.Nome,
                            Sigla = c.Estado.Sigla
                        }
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar cidades", ex);
            }
        }

        // Método para obter uma cidade pelo ID
        public async Task<CidadeDto> GetByIdAsync(int id)
        {
            try
            {
                var cidade = await _context.Cidades
                    .Include(c => c.Estado) // Inclui a navegação para o Estado
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (cidade == null) return null;

                return new CidadeDto
                {
                    Id = cidade.Id,
                    Nome = cidade.Nome,
                    EstadoId = cidade.EstadoId,
                    Estado = new EstadoDto
                    {
                        Id = cidade.Estado.Id,
                        Nome = cidade.Estado.Nome,
                        Sigla = cidade.Estado.Sigla
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar cidade com ID {id}", ex);
            }
        }

        // Método para adicionar uma nova cidade
        public async Task AddAsync(CidadeDto cidadeDto)
        {
            try
            {
                var cidade = new Cidade
                {
                    Nome = cidadeDto.Nome,
                    EstadoId = cidadeDto.EstadoId
                };

                _context.Cidades.Add(cidade);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar cidade", ex);
            }
        }

        // Método para atualizar uma cidade
        public async Task UpdateAsync(CidadeDto cidadeDto)
        {
            try
            {
                var cidade = await _context.Cidades.FindAsync(cidadeDto.Id);
                if (cidade == null) throw new Exception("Cidade não encontrada");

                cidade.Nome = cidadeDto.Nome;
                cidade.EstadoId = cidadeDto.EstadoId;

                _context.Cidades.Update(cidade);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar cidade", ex);
            }
        }

        // Método para deletar uma cidade pelo ID
        public async Task DeleteAsync(int id)
        {
            try
            {
                var cidade = await _context.Cidades.FindAsync(id);
                if (cidade != null)
                {
                    _context.Cidades.Remove(cidade);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Cidade não encontrada");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir cidade com ID {id}", ex);
            }
        }
    }
}

