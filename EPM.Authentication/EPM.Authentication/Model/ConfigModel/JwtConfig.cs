using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Model.ConfigModel
{
    /// <summary>
    /// Jwt配置信息实体
    /// </summary>
    public class JwtConfig
    {
        /// <summary>
        /// 令牌签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 令牌接收者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密key
        /// </summary>
        public string IssuerSigningKey { get; set; }

        /// <summary>
        /// 令牌过期时间
        /// </summary>
        public int AccessTokenExpiresMinutes { get; set; }
    }
}
