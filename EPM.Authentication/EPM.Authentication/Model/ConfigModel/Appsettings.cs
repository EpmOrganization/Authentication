using EPM.Authentication.Cache;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EPM.Authentication.Model.ConfigModel
{
    public class Appsettings
    {
        private static Appsettings single;

        /// <summary>
        /// 单例
        /// </summary>
        /// <returns></returns>
        public static Appsettings Instance()
        {
            if (single != null)
                return single;
            Appsettings temp = new Appsettings();
            Interlocked.CompareExchange(ref single, temp, null);

            return single;
        }

        private string _RedisConnStr;
        public string RedisConnStr
        {
            get { return _RedisConnStr; }
            set
            {
                _RedisConnStr = value;
                RedisCoreHelper.SetRedisConnStr(value);
            }
        }

        public void Initial(IConfiguration configuration)
        {
            RedisConnStr = configuration.GetValue("RedisConnStr", "local:6379,defaultdatabase=0");
        }
    }
}
