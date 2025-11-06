using Microsoft.AspNetCore.Mvc;
using Trial.DTO;

namespace TRIAL.Services
{
    public interface IRegistrationService
    {
        Task<string> Register(CreateNewAccount request);
        string Login(LoginRequest account);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordRequest model);
        //string VerifyCode(VerifyCodeRequest request);

        //Task<string> AssignRoleAsync(AssignRoleRequest request, string adminEmail);

        //Task<bool> CreateInitialAdmin(string username, string email, string password);

    }
}