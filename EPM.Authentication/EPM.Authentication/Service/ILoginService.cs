using EPM.Authentication.Model.ApiModel;
using EPM.Authentication.Model.Dto;
using System.Threading.Tasks;

namespace EPM.Authentication.Service
{
    public interface ILoginService
    {
        Task<LoginResult> IsExist(LoginRequestDto req);
    }
}
