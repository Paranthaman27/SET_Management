using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SET_Management.Helpers.DbContexts;
using SET_Management.Interface;
using SET_Management.Models.DTO;
using SET_Management.Models.Entity;
using SET_Management.Repositories;
using System;
using System.Text;

namespace SET_Management.Controllers
{
    public class AuthController : Controller
    {
        private IauthRepository _authRepose;

        private IApiResponseRepository _apiResponseRepository;
        public AuthController(IApiResponseRepository apiResponseRepository, IauthRepository authRepository)
        {
            _authRepose = authRepository;
            _apiResponseRepository = apiResponseRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind] mstUser user)
        {
            ApiResponseDTO registerResult = _authRepose.registerNewUser(user);
            if (registerResult.success == true)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.vbRegisterWError = registerResult.message;
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Login(userDTO user)
        {
            var userEmail = user.userEmail;
            var userpass = user.userPassword;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                ApiResponseDTO loginResult = _authRepose.loginUser(user);
                if (loginResult.success)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.vbLoginError = loginResult.message;
                return View();
            }
        }
    }
}
