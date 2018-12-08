using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointmentApp.ViewModels;

namespace AppointmentApp.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            CreateUserViewModel cuvm = new CreateUserViewModel();

            using (AppointDBContext _context = new AppointDBContext())
            {
                cuvm.Roles = _context.Roles.ToList();
            }

            return View(cuvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreData(CreateUserViewModel newData)
        {
            CreateUserViewModel cuvm = new CreateUserViewModel();
            if (!ModelState.IsValid)
            {
                using (AppointDBContext _context = new AppointDBContext())
                {
                    cuvm.Roles = _context.Roles.ToList();
                    cuvm.User = newData.User;
                }
                return View("Index",cuvm);
            }
            else
            {
                using (AppointDBContext _context = new AppointDBContext())
                {
                   var user =  _context.Users.SingleOrDefault(u=>u.Email==newData.User.Email);

                    if (user != null)
                    {
                        cuvm.Roles = _context.Roles.ToList();
                        cuvm.User = newData.User;
                        ModelState.AddModelError("User.Email","Email is already exists!!");
                        return View("Index", cuvm);
                    }
                    else
                    {
                        if (newData.User.RoleId==2)
                        {
                            newData.User.IsADoctor = true;
                        }
                        _context.Users.Add(newData.User);
                        _context.SaveChanges();
                    }


                }
             }

            return RedirectToAction("LogIn");
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateLogIn(LogInViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View("LogIn",data);
            }
            else
            {
                using(AppointDBContext _context = new AppointDBContext())
                {
                    var user = _context.Users.SingleOrDefault(u=>u.Email == data.Email);
                    if (user != null)
                    {
                        if (user.Password == data.Password)
                        {
                            Session["loged_user_id"] = user.Id;
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Password does not match!!!");
                            return View("LogIn", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email","User not found with this email!!");
                        return View("LogIn", data);
                    }
                }
            }
        }
    }
}