
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SET_Management.Helpers.DbContexts;
using SET_Management.Models.Entity;
using SET_Management.Interface;
using SET_Management.Models.DTO;
using System.ComponentModel.Design;

namespace SET_Management.Repositories
{

    public class rentalRepository : IrentalRepository
    {
        private readonly dbContext _dbContext;
        private IApiResponseRepository _apiResponseRepository;

        public rentalRepository(dbContext dbContext, IApiResponseRepository apiResponseRepository)
        {
            _dbContext = dbContext;
            _apiResponseRepository = apiResponseRepository;
        }
        public ApiResponseDTO checkRentalExistById(int mstRentalId)
        {
            var RentalDetails = _dbContext.mstRentalEntry.Where(a => a.mstRentalId == mstRentalId).FirstOrDefault();
            if (RentalDetails != null)
            {
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Rental Details Exist", data = RentalDetails });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Rental not Found" });
        }
        public ApiResponseDTO getRentalList()
        {
            List<mstRentalEntry> RentalDetailsList = _dbContext.mstRentalEntry.Where(a => a.isActive == true).ToList();
            if (RentalDetailsList != null)
            {
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Data Fetched Successfully", data = RentalDetailsList });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Rental details not Found" });
        }
        public ApiResponseDTO addRentaldetails(mstRentalEntry Rental)
        {
            ApiResponseDTO resultRentalDetails = checkRentalExistById(Rental.mstRentalId);
            mstRentalEntry RentalData = resultRentalDetails.data;
            if (resultRentalDetails.success != true)
            {
                _dbContext.mstRentalEntry.Add(Rental);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Rental Details Successfully Regisered" });
            }
            else if (resultRentalDetails.success == true && RentalData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Rental Details Already Exist. State is isActive False" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Rental Details already Exist" });
        }
        public ApiResponseDTO editRentaldetails(mstRentalEntry Rental)
        {
            ApiResponseDTO resultRentalDetails = checkRentalExistById(Rental.mstRentalId);
            mstRentalEntry RentalData = resultRentalDetails.data;
            if (resultRentalDetails.success == true && RentalData.isActive == true)
            {
                RentalData = Rental;
                _dbContext.mstRentalEntry.Update(RentalData);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Rental Details Updated Successfully " });
            }
            else if (resultRentalDetails.success == true && RentalData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Rental Details are Deleted" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Something Went wrong" });
        }
        public ApiResponseDTO deleteRentaldetails(mstRentalEntry Rental)
        {
            ApiResponseDTO resultRentalDetails = checkRentalExistById(Rental.mstRentalId);
            mstRentalEntry RentalData = resultRentalDetails.data;
            if (resultRentalDetails.success == true && RentalData.isActive == true)
            {
                RentalData.isActive = false;
                _dbContext.mstRentalEntry.Update(RentalData);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Rental Details Deleted Successfully " });
            }
            else if (resultRentalDetails.success == true && RentalData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Rental Details Already Deleted" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Something Went wrong" });
        }

    }
}