using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;

namespace challenge_c_sharp.Services
{
    public class PacienteService
    {
        private readonly IGenericRepository<PacienteDto> _pacienteRepository;
        private readonly ILogger<PacienteService> _logger;

        public PacienteService(IGenericRepository<PacienteDto> pacienteRepository, ILogger<PacienteService> logger)
        {
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PacienteDto>> GetPacientesAsync()
        {
            return await _pacienteRepository.GetAllAsync();
        }

        public async Task<PacienteDto> GetPacienteByIdAsync(int id)
        {
            return await _pacienteRepository.GetByIdAsync(id);
        }

        public async Task AddPacienteAsync(PacienteDto pacienteDto)
        {
            await _pacienteRepository.AddAsync(pacienteDto);
        }

        public async Task UpdatePacienteAsync(PacienteDto pacienteDto)
        {
            await _pacienteRepository.UpdateAsync(pacienteDto);
        }

        public async Task DeletePacienteAsync(int id)
        {
            await _pacienteRepository.DeleteAsync(id);
        }
    }
}
