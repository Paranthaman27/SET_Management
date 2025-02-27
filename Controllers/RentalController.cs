
using Microsoft.AspNetCore.Mvc;
using SET_Management.Interface;
using SET_Management.Models.Entity;

namespace SET_Management.Controllers
{
    public class RentalController : Controller
    {
        private IApiResponseRepository _apiResponseRepository;
        private IrentalRepository _rentalRepose;
        public RentalController(IApiResponseRepository apiResponseRepository, IrentalRepository rentalRepose)
        {
            _apiResponseRepository = apiResponseRepository;
            _rentalRepose = rentalRepose;
        }
        public IActionResult Rental()
        {
            return View();
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