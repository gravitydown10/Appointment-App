using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointmentApp.Models;
using System.IO;
using AppointmentApp.ViewModels;

namespace AppointmentApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            if (Session["loged_user_id"]!=null)
            {
                UserViewModel uvm = new UserViewModel();

                using (AppointDBContext _context = new AppointDBContext())
                {
                    uvm.User = _context.Users.Find((int)Session["loged_user_id"]);


                    if (uvm.User.SpecializationId == null && uvm.User.IsADoctor == true)
                    {
                        return RedirectToAction("AddSpecialization");
                    }

                    uvm.Image = _context.Images.SingleOrDefault(i => i.UserId == uvm.User.Id);
                    uvm.Specialization = _context.Specializations.SingleOrDefault(sp => sp.Id == uvm.User.SpecializationId);
                }

                return View(uvm);
            }
            else
            {
                return RedirectToAction("LogIn","Registration");
            }
        }

        public ActionResult UploadPhoto()
        {


            if (Session["loged_user_id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }


            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreImage(HttpPostedFileBase file)
        {

            if (Session["loged_user_id"] != null)
            {
                using (AppointDBContext _context = new AppointDBContext())
                {
                    if (file != null && file.ContentLength > 0 && file.ContentType.Contains("image") &&
                         Path.GetExtension(file.FileName).ToLower() != ".gif")
                    {
                        using (var reader = new BinaryReader(file.InputStream))
                        {
                            var imageForUser = new Image()
                            {
                                UserId = (int)Session["loged_user_id"],
                                ImageContent = reader.ReadBytes(file.ContentLength),
                                ImageType = file.ContentType
                            };

                            var newUserImage = _context.Images.SingleOrDefault(ui => ui.UserId == imageForUser.UserId);
                            if (newUserImage == null)
                            {
                                _context.Images.Add(imageForUser);
                                _context.SaveChanges();
                            }
                            else
                            {
                                var existingUser = _context.Images.SingleOrDefault(ui => ui.Id == newUserImage.Id);
                                existingUser.ImageContent = imageForUser.ImageContent;
                                existingUser.ImageType = imageForUser.ImageType;
                                _context.SaveChanges();
                            }

                            return RedirectToAction("Index", "User");
                        }
                    }
                    return Content("only .jpg, .jpeg, .png are allowed. <a href='/User/UploadPhoto'> back to previous.");

                }
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }
        }

        public ActionResult EditProfile(int id)
        {


            if (Session["loged_user_id"] != null)
            {

                UserEditViewModel uevm = new UserEditViewModel();

                using (AppointDBContext _context = new AppointDBContext())
                {
                    var user = _context.Users.SingleOrDefault(u => u.Id == id);
                    uevm.Id = user.Id;
                    uevm.Telephone = user.Telephone;
                    uevm.Mobile = user.Mobile;
                    uevm.Address = user.Address;
                }

                return View(uevm);
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreEditProfile(UserEditViewModel data)
        {



            if (Session["loged_user_id"] != null)
            {
                if (!ModelState.IsValid)
                {

                    return View("EditProfile", data);
                }
                else
                {
                    using (AppointDBContext _context = new AppointDBContext())
                    {

                        var user = _context.Users.SingleOrDefault(u => u.Id == data.Id);
                        user.Mobile = data.Mobile;
                        user.Telephone = data.Telephone;
                        user.Address = data.Address;

                        _context.SaveChanges();

                        return RedirectToAction("Index","User");
                    }
                }
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }
        }

        public ActionResult AddSpecialization()
        {


            if (Session["loged_user_id"] != null)
            {
                SpecializationViewModel svm = new SpecializationViewModel();

                using (AppointDBContext _context = new AppointDBContext())
                {
                    svm.Specializations = _context.Specializations.ToList();
                    svm.Id = (int)Session["loged_user_id"];
                }
                return View(svm);
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreSpecialization(SpecializationViewModel data)
        {


            if (Session["loged_user_id"] != null)
            {
                if (!ModelState.IsValid)
                {
                    SpecializationViewModel svm = new SpecializationViewModel();

                    using (AppointDBContext _context = new AppointDBContext())
                    {
                        svm.Specializations = _context.Specializations.ToList();
                        svm.Id = (int)Session["loged_user_id"];
                    }
                    return View("AddSpecialization", svm);
                }

                using (AppointDBContext _context = new AppointDBContext())
                {
                    var user = _context.Users.Find(data.Id);
                    user.SpecializationId = data.SpecialField;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }
        }

        public ActionResult LogOut()
        {
            Session["loged_user_id"] = null;
            return RedirectToAction("LogIn","Registration");
        }


        public ActionResult CreateAppointment()
        {
            if (Session["loged_user_id"] != null)
            {
                AppointmentViewModel avm = new AppointmentViewModel();

                using (AppointDBContext _context = new AppointDBContext())
                {
                    var specializations = _context.Specializations.ToList();
                    var schedules = _context.Schedules.ToList();
                    avm.Schedules = schedules;
                    avm.Specializations = specializations;
                }
                return View(avm);
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }

        }

        public JsonResult GetDoctors(int spId)
        {

            using(AppointDBContext _context = new AppointDBContext())
            {
                IList<User> doctors = _context.Users.Where(u=>u.SpecializationId==spId).ToList();
                return Json(doctors,JsonRequestBehavior.AllowGet);
            } 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreAppointment(AppointmentViewModel data)
        {
            if (Session["loged_user_id"] != null)
            {

                AppointmentViewModel avm = new AppointmentViewModel();
                if (!ModelState.IsValid)
                {
                    

                    using (AppointDBContext _context = new AppointDBContext())
                    {
                        var specializations = _context.Specializations.ToList();
                        var schedules = _context.Schedules.ToList();
                        avm.Schedules = schedules;
                        avm.Specializations = specializations;
                        IList<User> doctors = _context.Users.Where(u => u.SpecializationId == data.Appointment.DoctorId).ToList();
                        avm.Users = doctors;
                    }

                    return View("CreateAppointment", avm);
                }
                else
                {
                    using (AppointDBContext _context = new AppointDBContext())
                    {
                        int id = (int)Session["loged_user_id"];
                        var appointment = _context.Appointments.SingleOrDefault(ap=>ap.UId == id && ap.DoctorId==data.Appointment.DoctorId && ap.Date==data.Appointment.Date&&ap.ScheduleId==data.Appointment.ScheduleId);
                        if (appointment!=null)
                        {
                            AppointmentViewModel avm1 = new AppointmentViewModel();
                            var specializations = _context.Specializations.ToList();
                            var schedules = _context.Schedules.ToList();
                            avm1.Schedules = schedules;
                            avm1.Specializations = specializations;
                            IList<User> doctors = _context.Users.Where(u => u.SpecializationId == data.Appointment.DoctorId).ToList();
                            avm1.Users = doctors;
                            ModelState.AddModelError("Appointment.Date","an appointment is already exists with same entry!!!");
                            return View("CreateAppointment", avm1);
                        }
                    }


                    using (AppointDBContext _context = new AppointDBContext())
                    {
                        Appointment appointment = new Appointment();

                        appointment.UId = (int)Session["loged_user_id"];
                        appointment.DoctorId = data.Appointment.DoctorId;
                        appointment.Date = data.Appointment.Date;
                        appointment.ScheduleId = data.Appointment.ScheduleId;

                        _context.Appointments.Add(appointment);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }

        }

        public ActionResult CheckAppointments()
        {
            if (Session["loged_user_id"] != null)
            {
                List<CheckAppointmentViewModel> cavms = new List<CheckAppointmentViewModel>();

                int x = (int)Session["loged_user_id"];
                using (AppointDBContext _context = new AppointDBContext())
                {


                    var userForCheckingDoctor = _context.Users.Find(x);
                    if (userForCheckingDoctor.IsADoctor == true)
                    {
                        ViewBag.flag = true;
                        var checkUser = _context.Appointments.Where(ap => ap.DoctorId == x).ToList();
                        if (checkUser != null)
                        {

                            foreach (Appointment item in checkUser)
                            {
                                CheckAppointmentViewModel cavm = new CheckAppointmentViewModel();
                                var user = _context.Users.Find((int)Session["loged_user_id"]);
                                cavm.UserName = user.Name;
                                cavm.UserEmail = user.Email;

                                var patientUser = _context.Users.SingleOrDefault(u => u.Id == item.UId);
                                cavm.PatientName = patientUser.Name;

                                var specialist = _context.Specializations.Find(user.SpecializationId);
                                cavm.Specialist = specialist.Name;

                                var time = _context.Schedules.Find(item.ScheduleId);
                                cavm.Time = time.Time;

                                cavm.Date = item.Date;

                                cavms.Add(cavm);
                            }

                        }
                    }
                    else
                    {
                        ViewBag.flag = false;
                        var checkUser = _context.Appointments.Where(ap => ap.UId == x).ToList();
                        if (checkUser != null)
                        {

                            foreach (Appointment item in checkUser)
                            {
                                CheckAppointmentViewModel cavm = new CheckAppointmentViewModel();
                                var user = _context.Users.Find((int)Session["loged_user_id"]);
                                cavm.UserName = user.Name;
                                cavm.UserEmail = user.Email;

                                var doctorUser = _context.Users.SingleOrDefault(u => u.Id == item.DoctorId);
                                cavm.DoctorName = doctorUser.Name;

                                var specialist = _context.Specializations.Find(doctorUser.SpecializationId);
                                cavm.Specialist = specialist.Name;

                                var time = _context.Schedules.Find(item.ScheduleId);
                                cavm.Time = time.Time;

                                cavm.Date = item.Date;

                                cavms.Add(cavm);
                            }

                        }
                    }




                }

                return View(cavms);
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }
   
        }

        public ActionResult CheckAppointmentsByDate()
        {
            if (Session["loged_user_id"] != null)
            {

                List<CheckAppointmentViewModel> cavms = new List<CheckAppointmentViewModel>();

                int x = (int)Session["loged_user_id"];
                using (AppointDBContext _context = new AppointDBContext())
                {
                    var checkUser = _context.Appointments.Where(ap => ap.UId == x).ToList();
                    if (checkUser != null)
                    {

                        foreach (Appointment item in checkUser)
                        {
                            CheckAppointmentViewModel cavm = new CheckAppointmentViewModel();
                            var user = _context.Users.Find((int)Session["loged_user_id"]);
                            cavm.UserName = user.Name;
                            cavm.UserEmail = user.Email;

                            var doctorUser = _context.Users.SingleOrDefault(u => u.Id == item.DoctorId);
                            cavm.DoctorName = doctorUser.Name;

                            var specialist = _context.Specializations.Find(doctorUser.SpecializationId);
                            cavm.Specialist = specialist.Name;

                            var time = _context.Schedules.Find(item.ScheduleId);
                            cavm.Time = time.Time;

                            cavm.Date = item.Date;

                            cavms.Add(cavm);
                        }

                    }
                }

                return View(cavms);
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }


        }

        public ActionResult CheckAppointmentsDoctor()
        {
            if (Session["loged_user_id"] != null)
            {
                List<CheckAppointmentViewModel> cavms = new List<CheckAppointmentViewModel>();

                int x = (int)Session["loged_user_id"];
                using (AppointDBContext _context = new AppointDBContext())
                {
                    var checkUser = _context.Appointments.Where(ap => ap.UId == x).ToList();
                    if (checkUser != null)
                    {

                        foreach (Appointment item in checkUser)
                        {
                            CheckAppointmentViewModel cavm = new CheckAppointmentViewModel();
                            var user = _context.Users.Find((int)Session["loged_user_id"]);
                            cavm.UserName = user.Name;
                            cavm.UserEmail = user.Email;

                            var doctorUser = _context.Users.SingleOrDefault(u => u.Id == item.DoctorId);
                            cavm.DoctorName = doctorUser.Name;

                            var specialist = _context.Specializations.Find(doctorUser.SpecializationId);
                            cavm.Specialist = specialist.Name;

                            var time = _context.Schedules.Find(item.ScheduleId);
                            cavm.Time = time.Time;

                            cavm.Date = item.Date;

                            cavms.Add(cavm);
                        }

                    }
                }

                return View(cavms);
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }


  
        }



        public ActionResult DeleteProfile()
        {

            int x = (int)Session["loged_user_id"];
            using (AppointDBContext _context = new AppointDBContext())
            {
                var user = _context.Users.Find(x);
               
                    IList<Appointment> appointments = _context.Appointments.Where(ap => ap.DoctorId==user.Id).ToList();
                if (user.IsADoctor==true)
                {
                    if (appointments != null)
                    {

                        _context.Appointments.RemoveRange(_context.Appointments.Where(ap => ap.DoctorId == user.Id));


                    }
                }
                else
                {
                    _context.Appointments.RemoveRange(_context.Appointments.Where(ap => ap.UId == user.Id));

                }



                var image = _context.Images.SingleOrDefault(im=>im.UserId==user.Id);
                    if (image!=null)
                    {
                        _context.Images.Remove(image);
                       // _context.SaveChanges();
                     }
             

                    _context.Users.Remove(user);
                    _context.SaveChanges();
 
            }

            Session["loged_user_id"] = null;
            return RedirectToAction("LogIn","Registration");
        }
    }
}