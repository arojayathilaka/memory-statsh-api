using memory_stash.Data.Models;
using memory_stash.Data.Services;
using memory_stash.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memory_stash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Authenticate(UserAuth userAuth)
        {
            var token = _authService.Authenticate(userAuth);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }

   
}
