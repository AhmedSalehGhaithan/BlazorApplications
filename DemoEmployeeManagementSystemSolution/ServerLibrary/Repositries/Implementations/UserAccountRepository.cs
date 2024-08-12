using System.Collections.Generic;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerLibrary.Data;
using ServerLibrary.Helpus;
using ServerLibrary.Repositries.Contracts;
using System.Reflection.Metadata;
using System;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace ServerLibrary.Repositries.Implementations
{
    public class UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUSerAccount
    {

        async Task<GeneralResponse> IUSerAccount.CreateAsync(Register user)
        {
            if (user is null)
                return new GeneralResponse(false, "Model is empty");
            
            var checkUser = await FindUserByEmail(user.Email!);

            if (checkUser != null)
                return new GeneralResponse(false, "User already registered");

            //save user
            var applicationUser = await AddToDatabase(new ApplicationUser()
            {
                FullName = user.FullName,
                Gamil = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            }) ;

            //check ,create and assign role
            var chechAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            if(chechAdminRole is null)
            {
                var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
                await AddToDatabase(new UserRole() { RoleId = createAdminRole.Id,UserId = applicationUser.Id }) ;
                return new GeneralResponse(true,"Account Created");
            }

            var checkUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.User));
            SystemRole response = new();
            if (checkUserRole is null)
            {
                response = await AddToDatabase(new SystemRole() { Name = Constants.Admin}) ;
                await AddToDatabase(new UserRole() { UserId=applicationUser.Id,RoleId = response.Id }) ;
            }

            else
                await AddToDatabase(new UserRole() { UserId = applicationUser.Id, RoleId = checkUserRole.Id });

            return new GeneralResponse(true, "Account Created");
        }

        public async Task<LoginResponse> SigninAsync(Login user)
        {
            if (user is null)
                return new LoginResponse(false, "Model is empty");

            var applicationUser = await FindUserByEmail(user.Email!);
            if (applicationUser is null)
                return new LoginResponse(false, "User not found");

            //varify password
            if (!BCrypt.Net.BCrypt.Verify(user.Password,applicationUser.Password))
                return new LoginResponse(false, "Email/Password is not found");

            //getting the role
            var GetUserRole = await appDbContext.UserRoles.FirstOrDefaultAsync(_ => _.UserId == applicationUser.Id);
            if (GetUserRole is null)
                return new LoginResponse(false, "User Role Not Found");


            var GetRoleName = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ =>_.Id == GetUserRole.RoleId);
            if (GetUserRole is null)
                return new LoginResponse(false, "User Role Not Found");

            string JwtToken = GenerateToken(applicationUser, GetRoleName!.Name!);
            string RefreshToken = GenerateRefreshToken();
            return new LoginResponse(true, "Login success", JwtToken, RefreshToken);

        }

        private string GenerateToken(ApplicationUser user,string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.key!));
            var credential = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName!),
                new Claim(ClaimTypes.Email,user.Gamil!),
                new Claim(ClaimTypes.Role,role)
            };

            var token = new JwtSecurityToken(
                issuer:config.Value.Issuer,
                audience:config.Value.Audience,
                claims:userClaims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:credential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        private async Task<ApplicationUser> FindUserByEmail(string email)=>
          await appDbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Gamil!.ToLower().Equals(email.ToLower()));

        private async Task<T> AddToDatabase<T>(T model)
        {
            var result = appDbContext.Add(model);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }
    }
}
