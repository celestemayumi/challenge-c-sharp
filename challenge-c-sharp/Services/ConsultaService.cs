using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;


namespace challenge_c_sharp.Services
{
    public class ConsultaService
    {
        private readonly IGenericRepository<ConsultaDto> _consultaRepository;
        private readonly ILogger<ConsultaService> _logger;

        public ConsultaService(IGenericRepository<ConsultaDto> consultaRepository, ILogger<ConsultaService> logger)
        {
            _consultaRepository = consultaRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ConsultaDto>> GetConsultasAsync()
        {
            try
            {
                return await _consultaRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter consultas: {ex.Message}");
                throw; 
            }
        }

        public async Task<ConsultaDto> GetConsultaByIdAsync(int id)
        {
            try
            {
                return await _consultaRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter consulta com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task AddConsultaAsync(ConsultaDto consultaDto)
        {
            try
            {
                await _consultaRepository.AddAsync(consultaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar consulta: {ex.Message}");
                throw; // Re-throw a exception para o controller
            }
        }

        public async Task UpdateConsultaAsync(ConsultaDto consultaDto)
        {
            try
            {
                await _consultaRepository.UpdateAsync(consultaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar consulta: {ex.Message}");
                throw; // Re-throw a exception para o controller
            }
        }

        public async Task DeleteConsultaAsync(int id)
        {
            try
            {
                await _consultaRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir consulta com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
