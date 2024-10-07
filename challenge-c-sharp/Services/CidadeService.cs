using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;
using Microsoft.Extensions.Logging;

namespace challenge_c_sharp.Services
{
    public class CidadeService
    {
        private readonly IGenericRepository<CidadeDto> _cidadeRepository;
        private readonly ILogger<CidadeService> _logger;

        public CidadeService(IGenericRepository<CidadeDto> cidadeRepository, ILogger<CidadeService> logger)
        {
            _cidadeRepository = cidadeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CidadeDto>> GetCidadesAsync()
        {
            try
            {
                return await _cidadeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter cidades: {ex.Message}");
                throw;
            }
        }

        public async Task<CidadeDto> GetCidadeByIdAsync(int id)
        {
            try
            {
                return await _cidadeRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter cidade com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task AddCidadeAsync(CidadeDto cidadeDto)
        {
            try
            {
                await _cidadeRepository.AddAsync(cidadeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar cidade: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateCidadeAsync(int id, CidadeDto cidadeDto)
        {
            try
            {
                if (id != cidadeDto.Id)
                {
                    throw new ArgumentException("O ID da cidade fornecido não corresponde ao ID do objeto a ser atualizado.");
                }

                await _cidadeRepository.UpdateAsync(cidadeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar cidade com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteCidadeAsync(int id)
        {
            try
            {
                await _cidadeRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir cidade com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
