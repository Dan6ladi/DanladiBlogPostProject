using BlogPost.ApplicationService.Interface;
using BlogPost.Infrastructure.Model;
using BlogPost.SharedKernel;
using BlogPost.SharedKernel.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogPost.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("SignUp")]
        [HttpPost]
        [Produces(typeof(ResponseModel<string>))]
        public async Task<IActionResult> Register(SignUpRequest request)
        {
            if (request is null) { return BadRequest("Invalid Request"); }
            var registerResponse = await _authService.RegisterUser(request);
            if (registerResponse.StatusCode == HttpStatusCode.BadRequest || registerResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return BadRequest(registerResponse);
            }
            return Ok(registerResponse);

        }

        [Route("ThirdPartySignUp")]
        [HttpPost]
        [Produces(typeof(ResponseModel<ThirdPartyResponse>))]
        public async Task<IActionResult> ThirdPartySignUp(ThirdPartyRequest request)
        {
            if (request is null) { return BadRequest("Invalid Request"); }
            var registerResponse = await _authService.ThirdPartyRegister(request);
            if (registerResponse.StatusCode == HttpStatusCode.BadRequest || registerResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return BadRequest(registerResponse);
            }
            return Ok(registerResponse);

        }


        [Route("Login")]
        [HttpPost]
        [Produces(typeof(ResponseModel<LoginResponse>))]
        public async Task<IActionResult> Authorize(LoginRequest request)
        {
            if (request is null) { return BadRequest("Invalid Request"); }
            var loginResponse = await _authService.Login(request);
            if (loginResponse.StatusCode == HttpStatusCode.BadRequest || loginResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return BadRequest(loginResponse);
            }
            return Ok(loginResponse);

        }
    }
}
