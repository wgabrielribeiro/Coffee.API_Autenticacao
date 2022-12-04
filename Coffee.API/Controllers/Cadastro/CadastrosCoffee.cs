using AutoMapper;
using Coffee.API.Data;
using Coffee.API.Data.Dtos;
using Coffee.API.Data.Dtos.Request;
using Coffee.API.Data.Dtos.UserCad;
using Coffee.API.Model.Cadastro;
using Coffee.API.Services;
using FluentResults;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Coffee.API.Controllers.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cadastros : ControllerBase
    {
        private InsercoesServices _services;
        public Cadastros(InsercoesServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("Cadastro")]
        public IActionResult InsercaoCadastro([FromBody] CreateUserInsert insertUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Result resultado = _services.CadastrandoUsuario(insertUser);
                if (resultado.IsFailed)
                {
                    string txtFormatado = JsonConvert.SerializeObject(resultado.Errors);
                    if (txtFormatado.Contains("DuplicateUserName"))
                    {
                        string error = txtFormatado.Split(":\\")[2];
                        return Unauthorized(error);
                    }
                    else if (txtFormatado.Contains("DuplicateEmail"))
                    {
                        string error = txtFormatado.Split(":\\")[2];
                        return Unauthorized(error);
                    }
                    return StatusCode(500, resultado.Errors);
                }



                return Ok(resultado.Successes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        [Route("Ativa")]
        public IActionResult AtivandoCadastro([FromQuery] AtivaConta request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Result resultado = _services.AtivaContaUsuario(request);

                if (resultado.IsFailed)
                    return StatusCode(500, "Ocorreu um erro");

                return Ok("Login ativado com sucesso!");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost]
        [Route("InsercoesCadastro")]
        public IActionResult InsercaoCAD([FromBody] UserModel insertUser)
        {
            try
            {
                var date = DateTime.Now;
                var formattedDate = String.Format("{0:yyyy-MM-dd}", date);
                insertUser.DTCADASTRO = Convert.ToDateTime(formattedDate);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_services.InsereCadastroUser(insertUser))
                    return StatusCode(401, "Já existe um cadastro no sistema com esses dados! Se preferir, acesse o login e clique em esqueci a senha.");

                return Ok("Inserido com sucesso!");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        [Route("ConsultaCadastro")]
        public async Task<IActionResult> ConsultaUsuario(string pUsuario)
        {
            try
            {
                var cons = await _services.ConsultaCadastro(pUsuario);

                if (!cons.ToString().Contains("Usuario não encontrado"))
                    return Ok(cons);

                return BadRequest(cons);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        //[EnableCors("EnableCORS")]
        [HttpPost]
        [Route("AtualizaCadastro")]
        public IActionResult AtualizaCadastro([FromBody] UserModel insercoes)
        {
            try
            {
                var consulta = _services.AtualizaCadastro(insercoes);

                if (consulta == false)
                    return BadRequest("Não foi possível atualizar os dados");

                return Ok("Atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        //[HttpDelete]
        //[Route("deleteDados")]
        //public IActionResult deletaDados
    
    }
}
