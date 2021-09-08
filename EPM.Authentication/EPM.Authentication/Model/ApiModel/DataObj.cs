using EPM.Authentication.Common.Security;
using Newtonsoft.Json;

namespace EPM.Authentication.Model.ApiModel
{
    public class DataObj
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        public string SData { get; set; }

        /// <summary>
        /// 签名  MD5 32位小写
        /// </summary>
        public string Sign { get; set; }


        public string Encrypt()
        {
            Sign = MD5Utility.Get32LowerMD5(SData);
            string key = MD5Utility.Get32LowerMD5(Sign);
            string iv = MD5Utility.Get16LowerMD5(Timestamp.ToString());
            string result = AESUtility.AESEncrypt(SData, key, iv);
            return JsonConvert.SerializeObject(new
            {
                Data = result,
                Timestamp = Timestamp,
                Sign = Sign
            });
        }

        public string Dencrypt()
        {
            string key = MD5Utility.Get32LowerMD5(Sign);
            string iv = MD5Utility.Get16LowerMD5(Timestamp.ToString());
            string result = AESUtility.AESDecrypt(SData, key, iv);
            return result;
        }
    }
}
