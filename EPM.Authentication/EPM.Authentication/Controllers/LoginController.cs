using EPM.Authentication.Common.Helper;
using EPM.Authentication.Model.ApiModel;
using EPM.Authentication.Model.Dto;
using EPM.Authentication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EPM.Authentication.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginService"></param>
        public LoginController(ILoginService loginService)
        {
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
            LoginResult loginResult = await _loginService.IsExist(request);
            int value = (int)loginResult.LoginStatus;
            // 获取描述信息
            string description = EnumHelper.GetEnumDesc(loginResult.LoginStatus);
            result.Msg = description;
            result = value == 1 ? result.Success(loginResult.TokenInfo) : result.Fail();
            return result;
        }
    }
}
