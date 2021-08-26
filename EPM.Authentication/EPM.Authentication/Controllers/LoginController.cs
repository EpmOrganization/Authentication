using EPM.Authentication.Model.ApiModel;
using EPM.Authentication.Model.Dto;
using EPM.Authentication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILoginService _loginService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenService"></param>
        /// <param name="loginService"></param>
        public LoginController(ITokenService tokenService, ILoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }

        /// <summary>
        /// 登录验证 允许匿名登录
        /// </summary>
        /// <param name="request">登录用户</param>
        /// <returns>ResultContent实体</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ApiResponseWithData<string>>> Post([FromBody] LoginRequestDto request)
        {
            ApiResponseWithData<string> result = new ApiResponseWithData<string>();
            //LoginResult loginResult = await _loginService.IsExist(request);
            //int value = (int)loginResult.LoginStatus;
            //// 获取描述信息
            //string description = EnumHelper.GetEnumDesc(loginResult.LoginStatus);
            //result.ResultMsg = description;
            //if (value == 1)
            //{
            //    result.ResultCode = ResultCode.Success;
            //    // 赋值当前用户的Token信息 
            //    result.Data = loginResult.TokenInfo;
            //}
            //else
            //{
            //    result.ResultCode = ResultCode.Fail;
            //}
            return result;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("logOut")]
        public async Task<ActionResult<ApiResponse>> LogOut()
        {
            ApiResponse result = new ApiResponse();
            //LoginStatus loginStatus = await _loginService.LoginOut();
            //result.ResultCode = ResultCode.Success;
            //result.ResultMsg = "退出成功";
            return result;
        }
    }
}
