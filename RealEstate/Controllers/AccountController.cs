using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models.DTO;
using RealEstate.Repositories;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RealEstateDbContext dbContext;
        private readonly IAccountRepository accountRepository;

        public AccountController(RealEstateDbContext dbContext, IAccountRepository accountRepository)
        {
            this.dbContext = dbContext;
            this.accountRepository = accountRepository;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(RegisterRequestDto registerRequestDto)
        {
            var result = await accountRepository.SignUpAsync(registerRequestDto);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginRequestDto loginRequestDto)
        {
            var result = await accountRepository.SignInAsync(loginRequestDto);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
