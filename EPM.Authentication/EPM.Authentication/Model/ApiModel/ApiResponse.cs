using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Authentication.Model.ApiModel
{
    /// <summary>
    /// 接口统一返回格式
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string Msg { get; set; } = string.Empty;

        public static ApiResponse Success()
        {
            return new ApiResponse()
            {
                Code = 1,
                Msg = "操作成功"
            };
        }

        public static ApiResponse Fail()
        {
            return new ApiResponse()
            {
                Code = 0,
                Msg = "操作失败"
            };
        }
    }
}
