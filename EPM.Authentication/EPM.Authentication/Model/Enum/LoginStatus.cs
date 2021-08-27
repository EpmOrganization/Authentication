using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Model.Enum
{
    public enum LoginStatus
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        [Description("登录成功")]
        Success = 1,

        /// <summary>
        /// 登录失败,用户名或密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        Error = 2,

        /// <summary>
        /// 退出成功
        /// </summary>
        [Description("退出成功")]
        LoginOut = 3,

        /// <summary>
        /// 登录失败,该用户已被冻结
        /// </summary>
        [Description("登录失败,该用户已被冻结")]
        UserForzen = 4,

        /// <summary>
        /// 登录失败,该用户已被注销
        /// </summary>
        [Description("登录失败,该用户已被注销")]
        UserCancle = 5,

        /// <summary>
        /// 登录失败：基础数据权限缺失
        /// </summary>
        [Description("登录失败：基础数据权限缺失")]
        DataAuthorityLose = 6,

        /// <summary>
        /// 登录失败:该账号已被锁定，请稍后重试
        /// </summary>
        [Description("该账号已被锁定，请稍后重试")]
        AccountLock = 7
    }
}
