using System;
using SET_Management.Models.DTO;

namespace SET_Management.Interface
{
    public interface IApiResponseRepository
    {
        ApiResponseDTO SuccessResponse(ApiResponseDTO responseInfo);
        ApiResponseDTO FailureResponse(ApiResponseDTO responseInfo);
    }

}

