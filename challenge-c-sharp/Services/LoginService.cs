using challenge_c_sharp.Dtos;
using challenge_c_sharp.Repositories;

namespace challenge_c_sharp.Services
{
    public class LoginService
    {
        private readonly IGenericRepository<LoginDto> _loginRepository;

        public LoginService(IGenericRepository<LoginDto> loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<IEnumerable<LoginDto>> GetLoginsAsync()
        {
            return await _loginRepository.GetAllAsync();
        }

        public async Task<LoginDto> GetLoginByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));
            }

            var login = await _loginRepository.GetByIdAsync(id);
            if (login == null)
            {
                throw new KeyNotFoundException($"Login com ID {id} não encontrado.");
            }

            return login;
        }

        public async Task AddLoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto), "Login não pode ser nulo.");
            }

            await _loginRepository.AddAsync(loginDto);
        }

        public async Task UpdateLoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto), "Login não pode ser nulo.");
            }

            await _loginRepository.UpdateAsync(loginDto);
        }

        public async Task DeleteLoginAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));
            }

            await _loginRepository.DeleteAsync(id);
        }
    }
}
