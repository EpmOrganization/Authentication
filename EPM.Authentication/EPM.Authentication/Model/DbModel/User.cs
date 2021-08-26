using EPM.Authentication.Model.DbModel.Base;
using System;

namespace EPM.Authentication.Model.DbModel
{
    public class User : BaseModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public Guid RoleID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 用户状态  0：正常 1 冻结 2：注销
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 密码修改时间
        /// </summary>
        public DateTime? PasswordUpdateTime { get; set; }

        /// <summary>
        /// 登录错误次数
        /// </summary>
        public int LoginErrorCount { get; set; }

        /// <summary>
        /// 登录锁定时间
        /// </summary>
        public DateTime? LoginLockTime { get; set; }
    }
}
