using Microsoft.AspNetCore.Mvc;
using SET_Management.Interface;
using SET_Management.Models.DTO;
using SET_Management.Models.Entity;
using SET_Management.Models;
using static SET_Management.Models.clsViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SET_Management.Controllers
{
    public class RentalController : Controller
    {
        private IApiResponseRepository _apiResponseRepository;
        private IrentalRepository _rentalRepose;
        private IcompanyRepository _companyRepose;
        private IvehicleRepository _vehicleRepose;
        public RentalController(IApiResponseRepository apiResponseRepository, IrentalRepository rentalRepose, IcompanyRepository companyRepose, IvehicleRepository vehicleRepose)
        {
            _apiResponseRepository = apiResponseRepository;
            _rentalRepose = rentalRepose;
            _companyRepose = companyRepose;
            _vehicleRepose = vehicleRepose;
        }
        public IActionResult Rental()
        {
            List<mstCompany> companyList = (_companyRepose.getcompanyList()).data;
            List<mstVehicle> vehicleList = (_vehicleRepose.getVehicleList()).data;
            clsRentalViewModel viewModel = new clsRentalViewModel();
            viewModel.lstDrpCompany = companyList;
            viewModel.lstDrpVehicle = vehicleList;
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult GetRentalList()
        {
            var Rentals = _rentalRepose.getRentalList();
            return Json(Rentals);
        }

        [HttpGet]
        public IActionResult GetRentalById(int id)
        {
            var Rental = _rentalRepose.checkRentalExistById(id);
            return Json(Rental);
        }

        [HttpPost]
        public IActionResult SaveRental(mstRentalEntry Rental)
        {
            if (Rental.mstRentalId == 0)
                return Json(_rentalRepose.addRentaldetails(Rental));
            else
                return Json(_rentalRepose.editRentaldetails(Rental));
        }

        [HttpDelete]
        public IActionResult DeleteRental(int id)
        {
            var Rental = new mstRentalEntry { mstRentalId = id };
            return Json(_rentalRepose.deleteRentaldetails(Rental));
        }
    }
}