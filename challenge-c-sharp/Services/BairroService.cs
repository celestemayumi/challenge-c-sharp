using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;

namespace challenge_c_sharp.Services
{
    public class BairroService
    {
        private readonly IGenericRepository<BairroDto> _bairroRepository;
        private readonly ILogger<BairroService> _logger;

        public BairroService(IGenericRepository<BairroDto> bairroRepository, ILogger<BairroService> logger)
        {
            _bairroRepository = bairroRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<BairroDto>> GetBairrosAsync()
        {
            try
            {
                return await _bairroRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter bairros: {ex.Message}");
                throw;
            }
        }

        public async Task<BairroDto> GetBairroByIdAsync(int id)
        {
            try
            {
                return await _bairroRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter bairro com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task AddBairroAsync(BairroDto bairroDto)
        {
            try
            {
                await _bairroRepository.AddAsync(bairroDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar bairro: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateBairroAsync(int id, BairroDto bairroDto)
        {
            try
            {
                if (id != bairroDto.Id)
                {
                    throw new ArgumentException("O ID do bairro fornecido não corresponde ao ID do objeto a ser atualizado.");
                }

                await _bairroRepository.UpdateAsync(bairroDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar bairro com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteBairroAsync(int id)
        {
            try
            {
                await _bairroRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao excluir bairro com ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}

