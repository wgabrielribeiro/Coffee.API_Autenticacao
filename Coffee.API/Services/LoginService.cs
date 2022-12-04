using Coffee.API.Data.Dtos.Request;
using Coffee.API.Model.Login;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coffee.API.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public Result LogonUser(LoginRequest login)
        {
            try
            {
                var resultado = _signInManager.PasswordSignInAsync(login.Username, login.Password, false, true);

                if (resultado.Result.Succeeded)
                {
                    var identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario => usuario.NormalizedUserName == login.Username.ToUpper());
                    Token token = _tokenService.Createtoken(identityUser);

                    return Result.Ok().WithSuccess(token.Value);
                }

                return Result.Fail(resultado.Result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Result SolicitaResetSenhaUser(SolicitaResetRequest request)
        {
            try
            {
                IdentityUser<int> identityUser = RecuperaUsuarioPorEmail(request.Email);

                if (identityUser != null)
                {
                    string codRecuperacao = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

                    return Result.Ok().WithSuccess(codRecuperacao);
                }

                return Result.Fail("Falha ao solicitar redefinição!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Result ResetSenhaUser(EfetuaResetRequest request)
        {
            try
            {
                IdentityUser<int> identityUser = RecuperaUsuarioPorEmail(request.Email);

                IdentityResult identity = _signInManager.UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password).Result;

                if (identity.Succeeded)
                    return Result.Ok().WithSuccess("Senha redefinida com sucesso!");

                return Result.Fail("Falha ao redefinir senha!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IdentityUser<int> RecuperaUsuarioPorEmail(string email)
        {
            return _signInManager
                                .UserManager
                                .Users
                                .FirstOrDefault(user => user.NormalizedEmail == email.ToUpper());
        }

        
    }
}
