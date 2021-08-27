using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Authentication.Common.Security
{
    public class MD5Utility
    {
        #region 获取32位大写MD5
        /// <summary>
        /// 获取32位大写MD5
        /// </summary>
        /// <param name="encrypt">要加密的明文</param>
        /// <returns>加密后的32位大写</returns>
        public static string Get32UpperMD5(string encrypt)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(encrypt));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
        #endregion

        #region 获取32位小写MD5
        /// <summary>
        /// 获取32位小写MD5
        /// </summary>
        /// <param name="encrypt">要加密的明文</param>
        /// <returns>加密后的32位小写</returns>
        public static string Get32LowerMD5(string encrypt)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(encrypt));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToLower();
            }
        }
        #endregion

        /// <summary>
        /// 获取16位大写MD5
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public static string Get16UpperMD5(string encrypt)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(encrypt));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").Substring(8, 16);
            }
        }

        /// <summary>
        /// 获取16位小写MD5
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public static string Get16LowerMD5(string encrypt)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(encrypt));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").Substring(8, 16).ToLower();
            }
        }
    }
}
