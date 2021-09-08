using EPM.Authentication.Cache;
using EPM.Authentication.Common.Date;
using EPM.Authentication.Common.Security;
using EPM.Authentication.Data.Uow;
using EPM.Authentication.Model.ApiModel;
using EPM.Authentication.Model.ConfigModel;
using EPM.Authentication.Model.DbModel;
using EPM.Authentication.Model.Dto;
using EPM.Authentication.Model.Enum;
using EPM.Authentication.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.Authentication.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ITokenInfoRepository _tokenInfoRepository;
        private readonly ITokenService _tokenService;
        private readonly LoginLockConfig _loginLockConfig;

        public LoginService(IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            ITokenInfoRepository tokenInfoRepository,
            ITokenService tokenService,
            IOptions<LoginLockConfig> options)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _tokenInfoRepository = tokenInfoRepository;
            _tokenService = tokenService;
            _loginLockConfig = options.Value;
        }


        /// <summary>
        /// 验证数据库是否存在用户
        /// </summary>
        /// <param name="req">要验证的登录用户</param>
        /// <returns>存在 返回true 否则返回false</returns>
        public async Task<LoginResult> IsExist(LoginRequestDto req)
        {
            LoginResult result = new LoginResult();
            string loginName = req.LoginName.Trim();
            // MD5加密
            string password = MD5Utility.Get32LowerMD5(req.Password).Trim();
            // 数据库查询是否有该用户
            User user = await _userRepository.GetEntityAsync(p => p.LoginName == loginName);
            if (null != user)
            {
                #region 判断锁定时间
                if (null != user.LoginLockTime)
                {
                    if ((DateTime.Now - user.LoginLockTime).Value.Minutes < _loginLockConfig.LockTime)
                    {
                        return new LoginResult
                        {
                            LoginStatus = LoginStatus.AccountLock
                        };
                    }
                }
                #endregion

                #region 验证密码
                // 验证密码
                if (user.Password.ToLower() != password)
                {
                    // await UpdateUser(user);
                    return new LoginResult
                    {
                        LoginStatus = LoginStatus.Error
                    };
                }
                #endregion

                // 判断用户当前状态
                switch ((UserStatus)user.Status)
                {
                    // 用户被冻结
                    case UserStatus.Frozen:
                        result.LoginStatus = LoginStatus.UserForzen;
                        break;
                    case UserStatus.Cancel:
                        result.LoginStatus = LoginStatus.UserCancle;
                        break;
                    default:
                        //// 查询用户级数据权限
                        //V5_DataAuthorities dataAuthority = await _dataAuthorityRepository.GetEntityAsync(p => p.UserOrGroupID == user.ID && p.Type == (int)DataAuthorityLevel.UserLevel
                        //                                    && p.IsDeleted == (int)DeleteFlag.NotDelete);
                        //if (dataAuthority == null)
                        //{
                        //    // 登录失败：基础数据权限缺失
                        //    result.LoginStatus = LoginStatus.DataAuthorityLose;
                        //    break;
                        //}

                        #region 先生成token在加密
                        //string token = await _tokenService.CreateToken($"{user.ID.ToString()}");
                        //// 加密
                        //DataObj dataObj = new DataObj()
                        //{
                        //    SData = token,
                        //    Timestamp = DateTimeUtility.GetTimestampBase1970()
                        //};
                        //// 序列化加密数据
                        //result.TokenInfo = dataObj.Encrypt();
                        #endregion

                        #region 先加密在生成token
                        DataObj dataObj = new DataObj()
                        {
                            SData = $"{user.ID.ToString()}",
                            Timestamp = DateTimeUtility.GetTimestampBase1970()
                        };

                        // 根据用户生成token信息
                        string token = await _tokenService.CreateToken(dataObj.Encrypt());
                        // 序列化加密数据
                         result.TokenInfo = token;
                        #endregion

                        result.LoginStatus = LoginStatus.Success;
                     

                        // 将生成的token存入Redis缓存
                        await RedisCoreHelper.Instance.SetValueAsync(user.ID.ToString(), result.TokenInfo);
                        #region 处理当前登录用户的token信息，每登录一次都存入数据库
                        // 插入token信息
                        TokenInfo tokenInfo = new TokenInfo()
                        {
                            UserID = user.ID,
                            TokenMsg = result.TokenInfo
                        };
                        _tokenInfoRepository.Add(tokenInfo);
                        #endregion

                        #region 更新用户数据
                        user.LoginLockTime = null;
                        // 登录成功，清空错误登录次数
                        user.LoginErrorCount = 0;
                        user.LoginTime = DateTime.Now;
                        // 用表达式树，更新部分字段
                        Expression<Func<User, object>>[] updatedProperties =
                        {
                          p=>p.LoginErrorCount,
                          p=>p.LoginLockTime,
                          p=>p.LoginTime
                        };
                        // 更新账户信息
                        _userRepository.Update(user, updatedProperties);

                        #endregion

                        // 保存数据
                        await _unitOfWork.SaveChangesAsync();
                        break;
                }

            }
            else
            {
                result.LoginStatus = LoginStatus.Error;
                result.TokenInfo = "";
            }

            return result;
        }
    }
}
