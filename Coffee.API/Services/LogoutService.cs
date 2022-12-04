using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;

namespace Coffee.API.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result LogoutUser()
        {
            try
            {
                var resultIdentity = _signInManager.SignOutAsync();
                if (resultIdentity.IsCompletedSuccessfully)
                    return Result.Ok();

                return Result.Fail("Falhou");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
