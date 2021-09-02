using EPM.Authentication.Common.Helper;
using EPM.Authentication.Model.ApiModel;
using EPM.Authentication.Model.Dto;
using EPM.Authentication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;

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


        ///// <summary>
        ///// 登录验证 允许匿名登录
        ///// </summary>
        ///// <param name="request">登录用户</param>
        ///// <returns>ResultContent实体</returns>
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("login")]
        //public async Task<ActionResult<ApiResponseWithData<DataObj>>> Post([FromBody] LoginRequestDto request)
        //{
        //    ApiResponseWithData<DataObj> result = new ApiResponseWithData<DataObj>();
        //    LoginResult loginResult = await _loginService.IsExist(request);
        //    int value = (int)loginResult.LoginStatus;
        //    // 获取描述信息
        //    string description = EnumHelper.GetEnumDesc(loginResult.LoginStatus);
        //    result.Msg = description;
        //    //result = value == 1 ? result.Success(loginResult.TokenInfo.Replace("\\", "")) : result.Fail();

        //    result.Code = 1;
        //    result.Data = new DataObj()
        //    {
        //        SData = loginResult.TokenInfo,
        //        Sign = "123123123123",
        //        Timestamp = 1630301273
        //    };
        //    result.Msg = "成功";
        //    return result;
        //}

        ///// <summary>
        ///// 登录验证 允许匿名登录
        ///// </summary>
        ///// <param name="request">登录用户</param>
        ///// <returns>ResultContent实体</returns>
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("login")]
        //public async Task<string> Post([FromBody] LoginRequestDto request)
        //{
        //    ApiResponseWithData<DataObj> result = new ApiResponseWithData<DataObj>();
        //    LoginResult loginResult = await _loginService.IsExist(request);
        //    int value = (int)loginResult.LoginStatus;
        //    // 获取描述信息
        //    string description = EnumHelper.GetEnumDesc(loginResult.LoginStatus);
        //    result.Msg = description;
        //    //result = value == 1 ? result.Success(loginResult.TokenInfo.Replace("\\", "")) : result.Fail();

        //    result.Code = 1;
        //    result.Data = new DataObj()
        //    {
        //        SData = loginResult.TokenInfo,
        //        Sign = "123123123123",
        //        Timestamp = 1630301273
        //    };
        //    result.Msg = "成功";
        //    return JsonConvert.SerializeObject(result);
        //}

        ///// <summary>
        ///// 登录验证 允许匿名登录
        ///// </summary>
        ///// <param name="request">登录用户</param>
        ///// <returns>ResultContent实体</returns>
        //[AllowAnonymous]
        //[HttpPost]
        //[Route("login")]
        //public async Task<ActionResult<ApiResponseWithData<DataObj>>> Post([FromBody] LoginRequestDto request)
        //{
        //    ApiResponseWithData<DataObj> result = new ApiResponseWithData<DataObj>();
        //    LoginResult loginResult = await _loginService.IsExist(request);
        //    int value = (int)loginResult.LoginStatus;
        //    // 获取描述信息
        //    string description = EnumHelper.GetEnumDesc(loginResult.LoginStatus);
        //    result.Msg = description;
        //    result.Code = 1;
        //    result.Data = new DataObj()
        //    {
        //        SData = "iCYyG4999p2sB5+tOQdoRKyhwlow9iW6IWA1oCPDM0xVVnPxXFDszcWT7csoFaEU3L1uODcFSvYDjyGGsRaHH8yZYvpfsDc5MKDiAsg6sJr4wTVXzNTzRPnnpUKu8AQT0qeLTZ7eBY8wZIL7RySNaNZEwq7V0exut7mGl5RPk99/mHHeJSvUGKnynNN2UxtcLU4x2udwWYn6xVnqBaTHKkjyPquffi/DrnBrFq1aAMwQtFQ1xoAumq5cNucD9Rb+jpbvN4TBoywl8a3BCsxNF0e+OsHPk/hK6ZpJvjpHeokP3zdT5j3CH/LhBXkGJ0qC",
        //        Sign = "83b3058f891b063570019de317d38e29",
        //        Timestamp = 1630301273
        //    };


        //    //result.Data = "{\"SData\":" + "\"iCYyG4999p2sB5+tOQdoRKyhwlow9iW6IWA1oCPDM0xVVnPxXFDszcWT7csoFaEU3L1uODcFSvYDjyGGsRaHH8yZYvpfsDc5MKDiAsg6sJr4wTVXzNTzRPnnpUKu8AQT0qeLTZ7eBY8wZIL7RySNaNZEwq7V0exut7mGl5RPk99/mHHeJSvUGKnynNN2UxtcLU4x2udwWYn6xVnqBaTHKkjyPquffi/DrnBrFq1aAMwQtFQ1xoAumq5cNucD9Rb+jpbvN4TBoywl8a3BCsxNF0e+OsHPk/hK6ZpJvjpHeokP3zdT5j3CH/LhBXkGJ0qC\"" + ",\"Timestamp\":\"1630301273\", "
        //    //    + "\"Sign:83b3058f891b063570019de317d38e29\"}";
        //    //result.Data = "iCYyG4999p2sB5+tOQdoRKyhwlow9iW6IWA1oCPDM0xVVnPxXFDszcWT7csoFaEU3L1uODcFSvYDjyGGsRaHH8yZYvpfsDc5MKDiAsg6sJr4wTVXzNTzRPnnpUKu8AQT0qeLTZ7eBY8wZIL7RySNaNZEwq7V0exut7mGl5RPk99/mHHeJSvUGKnynNN2UxtcLU4x2udwWYn6xVnqBaTHKkjyPquffi/DrnBrFq1aAMwQtFQ1xoAumq5cNucD9Rb+jpbvN4TBoywl8a3BCsxNF0e+OsHPk/hK6ZpJvjpHeokP3zdT5j3CH/LhBXkGJ0qC";
        //    result.Msg = "成功";
        //    return result;
        //}


        //[AllowAnonymous]
        //[HttpPut]
        //public async Task<ActionResult<ApiResponseWithData<string>>> Put([FromBody] LoginRequestDto request)
        //{
        //    ApiResponseWithData<string> result = new ApiResponseWithData<string>();
        //    string name = HttpUtility.UrlDecode(request.Password);
        //    DataObj obj = JsonConvert.DeserializeObject<DataObj>(name);
        //    LoginResult loginResult = await _loginService.IsExist(request);
        //    int value = (int)loginResult.LoginStatus;
        //    // 获取描述信息
        //    string description = EnumHelper.GetEnumDesc(loginResult.LoginStatus);
        //    result.Msg = description;
        //    result.Code = 1;
        //    //result.Data = new DataObj()
        //    //{
        //    //    SData = "iCYyG4999p2sB5+tOQdoRKyhwlow9iW6IWA1oCPDM0xVVnPxXFDszcWT7csoFaEU3L1uODcFSvYDjyGGsRaHH8yZYvpfsDc5MKDiAsg6sJr4wTVXzNTzRPnnpUKu8AQT0qeLTZ7eBY8wZIL7RySNaNZEwq7V0exut7mGl5RPk99/mHHeJSvUGKnynNN2UxtcLU4x2udwWYn6xVnqBaTHKkjyPquffi/DrnBrFq1aAMwQtFQ1xoAumq5cNucD9Rb+jpbvN4TBoywl8a3BCsxNF0e+OsHPk/hK6ZpJvjpHeokP3zdT5j3CH/LhBXkGJ0qC",
        //    //    Sign = "83b3058f891b063570019de317d38e29",
        //    //    Timestamp = 1630301273
        //    //};


        //    //result.Data = "{\"SData\":" + "\"iCYyG4999p2sB5+tOQdoRKyhwlow9iW6IWA1oCPDM0xVVnPxXFDszcWT7csoFaEU3L1uODcFSvYDjyGGsRaHH8yZYvpfsDc5MKDiAsg6sJr4wTVXzNTzRPnnpUKu8AQT0qeLTZ7eBY8wZIL7RySNaNZEwq7V0exut7mGl5RPk99/mHHeJSvUGKnynNN2UxtcLU4x2udwWYn6xVnqBaTHKkjyPquffi/DrnBrFq1aAMwQtFQ1xoAumq5cNucD9Rb+jpbvN4TBoywl8a3BCsxNF0e+OsHPk/hK6ZpJvjpHeokP3zdT5j3CH/LhBXkGJ0qC\"" + ",\"Timestamp\":\"1630301273\", "
        //    //    + "\"Sign:83b3058f891b063570019de317d38e29\"}";
        //    //result.Data = "iCYyG4999p2sB5+tOQdoRKyhwlow9iW6IWA1oCPDM0xVVnPxXFDszcWT7csoFaEU3L1uODcFSvYDjyGGsRaHH8yZYvpfsDc5MKDiAsg6sJr4wTVXzNTzRPnnpUKu8AQT0qeLTZ7eBY8wZIL7RySNaNZEwq7V0exut7mGl5RPk99/mHHeJSvUGKnynNN2UxtcLU4x2udwWYn6xVnqBaTHKkjyPquffi/DrnBrFq1aAMwQtFQ1xoAumq5cNucD9Rb+jpbvN4TBoywl8a3BCsxNF0e+OsHPk/hK6ZpJvjpHeokP3zdT5j3CH/LhBXkGJ0qC";
        //    result.Msg = "成功";
        //    return result;
        //}
    }
}
