
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SET_Management.Helpers.Filters;
using SET_Management.Interface;
using SET_Management.Models.DTO;
using SET_Management.Models.Entity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SET_Management.Controllers
{
    [SessionAuthorize]
    // [RoleAuthorize("Driver")]
    public class CompanyController : Controller
    {
        private IApiResponseRepository _apiResponseRepository;
        private IcompanyRepository _companyRepose;
        public CompanyController(IApiResponseRepository apiResponseRepository, IcompanyRepository companyRepose)
        {
            _apiResponseRepository = apiResponseRepository;
            _companyRepose = companyRepose;
        }
        public IActionResult Company()
        {

            var companies = _companyRepose.getcompanyList();
            List<mstCompany> companyList = companies.data;
            ApiResponseDTO viewModel = new ApiResponseDTO();
            viewModel.data = companyList;
            return View(viewModel.data);
        }
        [HttpGet]
        public IActionResult GetCompanyList()
        {
            var companies = _companyRepose.getcompanyList();
            return Json(companies);
        }

        [HttpGet]
        public IActionResult GetCompanyById(int id)
        {
            var company = _companyRepose.checkCompanyExistById(id);
            return Json(company);
        }

        [HttpPost]
        public IActionResult SaveCompany(mstCompany company)
        {
            if (company.mstCompanyId == 0)
                return Json(_companyRepose.addcompanydetails(company));
            else
                return Json(_companyRepose.editcompanydetails(company));
        }

        [HttpDelete]
        public IActionResult DeleteCompany(int id)
        {
            var company = new mstCompany { mstCompanyId = id };
            return Json(_companyRepose.deletecompanydetails(company));
        }

    }
}