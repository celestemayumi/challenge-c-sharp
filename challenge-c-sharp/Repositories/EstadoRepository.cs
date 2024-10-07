using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Models;
using challenge_c_sharp.Config;
using challenge_c_sharp.Dtos;

namespace challenge_c_sharp.Repositories
{
    public class EstadoRepository : IGenericRepository<EstadoDto>
    {
        private readonly ApplicationDbContext _context;

        public EstadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para obter todos os estados
        public async Task<IEnumerable<EstadoDto>> GetAllAsync()
        {
            try
            {
                return await _context.Estados
                    .Select(e => new EstadoDto
                    {
                        Id = e.Id,
                        Nome = e.Nome,
                        Sigla = e.Sigla
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar estados", ex);
            }
        }

        // Método para obter um estado pelo ID
        public async Task<EstadoDto> GetByIdAsync(int id)
        {
            try
            {
                var estado = await _context.Estados
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (estado == null) return null;

                return new EstadoDto
                {
                    Id = estado.Id,
                    Nome = estado.Nome,
                    Sigla = estado.Sigla
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar estado com ID {id}", ex);
            }
        }

        // Método para adicionar um novo estado
        public async Task AddAsync(EstadoDto estadoDto)
        {
            try
            {
                var estado = new Estado
                {
                    Nome = estadoDto.Nome,
                    Sigla = estadoDto.Sigla
                };

                _context.Estados.Add(estado);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar estado", ex);
            }
        }

        // Método para atualizar um estado
        public async Task UpdateAsync(EstadoDto estadoDto)
        {
            try
            {
                var estado = await _context.Estados.FindAsync(estadoDto.Id);
                if (estado == null) throw new Exception("Estado não encontrado");

                estado.Nome = estadoDto.Nome;
                estado.Sigla = estadoDto.Sigla;

                _context.Estados.Update(estado);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar estado", ex);
            }
        }

        // Método para deletar um estado pelo ID
        public async Task DeleteAsync(int id)
        {
            try
            {
                var estado = await _context.Estados.FindAsync(id);
                if (estado != null)
                {
                    _context.Estados.Remove(estado);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Estado não encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir estado com ID {id}", ex);
            }
        }
    }
}
