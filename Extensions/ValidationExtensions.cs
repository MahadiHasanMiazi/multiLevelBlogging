using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace Multi_Level_Blogging_System.Extensions;

public static class ValidationExtensions
{
    private static readonly Regex RExpressionEmail = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    // private static readonly Regex RExpressionEmail = new Regex(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$");

    public static void Required(this string text, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw AppException.Validation($"{propertyName} cannot be null or empty ");
        }
    }
    
    public static void EmailValidation(this string email)
    {
        if (!RExpressionEmail.IsMatch(email))
        {
            throw AppException.Supported("Email is not valid");
        }
    }
}