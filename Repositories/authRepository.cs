using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SET_Management.Helpers.DbContexts;
using SET_Management.Models.Entity;
using SET_Management.Interface;
using SET_Management.Models.DTO;
using SET_Management.Helpers.Filters;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Session;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SET_Management.Repositories
{
    public class authRepository : IauthRepository
    {
        private readonly dbContext _dbContext;
        private IApiResponseRepository _apiResponseRepository;
        public IConfiguration _configuration;

        public authRepository(dbContext dbContext, IApiResponseRepository apiResponseRepository, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _apiResponseRepository = apiResponseRepository;
            _configuration = configuration;
        }


        public ApiResponseDTO checkUserExistByEmail(string useremail)
        {
            var userDetails = _dbContext.mstUsers.Where(a => a.userEmail == useremail).FirstOrDefault();
            if (userDetails != null)
            {
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "User Details Exist", data = userDetails });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "User not Found" });
        }
        [HttpPost]
        public ApiResponseDTO registerNewUser(mstUser user)
        {
            ApiResponseDTO resultUserDetails = checkUserExistByEmail(user.userEmail);
            mstUser userData = resultUserDetails.data;
            if (resultUserDetails.success != true)
            {
                _dbContext.mstUsers.Add(user);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "User Successfully Regisered" });
            }
            else if (resultUserDetails.success == true && userData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "User Details Already Exist. State isActive False" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "User already Exist" });
        }
        public ApiResponseDTO loginUser(userDTO user)
        {
            ApiResponseDTO resultUserDetails = checkUserExistByEmail(user.userEmail);
            mstUser userData = resultUserDetails.data;

            if (resultUserDetails.success == true && userData.isActive == true)
            {
                if (userData.userPassword == user.userPassword)
                {
                    var jwttoken = JWT(userData);
                    return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "User Already Exist", data = new { userDatas = userData, jwtToken = jwttoken } });
                }
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "InValid Password" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "User Not Found" });
        }
        public string JWT(mstUser user)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.mstUserId.ToString()),
                        new Claim("DisplayName", user.userName),
                       // new Claim("UserName", user.UserName),
                        new Claim("Email", user.userEmail)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}