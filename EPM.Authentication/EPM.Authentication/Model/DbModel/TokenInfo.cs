using EPM.Authentication.Model.DbModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Model.DbModel
{
    public class TokenInfo 
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        /// <summary>
        ///  对应v5_users表的ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string TokenMsg { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; } = DateTime.Now;
    }
}
