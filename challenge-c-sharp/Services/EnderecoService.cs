using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;

namespace challenge_c_sharp.Services
{
    public class EnderecoService
    {
        private readonly IGenericRepository<EnderecoDto> _enderecoRepository;
        private readonly ILogger<EnderecoService> _logger;

        public EnderecoService(IGenericRepository<EnderecoDto> enderecoRepository, ILogger<EnderecoService> logger)
        {
            _enderecoRepository = enderecoRepository;
            _logger = logger;
        }

        public async Task <IEnumerable<EnderecoDto>> GetEnderecosAsync()
        {
            try
            {
                return await _enderecoRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter endereco:{ex.Message}");
                throw;
            }
        }

        public async Task <EnderecoDto> GetEnderecoByIdAsync(int id)
        {
            try
            {
                return await _enderecoRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter endereco por ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task AddEnderecoAsync(EnderecoDto enderecoDto)
        {
            try
            {
                await _enderecoRepository.AddAsync(enderecoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar um novo endereco: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateEnderecoAsync(EnderecoDto enderecoDto)
        {
            try
            {
                await _enderecoRepository.UpdateAsync(enderecoDto);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro ao atualizar endereco: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteEnderecoAsync(int id)
        {
            try
            {
                await _enderecoRepository.DeleteAsync(id);
            }
            catch( Exception ex)
            {
                _logger.LogError($"Erro ao excluir endereco com o ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
