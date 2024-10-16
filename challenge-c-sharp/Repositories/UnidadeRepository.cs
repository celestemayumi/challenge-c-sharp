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
                        Endereco = new EnderecoDto
                        {
                            Id = u.Endereco.Id,
                            Logradouro = u.Endereco.Logradouro,
                            Numero = u.Endereco.Numero,
                            CEP = u.Endereco.CEP,
                            Complemento = u.Endereco.Complemento,
                            BairroId = u.Endereco.BairroId,
                            Bairro = new BairroDto
                            {
                                Id = u.Endereco.Bairro.Id,
                                Nome = u.Endereco.Bairro.Nome,
                                Cidade = new CidadeDto 
                                {
                                    Id = u.Endereco.Bairro.Cidade.Id,
                                    Nome = u.Endereco.Bairro.Cidade.Nome,
                                    Estado = new EstadoDto // Mapeando EstadoDto
                                    {
                                        Id = u.Endereco.Bairro.Cidade.Estado.Id,
                                        Nome = u.Endereco.Bairro.Cidade.Estado.Nome,
                                        Sigla = u.Endereco.Bairro.Cidade.Estado.Sigla
                                    }
                                }
                            }
                        }
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
                    Endereco = new EnderecoDto
                    {
                        Id = unidade.Endereco.Id,
                        Logradouro = unidade.Endereco.Logradouro,
                        Numero = unidade.Endereco.Numero,
                        CEP = unidade.Endereco.CEP,
                        Complemento = unidade.Endereco.Complemento,
                        BairroId = unidade.Endereco.BairroId,
                        Bairro = new BairroDto
                        {
                            Id = unidade.Endereco.Bairro.Id,
                            Nome = unidade.Endereco.Bairro.Nome,
                            Cidade = new CidadeDto
                            {
                                Id = unidade.Endereco.Bairro.Cidade.Id,
                                Nome = unidade.Endereco.Bairro.Cidade.Nome,
                                Estado = new EstadoDto // Mapeando EstadoDto
                                {
                                    Id = unidade.Endereco.Bairro.Cidade.Estado.Id,
                                    Nome = unidade.Endereco.Bairro.Cidade.Estado.Nome,
                                    Sigla = unidade.Endereco.Bairro.Cidade.Estado.Sigla
                                }
                            }
                        }
                    }
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
                    EnderecoId = unidadeDto.EnderecoId
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
                unidade.EnderecoId = unidadeDto.EnderecoId;

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
