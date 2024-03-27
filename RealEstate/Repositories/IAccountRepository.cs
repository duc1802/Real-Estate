using Microsoft.AspNetCore.Identity;
using RealEstate.Models.DTO;

namespace RealEstate.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(RegisterRequestDto registerRequest);
        public Task<string> SignInAsync(LoginRequestDto loginRequestDto);
    }
}
