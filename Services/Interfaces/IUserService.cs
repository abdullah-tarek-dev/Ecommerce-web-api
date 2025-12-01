using Ecommerce.DTOs.Users;

namespace Ecommerce.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<List<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> CreateAsync(UserRegisterDto dto);
        Task<UserResponseDto?> UpdateAsync(int id, UserRegisterDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
