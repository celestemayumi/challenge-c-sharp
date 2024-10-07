using challenge_c_sharp.Models;
using challenge_c_sharp.Dtos;
using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Config;

namespace challenge_c_sharp.Repositories
{
    public class PacienteRepository : IGenericRepository<PacienteDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PacienteRepository> _logger;

        public PacienteRepository(ApplicationDbContext context, ILogger<PacienteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Método para obter todos os pacientes
        public async Task<IEnumerable<PacienteDto>> GetAllAsync()
        {
            try
            {
                return await _context.Pacientes
                    .Select(p => new PacienteDto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Nascimento = p.Nascimento,
                        Email = p.Email,
                        CPF = p.CPF,
                        Telefone = p.Telefone,
                        ClienteSuspeito = p.ClienteSuspeito,
                        Genero = p.Genero,
                        Endereco = p.Endereco,
                        Login = p.Login,
                        GeneroDescricao = p.GeneroDescricao
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter pacientes: {ex.Message}");
                throw; // Re-throw a exception para o controller
            }
        }

        // Método para obter um paciente por Id
        public async Task<PacienteDto> GetByIdAsync(int id)
        {
            try
            {
                var paciente = await _context.Pacientes.FindAsync(id);
                if (paciente == null) return null;

                return new PacienteDto
                {
                    Id = paciente.Id,
                    Nome = paciente.Nome,
                    Nascimento = paciente.Nascimento,
                    Email = paciente.Email,
                    CPF = paciente.CPF,
                    Telefone = paciente.Telefone,
                    ClienteSuspeito = paciente.ClienteSuspeito,
                    Genero = paciente.Genero,
                    Endereco = paciente.Endereco,
                    Login = paciente.Login,
                    GeneroDescricao = paciente.GeneroDescricao
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter paciente com ID {id}: {ex.Message}");
                throw;
            }
        }

        // Método para adicionar um novo paciente
        public async Task AddAsync(PacienteDto pacienteDto)
        {
            try
            {
                var paciente = new Paciente
                {
                    Nome = pacienteDto.Nome,
                    Nascimento = pacienteDto.Nascimento,
                    Email = pacienteDto.Email,
                    CPF = pacienteDto.CPF,
                    Telefone = pacienteDto.Telefone,
                    ClienteSuspeito = pacienteDto.ClienteSuspeito,
                    Genero = pacienteDto.Genero,
                    Endereco = pacienteDto.Endereco,
                    Login = pacienteDto.Login
                };

                _context.Pacientes.Add(paciente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar paciente: {ex.Message}");
                throw;
            }
        }

        // Método para atualizar um paciente existente
        public async Task UpdateAsync(PacienteDto pacienteDto)
        {
            try
            {
                var paciente = await _context.Pacientes.FindAsync(pacienteDto.Id);
                if (paciente == null) throw new KeyNotFoundException($"Paciente com ID {pacienteDto.Id} não encontrado.");

                paciente.Nome = pacienteDto.Nome;
                paciente.Nascimento = pacienteDto.Nascimento;
                paciente.Email = pacienteDto.Email;
                paciente.CPF = pacienteDto.CPF;
                paciente.Telefone = pacienteDto.Telefone;
                paciente.ClienteSuspeito = pacienteDto.ClienteSuspeito;
                paciente.Genero = pacienteDto.Genero;
                paciente.Endereco = pacienteDto.Endereco;
                paciente.Login = pacienteDto.Login;

                _context.Pacientes.Update(paciente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar paciente: {ex.Message}");
                throw;
            }
        }

        // Método para excluir um paciente
        public async Task DeleteAsync(int id)
        {
            try
            {
                var paciente = await _context.Pacientes.FindAsync(id);
                if (paciente == null) throw new KeyNotFoundException($"Paciente com ID {id} não encontrado.");

                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir paciente com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
