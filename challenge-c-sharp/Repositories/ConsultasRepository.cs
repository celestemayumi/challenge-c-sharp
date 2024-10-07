using Microsoft.EntityFrameworkCore;
using challenge_c_sharp.Models;
using challenge_c_sharp.Config;
using challenge_c_sharp.Dtos;

namespace challenge_c_sharp.Repositories
{
    public class ConsultaRepository : IGenericRepository<ConsultaDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ConsultaRepository> _logger; // Logger

        public ConsultaRepository(ApplicationDbContext context, ILogger<ConsultaRepository> logger)
        {
            _context = context;
            _logger = logger; // Inicializa o logger
        }

        // Método para obter todas as consultas
        public async Task<IEnumerable<ConsultaDto>> GetAllAsync()
        {
            try
            {
                return await _context.Consultas
                    .Include(c => c.Paciente) // Inclui Paciente na consulta
                    .Include(c => c.Dentista) // Inclui Dentista na consulta
                    .Select(c => new ConsultaDto
                    {
                        Id = c.Id,
                        DataConsulta = c.DataConsulta,
                        PacienteId = c.PacienteId,
                        DentistaId = c.DentistaId,
                        Unidade = c.Unidade,
                        Paciente = c.Paciente.Nome,
                        Dentista = c.Dentista.Nome
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter consultas: {ex.Message}");
                throw;

            }
        }

        // Método para obter uma consulta por Id
        public async Task<ConsultaDto> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Consultas
                    .Include(c => c.Paciente)
                    .Include(c => c.Dentista)
                    .Where(c => c.Id == id)
                    .Select(c => new ConsultaDto
                    {
                        Id = c.Id,
                        DataConsulta = c.DataConsulta,
                        PacienteId = c.PacienteId,
                        DentistaId = c.DentistaId,
                        Unidade = c.Unidade,
                        Paciente = c.Paciente.Nome,
                        Dentista = c.Dentista.Nome
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter consulta com ID {id}: {ex.Message}");
                throw;
            }
        }

        // Método para adicionar uma nova consulta
        public async Task AddAsync(ConsultaDto consultaDto)
        {
            try
            {
                var consulta = new Consulta
                {
                    DataConsulta = consultaDto.DataConsulta,
                    PacienteId = consultaDto.PacienteId,
                    DentistaId = consultaDto.DentistaId,
                    Unidade = consultaDto.Unidade
                };

                _context.Consultas.Add(consulta);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar consulta: {ex.Message}");
                throw; // Re-throw a exception para o controller
            }
        }

        // Método para atualizar uma consulta existente
        public async Task UpdateAsync(ConsultaDto consultaDto)
        {
            try
            {
                var consulta = await _context.Consultas.FindAsync(consultaDto.Id);
                if (consulta == null) throw new Exception("Consulta não encontrada");

                consulta.DataConsulta = consultaDto.DataConsulta;
                consulta.PacienteId = consultaDto.PacienteId;
                consulta.DentistaId = consultaDto.DentistaId;
                consulta.Unidade = consultaDto.Unidade;

                _context.Consultas.Update(consulta);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar consulta: {ex.Message}");
                throw; // Re-throw a exception para o controller
            }
        }

        // Método para excluir uma consulta
        public async Task DeleteAsync(int id)
        {
            try
            {
                var consulta = await _context.Consultas.FindAsync(id);
                if (consulta != null)
                {
                    _context.Consultas.Remove(consulta);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning($"Consulta com ID {id} não encontrada para exclusão.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir consulta com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
