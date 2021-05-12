using DoktoraSor.BusinessLayer;
using DoktoraSor.BusinessLayer.Results;
using DoktoraSor.Entities;
using DoktoraSor.Entities.Messages;
using DoktoraSor.WebApp.Filters;
using DoktoraSor.WebApp.Models;
using DoktoraSor.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoktoraSor.WebApp.Controllers
{
    [Auth]
    public class OperationController : Controller
    { private DoctorUserManager doctorUserManager = new DoctorUserManager();
        private PatientUserManager patientUserManager = new PatientUserManager();
        // GET: Operation

        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ShowDoctorProfile()
        {
             
            
            BusinessLayerResult<DoctorUser> res = doctorUserManager.GetUserById(CurrentSession.UserDoctor.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata oluştu",                    
                    Items = res.Errors
                };
                return View("Error", ErrorNotifyObj);
            }
            return View(res.Result);
        }
        public ActionResult ShowPatientProfile()
        {
            
           
            BusinessLayerResult<PatientUser> res = patientUserManager.GetUserById(CurrentSession.UserPatient.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata oluştu",
                    Items = res.Errors
                };
                return View("Error", ErrorNotifyObj);
            }
            return View(res.Result);
        }
        public ActionResult EditProfileDoctor()
        {


            BusinessLayerResult<DoctorUser> res = doctorUserManager.GetUserById(CurrentSession.UserDoctor.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata oluştu",
                    Items = res.Errors
                };
                return View("Error", ErrorNotifyObj);
            }
             
            return View(res.Result);
        }
        [HttpPost]
        public ActionResult EditProfileDoctor(DoctorUser model, HttpPostedFileBase ProfileImage)
        {

            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                
                if (ProfileImage != null &&
                   (ProfileImage.ContentType == "image/jpeg" ||
                   ProfileImage.ContentType == "image/jpg" ||
                   ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/UserImages/Users/Doctors/{filename}"));
                    model.ProfileImageFilename = filename;
                }


               
                BusinessLayerResult<DoctorUser> res = doctorUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0) //update işlemi başarısızsa
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Operation/EditProfileDoctor"

                    };
                    return View("Error", errorNotifyObj);
                }

                CurrentSession.Set<DoctorUser>("login", res.Result); //Profil güncellendiği için Session güncellenir.
                return RedirectToAction("ShowDoctorProfile");
            }
            return View(model);
        }
        public ActionResult EditProfilePatient()
        {


            BusinessLayerResult<PatientUser> res = patientUserManager.GetUserById(CurrentSession.UserPatient.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata oluştu",
                    Items = res.Errors
                };
                return View("Error", ErrorNotifyObj);
            }
            return View(res.Result);
        }
        [HttpPost]
        public ActionResult EditProfilePatient(PatientUser model, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                (ProfileImage.ContentType == "image/jpeg" ||
                ProfileImage.ContentType == "image/jpg" ||
                ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/UserImages/Users/Patients/{filename}"));
                    model.ProfileImageFilename = filename;
                }


                
                BusinessLayerResult<PatientUser> res = patientUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0) //update işlemi başarısızsa
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Operation/EditProfilePatient"

                    };
                    return View("Error", errorNotifyObj);
                }

                CurrentSession.Set<PatientUser>("plogin",res.Result); //Profil güncellendiği için Session güncellenir.
                return RedirectToAction("ShowPatientProfile");
            }
            return View(model);
        }
        public ActionResult RemoveProfile() //Silme işlemine daha sonra bak
        {     
           BusinessLayerResult<PatientUser> pres = patientUserManager.RemoveUserById(CurrentSession.UserPatient.Id);
            BusinessLayerResult<DoctorUser> res = doctorUserManager.RemoveUserById(CurrentSession.UserDoctor.Id);

            if (CurrentSession.UserDoctor!=null)
            {
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel messages = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Silinemedi",
                        RedirectingUrl = "/Operation/ShowDoctorProfile"
                    };
                    return View("Error", messages);
                }
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (res.Errors.Count > 0)
                {
                    ErrorViewModel messages = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Silinemedi",
                        RedirectingUrl = "/Operation/ShowPatientProfile"
                    };
                    return View("Error", messages);
                }
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            
        }

        public ActionResult TestNotify()
        {
            ErrorViewModel model = new ErrorViewModel()
            {
                Header = "Yönlendirme",
                Title = "Ok Test",
                RedirectingTimeout = 10000,
                Items = new List<ErrorMessageObj>() { 
                    new ErrorMessageObj() { Message = "Test başarılı 1" }, 
                    new ErrorMessageObj() { Message = "Test başarılı 2" }}
        };
            return View("Error",model);

        }
    }
}