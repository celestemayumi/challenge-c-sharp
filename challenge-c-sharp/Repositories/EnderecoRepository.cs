using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Models;
using challenge_c_sharp.Config;
using challenge_c_sharp.Dtos;

namespace challenge_c_sharp.Repositories
{
    public class EnderecoRepository : IGenericRepository<EnderecoDto>
    {
        private readonly ApplicationDbContext _context;

        public EnderecoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EnderecoDto>> GetAllAsync()
        {
            try
            {
                return await _context.Enderecos
                    .Include(e => e.Bairro) // Incluindo a navegação para Bairro
                    .Select(e => new EnderecoDto
                    {
                        Id = e.Id,
                        Logradouro = e.Logradouro,
                        Numero = e.Numero,
                        CEP = e.CEP,
                        Complemento = e.Complemento,
                        BairroId = e.BairroId,
                        Bairro = new BairroDto // Mapeando BairroDto
                        {
                            Id = e.Bairro.Id,
                            Nome = e.Bairro.Nome,
                            Cidade = new CidadeDto // Mapeando CidadeDto
                            {
                                Id = e.Bairro.Cidade.Id,
                                Nome = e.Bairro.Cidade.Nome,
                                Estado = new EstadoDto // Mapeando EstadoDto
                                {
                                    Id = e.Bairro.Cidade.Estado.Id,
                                    Nome = e.Bairro.Cidade.Estado.Nome,
                                    Sigla = e.Bairro.Cidade.Estado.Sigla
                                }
                            }
                        }
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar endereços", ex);
            }
        }

        public async Task<EnderecoDto> GetByIdAsync(int id)
        {
            try
            {
                var endereco = await _context.Enderecos
                    .Include(e => e.Bairro)             // Inclui o Bairro
                         .ThenInclude(b => b.Cidade)      // Inclui a Cidade dentro do Bairro
                               .ThenInclude(c => c.Estado)  // Inclui o Estado dentro da Cidade
                     .FirstOrDefaultAsync(e => e.Id == id);

                if (endereco == null) return null;

                return new EnderecoDto
                {
                    Id = endereco.Id,
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                    CEP = endereco.CEP,
                    Complemento = endereco.Complemento,
                    BairroId = endereco.BairroId,
                    Bairro = new BairroDto // Mapeando BairroDto
                    {
                        Id = endereco.Bairro.Id,
                        Nome = endereco.Bairro.Nome,
                        Cidade = new CidadeDto 
                        {
                            Id = endereco.Bairro.Cidade.Id,
                            Nome = endereco.Bairro.Cidade.Nome,
                            Estado = new EstadoDto 
                            {
                                Id = endereco.Bairro.Cidade.Estado.Id,
                                Nome = endereco.Bairro.Cidade.Estado.Nome,
                                Sigla = endereco.Bairro.Cidade.Estado.Sigla
                            }
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar endereço com ID {id}", ex);
            }
        }

        public async Task AddAsync(EnderecoDto enderecoDto)
        {
            try
            {
                var endereco = new Endereco
                {
                    Logradouro = enderecoDto.Logradouro,
                    Numero = enderecoDto.Numero,
                    CEP = enderecoDto.CEP,
                    Complemento = enderecoDto.Complemento,
                    BairroId = enderecoDto.BairroId 
                };

                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar endereço", ex);
            }
        }

        public async Task UpdateAsync(EnderecoDto enderecoDto)
        {
            try
            {
                // Busca o endereço junto com o Bairro, Cidade e Estado
                var endereco = await _context.Enderecos
                    .Include(e => e.Bairro)
                        .ThenInclude(b => b.Cidade)
                            .ThenInclude(c => c.Estado)
                    .FirstOrDefaultAsync(e => e.Id == enderecoDto.Id);

                if (endereco == null) throw new Exception("Endereço não encontrado");

                // Atualizando as informações do Endereço
                endereco.Logradouro = enderecoDto.Logradouro;
                endereco.Numero = enderecoDto.Numero;
                endereco.CEP = enderecoDto.CEP;
                endereco.Complemento = enderecoDto.Complemento;

                // Verificando se o Bairro foi alterado
                if (endereco.Bairro.Id != enderecoDto.Bairro.Id)
                {
                    // Busca o novo Bairro e suas dependências (Cidade e Estado)
                    var bairro = await _context.Bairros
                        .Include(b => b.Cidade)
                            .ThenInclude(c => c.Estado)
                        .FirstOrDefaultAsync(b => b.Id == enderecoDto.Bairro.Id);

                    if (bairro == null) throw new Exception("Bairro não encontrado");

                    // Atualizando o Bairro associado ao Endereço
                    endereco.BairroId = bairro.Id;
                    endereco.Bairro = bairro;
                }
                else
                {
                    // Atualizando informações do Bairro
                    endereco.Bairro.Nome = enderecoDto.Bairro.Nome;

                    // Verificando se a Cidade foi alterada
                    if (endereco.Bairro.Cidade.Id != enderecoDto.Bairro.Cidade.Id)
                    {
                        var cidade = await _context.Cidades
                            .Include(c => c.Estado)
                            .FirstOrDefaultAsync(c => c.Id == enderecoDto.Bairro.Cidade.Id);

                        if (cidade == null) throw new Exception("Cidade não encontrada");

                        endereco.Bairro.Cidade = cidade;
                    }
                    else
                    {
                        // Atualizando informações da Cidade
                        endereco.Bairro.Cidade.Nome = enderecoDto.Bairro.Cidade.Nome;

                        // Verificando se o Estado foi alterado
                        if (endereco.Bairro.Cidade.Estado.Id != enderecoDto.Bairro.Cidade.Estado.Id)
                        {
                            var estado = await _context.Estados
                                .FirstOrDefaultAsync(es => es.Id == enderecoDto.Bairro.Cidade.Estado.Id);

                            if (estado == null) throw new Exception("Estado não encontrado");

                            endereco.Bairro.Cidade.Estado = estado;
                        }
                        else
                        {
                            // Atualizando informações do Estado
                            endereco.Bairro.Cidade.Estado.Nome = enderecoDto.Bairro.Cidade.Estado.Nome;
                            endereco.Bairro.Cidade.Estado.Sigla = enderecoDto.Bairro.Cidade.Estado.Sigla;
                        }
                    }
                }

                // Salvando as alterações no banco de dados
                _context.Enderecos.Update(endereco);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar endereço", ex);
            }
        }


        public async Task DeleteAsync(int id)
        {
            try
            {
                var endereco = await _context.Enderecos.FindAsync(id);
                if (endereco != null)
                {
                    _context.Enderecos.Remove(endereco);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Endereço não encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir endereço com ID {id}", ex);
            }
        }
    }
}
