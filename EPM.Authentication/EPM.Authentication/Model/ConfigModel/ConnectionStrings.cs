using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Model.ConfigModel
{
    public class ConnectionStrings
    {
        public string DbConnection { get; set; }

        /// <summary>
        /// 主版本
        /// </summary>
        public int Major { get; set; }

        /// <summary>
        /// 次要版本
        /// </summary>
        public int Minor { get; set; }

        /// <summary>
        /// 编译版本
        /// </summary>
        public int Build { get; set; }
    }
}
