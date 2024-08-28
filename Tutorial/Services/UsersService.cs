using Microsoft.AspNetCore.Identity; 

namespace IntergrateMongodb.Services
{
    public class UsersService
    {
        static public IEnumerable<IdentityError> GetSignInErrors(SignInResult result)
        {
            if (result.IsLockedOut)
            {
                yield return new IdentityError { Code = "UserLockedOut", Description = "User account is locked" };
            }

            if (result.IsNotAllowed)
            {
                yield return new IdentityError { Code = "UserNotAllowed", Description = "User is not allowed to log in" };
            }

            if (result.RequiresTwoFactor)
            {
                yield return new IdentityError { Code = "RequiresTwoFactor", Description = "Two-factor authentication is required" };
            }

            yield return new IdentityError { Code = "InvalidLoginAttempt", Description = "Invalid login attempt" };
        }
         


    }
}
