using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositries.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUSerAccount accountInterface) : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<IActionResult>CreateAsync(Register user)
        {
            if (user == null) return BadRequest("Model is empty");
            var result = await accountInterface.CreateAsync(user);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> SigninAsync(Login user)
        {
            if(user == null) return BadRequest("Model is empty");
            var result = await accountInterface.SigninAsync(user);
            return Ok(result);
        }
    }
}
