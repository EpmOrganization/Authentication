using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Authentication.Model.ApiModel
{
    public class ApiResponseWithData<T> : ApiResponse
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 返回数据的数量
        /// </summary>
        public int Count { get; set; } = 0;

        public new ApiResponseWithData<T> Success()
        {
            return new ApiResponseWithData<T>()
            {
                Code = 1,
                Msg = "操作成功"
            };
        }

        public ApiResponseWithData<T> Success(T data, int count = 0)
        {
            return new ApiResponseWithData<T>()
            {
                Code = 1,
                Msg = "操作成功",
                Data = data,
                Count = count
            };
        }

        public new ApiResponseWithData<T> Fail()
        {
            return new ApiResponseWithData<T>()
            {
                Code = 0,
                Msg = "操作失败"
            };
        }
    }
}
