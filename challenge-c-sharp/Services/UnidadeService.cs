using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;

namespace challenge_c_sharp.Services
{
    public class UnidadeService
    {
        private readonly IGenericRepository<UnidadeDto> _unidadeRepository;
        private readonly ILogger<UnidadeService> _logger;

        public UnidadeService(IGenericRepository<UnidadeDto> unidadeRepository, ILogger<UnidadeService> logger)
        {
            _unidadeRepository = unidadeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UnidadeDto>> GetUnidadesAsync()
        {
            try
            {
                return await _unidadeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter unidades: {ex.Message}");
                throw;
            }
        }

        public async Task<UnidadeDto> GetUnidadeByIdAsync(int id)
        {
            try
            {
                return await _unidadeRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter unidade com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task AddUnidadeAsync(UnidadeDto unidadeDto)
        {
            try
            {
                await _unidadeRepository.AddAsync(unidadeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar unidade: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateUnidadeAsync(UnidadeDto unidadeDto)
        {
            try
            {
                await _unidadeRepository.UpdateAsync(unidadeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar unidade: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteUnidadeAsync(int id)
        {
            try
            {
                await _unidadeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir unidade com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
