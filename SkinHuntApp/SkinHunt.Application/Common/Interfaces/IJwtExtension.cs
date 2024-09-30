using SkinHunt.Application.Common.Entities;

namespace SkinHunt.Application.Common.Interfaces
{
    public interface IJwtExtension
    {
        Task<string> GenerateTokenAsync(UserEntity user);
    }
}
