
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

    public class invoiceRepository : IinvoiceRepository
    {
        private readonly dbContext _dbContext;
        private IApiResponseRepository _apiResponseRepository;

        public invoiceRepository(dbContext dbContext, IApiResponseRepository apiResponseRepository)
        {
            _dbContext = dbContext;
            _apiResponseRepository = apiResponseRepository;
        }
        public ApiResponseDTO checkInvoiceExistById(int mstInvoiceId)
        {
            var InvoiceDetails = _dbContext.mstInvoice.Where(a => a.mstInvoiceId == mstInvoiceId).FirstOrDefault();
            if (InvoiceDetails != null)
            {
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Invoice Details Exist", data = InvoiceDetails });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Invoice not Found" });
        }
        public ApiResponseDTO getInvoiceList()
        {
            List<mstInvoice> InvoiceDetailsList = _dbContext.mstInvoice.Where(a => a.isActive == true).ToList();
            if (InvoiceDetailsList != null)
            {
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Data Fetched Successfully", data = InvoiceDetailsList });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Invoice details not Found" });
        }
        public ApiResponseDTO addInvoicedetails(mstInvoice Invoice)
        {
            ApiResponseDTO resultInvoiceDetails = checkInvoiceExistById(Invoice.mstInvoiceId);
            mstInvoice InvoiceData = resultInvoiceDetails.data;
            if (resultInvoiceDetails.success != true)
            {
                _dbContext.mstInvoice.Add(Invoice);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Invoice Details Successfully Regisered" });
            }
            else if (resultInvoiceDetails.success == true && InvoiceData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Invoice Details Already Exist. State is isActive False" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Invoice Details already Exist" });
        }
        public ApiResponseDTO editInvoicedetails(mstInvoice Invoice)
        {
            ApiResponseDTO resultInvoiceDetails = checkInvoiceExistById(Invoice.mstInvoiceId);
            mstInvoice InvoiceData = resultInvoiceDetails.data;
            if (resultInvoiceDetails.success == true && InvoiceData.isActive == true)
            {
                InvoiceData = Invoice;
                _dbContext.mstInvoice.Update(InvoiceData);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Invoice Details Updated Successfully " });
            }
            else if (resultInvoiceDetails.success == true && InvoiceData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Invoice Details are Deleted" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Something Went wrong" });
        }
        public ApiResponseDTO deleteInvoicedetails(mstInvoice Invoice)
        {
            ApiResponseDTO resultInvoiceDetails = checkInvoiceExistById(Invoice.mstInvoiceId);
            mstInvoice InvoiceData = resultInvoiceDetails.data;
            if (resultInvoiceDetails.success == true && InvoiceData.isActive == true)
            {
                InvoiceData.isActive = false;
                _dbContext.mstInvoice.Update(InvoiceData);
                _dbContext.SaveChanges();
                return _apiResponseRepository.SuccessResponse(new ApiResponseDTO { message = "Invoice Details Deleted Successfully " });
            }
            else if (resultInvoiceDetails.success == true && InvoiceData.isActive == false)
            {
                return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Invoice Details Already Deleted" });
            }
            return _apiResponseRepository.FailureResponse(new ApiResponseDTO { message = "Something Went wrong" });
        }

    }
}