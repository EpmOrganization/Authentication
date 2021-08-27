using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Model.Enum
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 冻结
        /// </summary>
        Frozen = 1,

        /// <summary>
        /// 注销
        /// </summary>
        Cancel = 2
    }
}
