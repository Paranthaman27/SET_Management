
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SET_Management.Helpers.DbContexts;
using SET_Management.Interface;
using SET_Management.Models.DTO;
using SET_Management.Models.Entity;
using SET_Management.Repositories;
using System;
using Microsoft.AspNetCore.Session;
using System.Text;
using SET_Management.Helpers.Filters;

namespace SET_Management.Controllers
{
    [SessionAuthorize]
   // [RoleAuthorize("Driver")]
    public class VehicleController : Controller
    {
        private IApiResponseRepository _apiResponseRepository;
        private IvehicleRepository _vehicleRepose;
        public VehicleController(IApiResponseRepository apiResponseRepository, IvehicleRepository vehicleRepose)
        {
            _apiResponseRepository = apiResponseRepository;
            _vehicleRepose = vehicleRepose;
        }
        public IActionResult Vehicle()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetVehicleList()
        {
            var vehicles = _vehicleRepose.getVehicleList();
            return Json(vehicles);
        }

        [HttpGet]
        public IActionResult GetVehicleById(int id)
        {
            var vehicle = _vehicleRepose.checkVehicleExistByRegId(id);
            return Json(vehicle);
        }

        [HttpPost]
        public IActionResult SaveVehicle(mstVehicle vehicle)
        {
            if (vehicle.mstVehicleId == 0)
                return Json(_vehicleRepose.addVehicledetails(vehicle));
            else
                return Json(_vehicleRepose.editVehicledetails(vehicle));
        }

        [HttpDelete]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = new mstVehicle { mstVehicleId = id };
            return Json(_vehicleRepose.deleteVehicledetails(vehicle));
        }
    }
}