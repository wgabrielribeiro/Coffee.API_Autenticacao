using Coffee.API.Model.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Coffee.API.Services
{
    public class TokenService
    {

        public Token Createtoken(IdentityUser<int> usuario)
        {
            try
            {
                Claim[] direitosUsuario = new Claim[]
                {
                    new Claim("username", usuario.UserName),
                    new Claim("id", usuario.Id.ToString()),
                    new Claim("email", usuario.Email)
                };

                var chave = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("ushdisbdiusabdubsadi08sadas44asd85s4dsw84"));

                var credentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: direitosUsuario,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddHours(1)
                    );

                var tokenStrint = new JwtSecurityTokenHandler().WriteToken(token);

                return new Token(tokenStrint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            
            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        



    }
}
