using System.Linq;
using Coffee.API.Data;
using System;
using System.Threading.Tasks;
using FluentResults;
using Coffee.API.Data.Dtos.UserCad;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Coffee.API.Data.Dtos.Request;
using System.Web;
using Coffee.API.Model.Cadastro;
using Newtonsoft.Json;

namespace Coffee.API.Services
{
    public class InsercoesServices
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        public InsercoesServices(AppDbContext context, IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public bool InsereCadastroUser(UserModel insercoes)
        {
            try
            {
                var gg = _context.CoffeeUser.Where(usuario => usuario.USUARIO == insercoes.USUARIO)
                    .FirstOrDefault();

                if (gg != null)
                    return false;

                _context.CoffeeUser.Add(insercoes);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao inserir cadastro: {0}", ex.Message));
            }

        }

        public async Task<object> ConsultaCadastro(string usuario)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1000));
                var consulta = _context.CoffeeUser.Where(p => p.USUARIO == usuario)
                    .FirstOrDefault();

                if (consulta == null)
                    return "Usuario não encontrado!";

                var stringRetorno = JsonConvert.SerializeObject(consulta, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                return stringRetorno;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Result AtivaContaUsuario(AtivaConta request)
        {
            try
            {
                var identityUser = _userManager.Users.FirstOrDefault(user => user.Id == request.UsuarioId);

                var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.CodigoAtivacao).Result;

                if(identityResult.Succeeded)
                {
                    return Result.Ok();
                }

                return Result.Fail("Falha ao ativar conta do usuario");

            }
            catch (Exception ex )
            {
                throw ex;
            }
        }

        public Result CadastrandoUsuario(CreateUserInsert user)
        {
            try
            {
                Usuario usuario = _mapper.Map<Usuario>(user);
                IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(usuario);

                Task<IdentityResult> resultIdentity = _userManager.CreateAsync(identityUser, user.Password);

                if (resultIdentity.Result.Succeeded)
                {
                    var code = _userManager.GenerateEmailConfirmationTokenAsync(identityUser).Result;

                    var encodedCode = HttpUtility.UrlEncode(code);

                    _emailService.EnviarEmail(new[] { identityUser.Email }, "Ativação Cadastro Café", identityUser.Id, encodedCode);

                    return Result.Ok().WithSuccess(code);
                }

                object jsonErro = resultIdentity.Result.Errors;
                string jsontxt = JsonConvert.SerializeObject(jsonErro);

                return Result.Fail(jsontxt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AtualizaCadastro(UserModel insercoes)
        {
            try
            {
                UserModel user = _context.CoffeeUser.Where(p => p.USUARIO == insercoes.USUARIO).FirstOrDefault();

                if(user == null)
                    return false;

                user.USUARIO = insercoes.USUARIO;
                user.NOME = insercoes.NOME;
                user.DT_NASCIMENTO = insercoes.DT_NASCIMENTO;

               // user.Cadastro.DOCUMENTO = insercoes.Cadastro.DOCUMENTO;
                user.Cadastro.EMAIL = insercoes.Cadastro.EMAIL;
                user.Cadastro.TELEFONE = insercoes.Cadastro.TELEFONE;
                user.Cadastro.USUARIO = insercoes.Cadastro.USUARIO;

                user.Endereco.USUARIO = insercoes.Endereco.USUARIO;
                user.Endereco.RUA = insercoes.Endereco.RUA;
                user.Endereco.NUMERO = insercoes.Endereco.NUMERO;
                user.Endereco.COMPLEMENTO = insercoes.Endereco.COMPLEMENTO;
                user.Endereco.CEP = insercoes.Endereco.CEP;
                user.Endereco.BAIRRO = insercoes.Endereco.BAIRRO;
                user.Endereco.CIDADE = insercoes.Endereco.CIDADE;
                user.Endereco.ESTADO = insercoes.Endereco.ESTADO;
                user.Endereco.PAIS = insercoes.Endereco.PAIS;


                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
