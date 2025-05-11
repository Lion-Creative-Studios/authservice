using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Data;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers;

/* Controller Updated to convert from "regular" controller to ApiController with the help of chatgpt 4o generated code */

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, SignInManager<UserEntity> signInManager) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly SignInManager<UserEntity> _signinManager = signInManager;

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpFormData model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.SignUpAsync(model);

        if (!result.Succeeded)
            return StatusCode(result.StatusCode, new { message = result.Error });

        return StatusCode(result.StatusCode, new { message = "Sign-up successful" });
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInFormData model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.SignInAsync(model);

        if (!result.Succeeded)
            return Unauthorized(new { message = result.Error });

        return Ok(new { message = "Sign-in successful" });
    }

    [HttpPost("signout")]
    public async Task<IActionResult> LogOut()
    {
        await _signinManager.SignOutAsync();
        return Ok(new { message = "Sign-out successful" });
    }
}

/* Controller Updated to convert from "regular" controller to ApiController with the help of chatgpt 4o generated code */
