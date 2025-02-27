﻿using SET_Management.Models.DTO;
using SET_Management.Models.Entity;

namespace SET_Management.Interface
{
    public interface IauthRepository
    {
        ApiResponseDTO registerNewUser(mstUser user);
        ApiResponseDTO loginUser(userDTO user);
        ApiResponseDTO checkUserExistByEmail(string useremail);

    }
}