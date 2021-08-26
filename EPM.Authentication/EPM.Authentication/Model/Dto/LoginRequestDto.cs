using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Model.Dto
{
    public class LoginRequestDto
    {
        public string LoginName { get; set; }

        public string Password { get; set; }
    }
}
