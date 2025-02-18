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

namespace SET_Management.Controllers
{
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
    }
}
