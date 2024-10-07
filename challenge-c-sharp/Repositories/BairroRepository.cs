using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Models;
using challenge_c_sharp.Config;
using challenge_c_sharp.Dtos;

namespace challenge_c_sharp.Repositories
{
    public class BairroRepository : IGenericRepository<BairroDto>
    {
        private readonly ApplicationDbContext _context;

        public BairroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BairroDto>> GetAllAsync()
        {
            try
            {
                return await _context.Bairros
                    .Include(b => b.Cidade) // Incluindo a navegação para Cidade
                    .Select(b => new BairroDto
                    {
                        Id = b.Id,
                        Nome = b.Nome,
                        CidadeId = b.CidadeId,
                        Cidade = new CidadeDto // Criando uma nova instância de CidadeDto
                        {
                            Id = b.Cidade.Id, // Atribuindo o Id da cidade
                            Nome = b.Cidade.Nome ,
                            Estado = new EstadoDto
                            {
                                Id = b.Cidade.Estado.Id,
                                Nome = b.Cidade.Estado .Nome ,
                                Sigla = b.Cidade.Estado.Sigla
                            }                                                 // Se você tiver outras propriedades em CidadeDto, atribua-as aqui também
                        }
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar bairros", ex);
            }
        }

        public async Task<BairroDto> GetByIdAsync(int id)
        {
            try
            {
                var bairro = await _context.Bairros
                    .Include(b => b.Cidade) // Incluindo a navegação para Cidade
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (bairro == null) return null;

                return new BairroDto
                {
                    Id = bairro.Id,
                    Nome = bairro.Nome,
                    CidadeId = bairro.CidadeId
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar bairro com ID {id}", ex);
            }
        }

        public async Task AddAsync(BairroDto bairroDto)
        {
            try
            {
                var bairro = new Bairro
                {
                    Nome = bairroDto.Nome,
                    CidadeId = bairroDto.CidadeId // Definindo a CidadeId
                };

                _context.Bairros.Add(bairro);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar bairro", ex);
            }
        }

        public async Task UpdateAsync(BairroDto bairroDto)
        {
            try
            {
                var bairro = await _context.Bairros.FindAsync(bairroDto.Id);
                if (bairro == null) throw new Exception("Bairro não encontrado");

                bairro.Nome = bairroDto.Nome;
                bairro.CidadeId = bairroDto.CidadeId; // Atualizando CidadeId

                _context.Bairros.Update(bairro);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar bairro", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var bairro = await _context.Bairros.FindAsync(id);
                if (bairro != null)
                {
                    _context.Bairros.Remove(bairro);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Bairro não encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir bairro com ID {id}", ex);
            }
        }
    }
}
