using EPM.Authentication.Model.ConfigModel;
using EPM.Authentication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Authentication.Service
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;

        public TokenService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private string CreateTokenString(List<Claim> claims)
        {
            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromMinutes(_jwtConfig.AccessTokenExpiresMinutes));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.IssuerSigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,//Token发布者
                audience: _jwtConfig.Audience,    //Token接受者
                claims: claims, //携带的负载
                notBefore: now, //当前时间token生成时间
                expires: expires,//过期时间
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<string> CreateToken(string id)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("ID", id);

            //携带的负载部分，类似一个键值对
            List<Claim> claims = new List<Claim>();
            //这里我们通过键值对把数据提供给它
            foreach (var item in keyValuePairs)
            {
                claims.Add(new Claim(item.Key, item.Value));
            }
            string token = await Task.Run<string>(() =>
            {
                return CreateTokenString(claims);
            });

            return token;
        }
    }
}
