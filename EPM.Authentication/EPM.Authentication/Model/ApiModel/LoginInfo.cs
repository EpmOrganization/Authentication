using EPM.Authentication.Model.DbModel;

namespace EPM.Authentication.Model.ApiModel
{
    /// <summary>
    /// 存放当前登录用户信息
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public User LoginUser { get; set; }

        ///// <summary>
        ///// 当前登录用户的数据权限
        ///// </summary>
        //public V5_DataAuthorities DataAuthority { get; set; }

        ///// <summary>
        ///// 功能权限
        ///// </summary>
        //public Dictionary<string, Role> Role { get; set; }
    }
}
