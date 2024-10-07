using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Models;
using challenge_c_sharp.Config;
using challenge_c_sharp.Dtos;

namespace challenge_c_sharp.Repositories
{
    public class DentistaRepository : IGenericRepository<DentistaDto>
    {
        private readonly ApplicationDbContext _context;

        public DentistaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DentistaDto>> GetAllAsync()
        {
            try
            {
                return await _context.Dentistas
                    .Select(d => new DentistaDto
                    {
                        Id = d.Id,
                        Nome = d.Nome,
                        Nascimento = d.Nascimento,
                        CRO = d.CRO,
                        Email = d.Email,
                        CPF = d.CPF,
                        Telefone = d.Telefone,
                        Genero = d.Genero,
                        GeneroDescricao = d.Genero == 1 ? "Mulher" : d.Genero == 2 ? "Homem" : "Não Definido",
                        DentistaSuspeito = d.DentistaSuspeito,
                        Login = d.Login
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar dentistas", ex);
            }
        }

        public async Task<DentistaDto> GetByIdAsync(int id)
        {
            try
            {
                var dentista = await _context.Dentistas.FindAsync(id);
                if (dentista == null) return null;

                return new DentistaDto
                {
                    Id = dentista.Id,
                    Nome = dentista.Nome,
                    Nascimento = dentista.Nascimento,
                    CRO = dentista.CRO,
                    Email = dentista.Email,
                    CPF = dentista.CPF,
                    Telefone = dentista.Telefone,
                    Genero = dentista.Genero,
                    GeneroDescricao = dentista.Genero == 1 ? "Mulher" : dentista.Genero == 2 ? "Homem" : "Não Definido",
                    DentistaSuspeito = dentista.DentistaSuspeito,
                    Login = dentista.Login
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar dentista com ID {id}", ex);
            }
        }

        public async Task AddAsync(DentistaDto dentistaDto)
        {
            try
            {
                var dentista = new Dentista
                {
                    Nome = dentistaDto.Nome,
                    Nascimento = dentistaDto.Nascimento,
                    CRO = dentistaDto.CRO,
                    Email = dentistaDto.Email,
                    CPF = dentistaDto.CPF,
                    Telefone = dentistaDto.Telefone,
                    Genero = dentistaDto.Genero,
                    Endereco = dentistaDto.Endereco,
                    DentistaSuspeito = dentistaDto.DentistaSuspeito,
                    Login = dentistaDto.Login
                };

                _context.Dentistas.Add(dentista);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar dentista", ex);
            }
        }

        public async Task UpdateAsync(DentistaDto dentistaDto)
        {
            try
            {
                var dentista = await _context.Dentistas.FindAsync(dentistaDto.Id);
                if (dentista == null) throw new Exception("Dentista não encontrado");

                dentista.Nome = dentistaDto.Nome;
                dentista.Nascimento = dentistaDto.Nascimento;
                dentista.CRO = dentistaDto.CRO;
                dentista.Email = dentistaDto.Email;
                dentista.CPF = dentistaDto.CPF;
                dentista.Telefone = dentistaDto.Telefone;
                dentista.Genero = dentistaDto.Genero;
                dentista.DentistaSuspeito = dentistaDto.DentistaSuspeito;
                dentista.Login = dentistaDto.Login;

                _context.Dentistas.Update(dentista);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar dentista", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var dentista = await _context.Dentistas.FindAsync(id);
                if (dentista != null)
                {
                    _context.Dentistas.Remove(dentista);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Dentista não encontrado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir dentista com ID {id}", ex);
            }
        }
    }
}
