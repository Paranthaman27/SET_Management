using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SET_Management.Helpers.DbContexts;
using SET_Management.Models.Entity;
using SET_Management.Interface;
using SET_Management.Models.DTO;

namespace SET_Management.Repositories
{

    public class vehicleRepository : IvehicleRepository
    {
        private readonly dbContext _dbContext;
        private IApiResponseRepository _apiResponseRepository;

        public vehicleRepository(dbContext dbContext, IApiResponseRepository apiResponseRepository)
        {
            _dbContext = dbContext;
            _apiResponseRepository = apiResponseRepository;
        }
        public ApiResponseDTO checkVehicleExistByRegId(int vehicleRegId)
        {
            var vehicleDetails = _dbContext.mstVehicle.Where(a => a.vehicleRegId == vehicleRegId).FirstOrDefault();
            if (vehicleDetails != null)
            {
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Vehicle Details Exist", data = vehicleDetails });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Vehicle not Found" });
        }
        public ApiResponseDTO getVehicleList()
        {
            List<mstVehicle> vehicleDetailsList = _dbContext.mstVehicle.Where(a => a.isActive == true).ToList();
            if (vehicleDetailsList != null)
            {
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Data Fetched Successfully", data = vehicleDetailsList });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Vehicle details not Found" });
        }
        public ApiResponseDTO addVehicledetails(mstVehicle vehicle)
        {
            ApiResponseDTO resultVehicleDetails = checkVehicleExistByRegId(vehicle.vehicleRegId);
            mstVehicle vehicleData = resultVehicleDetails.data;
            if (resultVehicleDetails.success != true)
            {
                _dbContext.mstVehicle.Add(vehicle);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Vehicle Details Successfully Regisered" });
            }
            else if (resultVehicleDetails.success == true && vehicleData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Vehicle Details Already Exist. State is isActive False" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Vehicle Details already Exist" });
        }
        public ApiResponseDTO editVehicledetails(mstVehicle vehicle)
        {
            ApiResponseDTO resultVehicleDetails = checkVehicleExistByRegId(vehicle.vehicleRegId);
            mstVehicle vehicleData = resultVehicleDetails.data;
            if (resultVehicleDetails.success == true && vehicleData.isActive == true)
            {
                vehicleData = vehicle;
                _dbContext.mstVehicle.Update(vehicleData);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Vehicle Details Updated Successfully " });
            }
            else if (resultVehicleDetails.success == true && vehicleData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Vehicle Details are Deleted" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Something Went wrong" });
        }
        public ApiResponseDTO deleteVehicledetails(mstVehicle vehicle)
        {
            ApiResponseDTO resultVehicleDetails = checkVehicleExistByRegId(vehicle.vehicleRegId);
            mstVehicle vehicleData = resultVehicleDetails.data;
            if (resultVehicleDetails.success == true && vehicleData.isActive == true)
            {
                vehicleData.isActive = false;
                _dbContext.mstVehicle.Update(vehicleData);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Vehicle Details Deleted Successfully " });
            }
            else if (resultVehicleDetails.success == true && vehicleData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Vehicle Details Already Deleted" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Something Went wrong" });
        }

    }
}