using Coffee.API.Data.Dtos.Request;
using Coffee.API.Model.Login;
using Coffee.API.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Coffee.API.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginCoffee : ControllerBase
    {
        private LoginService _loginService;
        private LogoutService _logoutService;
        public LoginCoffee(LoginService loginService, LogoutService logoutService)
        {
            _loginService = loginService;
            _logoutService = logoutService;
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult Logon([FromBody] LoginRequest login)
        {
            try
            {
                Result result = _loginService.LogonUser(login);

                if (result.IsFailed)
                {
                    string tpRetorno = JsonConvert.SerializeObject(result.Errors);
                    if (tpRetorno.Contains("Lockedout"))
                        return Unauthorized("Você excedeu as 3 tentativas e agora será necessário resetar a sua senha!");

                    return Unauthorized(result.Errors);
                }

                string tokenPregerado = result.Successes[0].ToString().Split("='")[1];
                tokenPregerado = tokenPregerado.Replace("'", "").Trim();

                Username user = new Username();

                user.Usuario = login.Username;
                user.Token = tokenPregerado;

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("obterToken")]
        [Authorize]
        public IActionResult getToken()
        {
            try
            {
                var claimValuesArray = User.Claims.ToList();

                var usuario = claimValuesArray[0].Value;
                var email = claimValuesArray[2].Value;

                RetornoToken tokenResponse = new RetornoToken();
                tokenResponse.Usuario = usuario;
                tokenResponse.Email = email;

                var stringJson = JsonConvert.SerializeObject(tokenResponse);

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


        [HttpPost]
        [Route("LogoutUser")]
        public IActionResult Logoutuser()
        {
            try
            {
                Result result = _logoutService.LogoutUser();

                if (result.IsFailed)
                    return Unauthorized(result.Errors);


                return Ok(result.Successes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("SolicitaReset")]
        public IActionResult SolicitaReset(SolicitaResetRequest request)
        {
            try
            {
                Result result = _loginService.SolicitaResetSenhaUser(request);

                if (result.IsFailed) return Unauthorized(result.Errors);

                return Ok(result.Successes);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost]
        [Route("EfetuaReset")]
        public IActionResult EfetuaReset(EfetuaResetRequest request)
        {
            try
            {
                Result result = _loginService.ResetSenhaUser(request);

                if (result.IsFailed) return Unauthorized(result.Errors);

                return Ok(result.Successes);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
