using Microsoft.AspNetCore.Identity;
using Multi_Level_Blogging_System.Models;

namespace Multi_Level_Blogging_System.Common.helper;

public class CustomPasswordValidation : IPasswordValidator<User>
{
    public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
    {
        var errors = new List<IdentityError>();

        if (password.Contains("password", StringComparison.OrdinalIgnoreCase))
        {
            errors.Add(new IdentityError { Description = "Password cannot contain the word 'password'." });
        }

        if (password.Length < 8)
        {
            errors.Add(new IdentityError { Description = "Password must be at least 8 characters long." });
        }

        return errors.Any() ? Task.FromResult(IdentityResult.Failed(errors.ToArray())) : Task.FromResult(IdentityResult.Success);
    }
}

