using helperland.Controllers.Data;
using helperland.Controllers.repo;
using helperland.Controllers.Repository;
using helperland.Models;
using helperland.Models.Data;
using helperland.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MimeKit;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System.Text.Json;

namespace helperland.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HelperlandContext helperlandContext;
        
        public HomeController(ILogger<HomeController> logger,HelperlandContext helperlandContext)
        {
            _logger = logger;
            this.helperlandContext = helperlandContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(loginView loginView)
        {
            if (ModelState.IsValid)
            {
                iUserRepository iuserRepository = new iUserRepository(helperlandContext);
                var user = iuserRepository.login(loginView);
                if (user == null)
                {
                    ModelState.AddModelError(String.Empty, "User Not Found");
                }
                else
                {
                    if (user.Password == loginView.Password)
                    {
                        HttpContext.Session.SetString("username", user.UserId.ToString());
                        return RedirectToAction("service_history");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Email or Password is incorrect");
                    }

                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create_account()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create_account(User user)
        {
            if (ModelState.IsValid)
            {
                iUserRepository iuserRepository = new iUserRepository(helperlandContext);
                if (iuserRepository.login(user) != null)
                {
                    ModelState.AddModelError("Email", "*Email already registered");
                }
                else
                {
                    iuserRepository.AddCustomer(user);
                    return RedirectToAction("index");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Become_a_provider()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Become_a_provider(User user)
        {
            if (ModelState.IsValid)
            {
                iUserRepository iuserRepository = new iUserRepository(helperlandContext);
                iuserRepository.AddServiceProvider(user);
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(loginView loginView)
        {
            if (ModelState.IsValid)
            {
                iUserRepository iuserRepository = new iUserRepository(helperlandContext);
                var user = iuserRepository.login(loginView);
                if (user == null)
                {
                    ModelState.AddModelError(String.Empty, "User Not Found");
                }
                else
                {
                    if (user.Password == loginView.Password)
                    {
                        HttpContext.Session.SetString("username", user.UserId.ToString());
                        return RedirectToAction("service_history");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Email or Password is incorrect");
                    }

                }
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Forget_password()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forget_password(ForgetPasswordView forgetPasswordView)
        {
            if (ModelState.IsValid)
            {
                iUserRepository iuserRepository = new iUserRepository(helperlandContext);
                var user = iuserRepository.login(forgetPasswordView);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "*Email isn't registered");
                }
                else
                {
                    var message = new MimeMessage();
                    // parthtemp163@gmail.com; Helperland123
                    message.From.Add(new MailboxAddress("Helperland", "parthtemp163@gmail.com"));
                    message.To.Add(new MailboxAddress("Helperland", forgetPasswordView.Email));
                    message.Subject = "For Reseting Password " + forgetPasswordView.Email;
                    message.Body = new TextPart("plain")
                    {
                        Text = "Link for reset password http://localhost:48981/home/Reset_Password?userid=" + user.UserId.ToString() 
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("parthtemp163@gmail.com", "Helperland123");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    return RedirectToAction("login");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Reset_Password([FromQuery(Name = "userid")] int userid,ResetPasswordView resetPasswordView)
        {
            resetPasswordView.UserId = userid;
            var user = helperlandContext.Users.FirstOrDefault(u => u.UserId == userid);
            if (user == null)
            { 
                return RedirectToAction("login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Reset_Password(ResetPasswordView resetPasswordView)
        {
            if (ModelState.IsValid)
            {
                var user = helperlandContext.Users.FirstOrDefault(u => u.UserId == resetPasswordView.UserId);
                if (user != null)
                {
                    user.Password = resetPasswordView.Password;
                    helperlandContext.SaveChanges();
                    return RedirectToAction("login");
                }
            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Service_history()
        {
            if(HttpContext.Session.GetString("username") != null)
            {
                return View();
            }
            return RedirectToAction("index");
        }

        public IActionResult Book_service()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                return View();
            }
            //return RedirectToAction("index");
            return View();
        }

        [HttpPost]
        public JsonResult Check_Postal([FromBody]ZipCodeView zipCodeView)
        {
            iUserRepository iuserRepository = new iUserRepository(helperlandContext);
            if (iuserRepository.IsServiceProvidedOnPostal(zipCodeView)) return Json(true);
            return Json(false);
        }

        public JsonResult Get_address()
        {
            if (HttpContext.Session.GetString("username") == null) return Json(false);
            string UserID = HttpContext.Session.GetString("username");
            iUserRepository iuserRepository = new iUserRepository(helperlandContext);
            return Json(iuserRepository.GetUserAddress(UserID));
        }

        public JsonResult Add_Address([FromBody]UserAddress userAddress)
        {
            iUserAddressRepository iuserAddressRepository = new iUserAddressRepository(helperlandContext);
            userAddress.UserId = int.Parse(HttpContext.Session.GetString("username"));
            iuserAddressRepository.AddUserAddress(userAddress);
            return Json(userAddress);
        }

        [HttpPost]
        public JsonResult Add_service([FromBody]AddService addService)
        {
            //if (HttpContext.Session.GetString("username") == null) return Json(false);
            iServiceRepository iserviceRepository = new iServiceRepository(helperlandContext);
            ServiceRequest serviceRequest = iserviceRepository.AddService(addService, HttpContext.Session.GetString("username"));
            return Json(serviceRequest.ServiceRequestId);
        }

        public IActionResult Prices()
        {
            return View();
        }

        public IActionResult ContactUS()
        {
            return View();
        }

        public IActionResult Faqs()
        {
            return View();
        }

        public IActionResult AboutUS()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
