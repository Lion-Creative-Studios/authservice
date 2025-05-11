using Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Presentation.Data;

namespace Presentation.Services;

public interface IAuthService
{
    Task<AuthResult> SignInAsync(SignInFormData formData);
    Task<AuthResult> SignOutAsync();
    Task<AuthResult> SignUpAsync(SignUpFormData formData);
}

/* Methods in file updateded and / or validated by chatgpt 4o */
/* Use of microsoft identity for register / logins, injecting UserEntity for UserManager and SignInManager */
public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : IAuthService
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<AuthResult> SignInAsync(SignInFormData formData)
    {
        if (formData == null)
        {
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are supplied." };
        }

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersistent, false);
        return result.Succeeded
            ? new AuthResult { Succeeded = true, StatusCode = 200 }
            : new AuthResult { Succeeded = false, StatusCode = 401, Error = "Invalid email or password" };
    }

    /* Method updated to use userManager again, with help of chatgpt 4o, generated code */
    public async Task<AuthResult> SignUpAsync(SignUpFormData formData)
    {
        if (formData == null)
        {
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = "Form data is null." };
        }

        var user = new UserEntity
        {
            UserName = formData.Email,
            Email = formData.Email,
            FirstName = formData.FirstName,
            LastName = formData.LastName
        };

        var result = await _userManager.CreateAsync(user, formData.Password);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
            Console.WriteLine($"User creation failed: {errorMessages}");
            return new AuthResult { Succeeded = false, StatusCode = 400, Error = errorMessages };
        }

        return new AuthResult { Succeeded = true, StatusCode = 201 };
    }

    public async Task<AuthResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { Succeeded = true, StatusCode = 200 };
    }
}
