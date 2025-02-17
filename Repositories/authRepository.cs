using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SET_Management.Helpers.DbContexts;
using SET_Management.Models.Entity;
using SET_Management.Interface;
using SET_Management.Models.DTO;
using System;
using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SET_Management.Repositories
{
    public class authRepository : IauthRepository
    {
        private readonly dbContext _dbContext;
        private IApiResponseRepository _apiResponseRepository;

        public authRepository(dbContext dbContext, IApiResponseRepository apiResponseRepository)
        {
            _dbContext = dbContext;
            _apiResponseRepository = apiResponseRepository;
        }


        public ApiResponseDTO checkUserExist(string useremail)
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

            if (checkUserExist(user.userEmail).success != true)
            {
                _dbContext.mstUsers.Add(user);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "User Successfully Regisered" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "User already Exist" });
        }
        public ApiResponseDTO loginUser(userDTO user)
        {
            ApiResponseDTO userDetails = checkUserExist(user.userEmail);
            if (userDetails.success)
            {
                var userData = userDetails.data;
                if (userData.userPassword == user.userPassword)
                {
                    return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "User Already Exist" });
                }
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "InValid Password" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "User Not Found" });
        }
    }



}