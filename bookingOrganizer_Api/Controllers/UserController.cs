using bookingOrganizer_Api.Models;
using bookingOrganizer_Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingOrganizer_Api.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public UserController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login(User request)
        {
            var result = await _jwtService.Authenticate(request);
            if (result == null) { return Unauthorized(); }
            ;

            return result;
        }
    }
}
