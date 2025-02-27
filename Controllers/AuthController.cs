using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SET_Management.Helpers.DbContexts;
using SET_Management.Interface;
using SET_Management.Models;
using SET_Management.Models.DTO;
using SET_Management.Models.Entity;
using SET_Management.Repositories;
using System.Collections.Generic;
using System;
using System.Text;
using static SET_Management.Models.clsCommon;

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
            HttpContext.Session.Clear();
            ViewBag.SessionId = HttpContext.Session?.GetString("userSessionId");
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
                    string jwtdata = loginResult.data.jwtToken.ToString();
                    mstUser userdata = loginResult.data.userDatas;
                    string mstUserId = userdata.mstUserId.ToString();
                    HttpContext.Session.SetString("JWTToken", jwtdata);
                    HttpContext.Session.SetString("userSessionId", mstUserId);
                    string Role = getUserType(userdata.userTypeId);
                    HttpContext.Session.SetString("Role", Role);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.vbLoginError = loginResult.message;
                return View();
            }
        }
    }
}