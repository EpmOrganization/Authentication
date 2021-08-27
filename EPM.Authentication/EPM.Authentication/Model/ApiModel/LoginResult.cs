using EPM.Authentication.Model.Enum;

namespace EPM.Authentication.Model.ApiModel
{
    /// <summary>
    /// 登录验证结果
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 枚举
        /// </summary>
        public LoginStatus LoginStatus { get; set; }

        /// <summary>
        /// 登录验证通过,存放当前用户的Token信息
        /// </summary>
        public string TokenInfo { get; set; }
    }
}
