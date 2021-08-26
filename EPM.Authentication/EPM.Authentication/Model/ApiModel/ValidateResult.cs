using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Authentication.Model.ApiModel
{
    /// <summary>
    /// 服务端处理返回结果
    /// </summary>
    public class ValidateResult
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string Msg { get; set; }
    }
}
