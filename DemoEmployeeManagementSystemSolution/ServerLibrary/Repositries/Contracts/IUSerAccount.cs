
using BaseLibrary.DTOs;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositries.Contracts
{
    public interface IUSerAccount
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> SigninAsync(Login user);

    }
}
