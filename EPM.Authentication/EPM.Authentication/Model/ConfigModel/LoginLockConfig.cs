using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Model.ConfigModel
{
    public class LoginLockConfig
    {
        /// <summary>
        /// 登录失败锁定时间
        /// </summary>
        public int LockTime { get; set; }

        /// <summary>
        /// 登录失败锁定次数
        /// </summary>
        public int LoginErrorCount { get; set; }
    }
}
