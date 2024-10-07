using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;
using Microsoft.Extensions.Logging;

namespace challenge_c_sharp.Services
{
    public class EstadoService
    {
        private readonly IGenericRepository<EstadoDto> _estadoRepository;
        private readonly ILogger<EstadoService> _logger;

        public EstadoService(IGenericRepository<EstadoDto> estadoRepository, ILogger<EstadoService> logger)
        {
            _estadoRepository = estadoRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<EstadoDto>> GetEstadosAsync()
        {
            try
            {
                return await _estadoRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter estados: {ex.Message}");
                throw;
            }
        }

        public async Task<EstadoDto> GetEstadoByIdAsync(int id)
        {
            try
            {
                return await _estadoRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter estado com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task AddEstadoAsync(EstadoDto estadoDto)
        {
            try
            {
                await _estadoRepository.AddAsync(estadoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar estado: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateEstadoAsync(int id, EstadoDto estadoDto)
        {
            try
            {
                if (id != estadoDto.Id)
                {
                    throw new ArgumentException("O ID do estado fornecido não corresponde ao ID do objeto a ser atualizado.");
                }

                await _estadoRepository.UpdateAsync(estadoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar estado com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteEstadoAsync(int id)
        {
            try
            {
                await _estadoRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir estado com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
