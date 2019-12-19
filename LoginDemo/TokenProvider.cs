using LoginDemo.CustomAttributes;
using LoginDemo.Models;
using LoginDemo.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo
{
    public class TokenProvider
    {
        public User LoginUser(User userLogin)
        {
            var key = Encoding.ASCII.GetBytes("e10adc3949ba59abbe56e057f20f883e");
            var JWToken = new JwtSecurityToken(
                issuer: "http://localhost:62403/",
                audience: "http://localhost:62403/",
                claims: GetUserClaims(userLogin),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(7)).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);

            userLogin.LOCALACCESSTOKEN = token;

            return userLogin;
        }        

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            Claim _claim;
            _claim = new Claim(ClaimTypes.Name, user.NOME);
            claims.Add(_claim);
            _claim = new Claim("CPF", user.CPF);
            claims.Add(_claim);
            _claim = new Claim("NOME", user.NOME);
            claims.Add(_claim);
            _claim = new Claim("SENHA", user.SENHA);
            claims.Add(_claim);
            _claim = new Claim("NASCIMENTO", user.NASCIMENTO.ToString("dd/MM/yyyy"));
            claims.Add(_claim);
            _claim = new Claim("PERFIL", user.PERFIL);
            claims.Add(_claim);
            _claim = new Claim(user.PERFIL, user.PERFIL);
            claims.Add(_claim);
            _claim = new Claim("APIACCESSTOKEN", user.APIACCESSTOKEN);
            claims.Add(_claim);
            return claims.AsEnumerable<Claim>();
        }
    }
}
