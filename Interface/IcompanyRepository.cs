using System;
using SET_Management.Models.DTO;
using SET_Management.Models.Entity;

namespace SET_Management.Interface
{
    public interface IcompanyRepository
    {
        ApiResponseDTO checkCompanyExistById(int companyId);
        ApiResponseDTO getcompanyList();
        ApiResponseDTO addcompanydetails(mstCompany company);
        ApiResponseDTO editcompanydetails(mstCompany company);
        ApiResponseDTO deletecompanydetails(mstCompany company);


    }
}