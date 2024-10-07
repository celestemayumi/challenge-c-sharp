using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;

namespace challenge_c_sharp.Services
{
    public class DentistaService
    {
        private readonly IGenericRepository<DentistaDto> _dentistaRepository;

        public DentistaService(IGenericRepository<DentistaDto> dentistaRepository)
        {
            _dentistaRepository = dentistaRepository;
        }

        public async Task<IEnumerable<DentistaDto>> GetDentistasAsync()
        {
            return await _dentistaRepository.GetAllAsync();
        }

        public async Task<DentistaDto> GetDentistaByIdAsync(int id)
        {
            return await _dentistaRepository.GetByIdAsync(id);
        }

        public async Task AddDentistaAsync(DentistaDto dentistaDto)
        {
            await _dentistaRepository.AddAsync(dentistaDto);
        }

        public async Task UpdateDentistaAsync(DentistaDto dentistaDto)
        {
            await _dentistaRepository.UpdateAsync(dentistaDto);
        }

        public async Task DeleteDentistaAsync(int id)
        {
            await _dentistaRepository.DeleteAsync(id);
        }
    }
}
