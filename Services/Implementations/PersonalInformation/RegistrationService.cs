using System.Net;
using System;
using Trial.DTO;
using TRIAL.Persistence.entity;
using System.Collections.Generic;
using TRIAL.Persistence.Repository;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using EncryptDecrypt;
using EmailSending;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TRIAL.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        //database connection
        private readonly AppDBContext appdbContext;
        private readonly DatabaseSettings dBSettings;
        private readonly Emailsending _emailSending;
        public RegistrationService(AppDBContext appDbContext, IOptions<DatabaseSettings> dbSettings, Emailsending emailSending)//, VerificationRegister verificationregister, AssigningRole assigningrole)
        {
            appdbContext = appDbContext;
            dBSettings = dbSettings.Value;
            _emailSending = emailSending;
        }


        public async Task<string> Register(CreateNewAccount request)
        {
            var existingUser = await appdbContext.registrations
                                        .SingleOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return "EmailExists";
            }

            string passwordHash = EnDePassword.ConvertToEncrypt(request.Password);

            var user = new Registration
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth.Value,
                Address = request.Address,
                Role = request.Role
            };

            try
            {
                // Save the user to the database
                appdbContext.registrations.Add(user);
                await appdbContext.SaveChangesAsync();
                return "Success";
            }
            catch (Exception)
            {
                // Log exception (optional)
                return "Error";
            }
        }

        public string Login(LoginRequest account)
        {
            var user = appdbContext.registrations.SingleOrDefault(u => u.Email == account.Email);
            Console.WriteLine($"DEBUG: Searching for {account.Email}");
            if (user == null) return "NotFound";

            string decryptedPassword = EnDePassword.ConvertToDecrypt(user.PasswordHash);
            if (decryptedPassword != account.Password) return "Invalid Email or Password";
            // for authorization//
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ThisIsASuperLongSecretKeyForJWTToken12345"); // match Program.cs

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            //....//
        }


        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await appdbContext.registrations.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return "User not found.";
            }

            var token = Guid.NewGuid().ToString(); //generates a Globally Unique IDentifier....128-bit number
            user.PasswordResetToken = token;
            user.ResetTokenExpiration = DateTime.Now.AddHours(1);
            appdbContext.registrations.Update(user); //This updates the user's record with the new token and expiration time.
            await appdbContext.SaveChangesAsync();

            var resetLink = $"http://localhost:5000/api/account/resetpassword?token={token}&email={email}";
            await _emailSending.SendEmail(email, "Password Reset Request", $"Click <a href='{resetLink}'>here</a> to reset your password.");

            return "Password reset token generated.";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest model) //excuted after the user will click on the link that has been sent to him
        {
            var user = await appdbContext.registrations.FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordResetToken == model.Token);
            if (user == null || user.ResetTokenExpiration < DateTime.Now)
            {
                return "Invalid token or token expired.";
            }

            user.PasswordHash = EnDePassword.ConvertToEncrypt(model.NewPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpiration = null;
            appdbContext.registrations.Update(user);
            await appdbContext.SaveChangesAsync();

            return "Password reset successful.";
        }
    }
}