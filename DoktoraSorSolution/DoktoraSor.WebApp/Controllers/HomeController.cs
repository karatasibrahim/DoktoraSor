using DoktoraSor.BusinessLayer;
using DoktoraSor.BusinessLayer.Results;
using DoktoraSor.Entities;
using DoktoraSor.Entities.Messages;
using DoktoraSor.Entities.ValueObjects;
using DoktoraSor.WebApp.Filters;
using DoktoraSor.WebApp.Models;
using DoktoraSor.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoktoraSor.WebApp.Controllers
{    [Exc]
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private DoctorUserManager doctorUserManager = new DoctorUserManager();
        private PatientUserManager patientUserManager = new PatientUserManager();
        public ActionResult Index()
        {
            //object o = 0; // HATA KONTROL
            //int a = 1;
            //int c = a / (int)o;
            //throw new Exception("Hata oluştu");

            return View();
        }

        public ActionResult Blog()
        {
            

            return View(noteManager.ListIQueryable().Where(x=>x.IsDraft==false).OrderByDescending(x => x.ModifiedOn).ToList());
            //return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList()); //son yazılan yazıları çekme işlemi
        }
        public ActionResult ByCategory(int? id) //int bir değer alabilir ve id değeri alsın ve bu değer boş olabilir
        {
            if (id == null) //eğer id boşsa
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // List<Note> notes= cat.Notes.Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList());

            List<Note> notes= noteManager.ListIQueryable().Where(x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(x => x.ModifiedOn).ToList();
             return View("Blog", notes);
            //kategorinin notları gelecek

             
        }
        public ActionResult MostLiked()
        {
             
            return View("Blog", noteManager.ListIQueryable().OrderByDescending(x => x.LikeCount).ToList()); //Blog.cshtml sayfasında en beğenilenleri göster
        }     

        public ActionResult Login()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {// BusinessLayerResult<PatientUser> pres = patientUserManager.LoginPatientUser(model);

            if (ModelState.IsValid)
            {
                if (model.ChechkedControl == true)
                {
                    BusinessLayerResult<DoctorUser> res = doctorUserManager.LoginDoctorUser(model);
                    if (res.Errors.Count > 0)
                    {
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(model);
                    }
                    CurrentSession.Set<DoctorUser>("login", res.Result);
                    return RedirectToAction("Index", "Operation");
                }
                else if (model.ChechkedControl == false)
                {
                    BusinessLayerResult<PatientUser> res = patientUserManager.LoginPatientUser(model);
                    if (res.Errors.Count > 0)
                    {
                        res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                        return View(model);
                    }
                    CurrentSession.Set<PatientUser>("plogin", res.Result);
                    return RedirectToAction("Index", "Operation");
                }




                }
            return View(model);
        }
       
        public ActionResult RegisterDoctor()
        {                        
            return View();
        }
        [HttpPost]
        public ActionResult RegisterDoctor(RegisterViewModel model)
        {   
            
            if (ModelState.IsValid) //model de kayıt varsa eğer
            {
                BusinessLayerResult<DoctorUser> res= doctorUserManager.RegisterDoctorUser(model);

                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt başarılı..",
                    RedirectingUrl = "Home/Login",

                };
                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden sisteme giriş yapamazsınız.");
                return View("Ok", notifyObj);
            }
            return View(model);
        }
        public ActionResult RegisterPatient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterPatient(RegisterViewModel model)
        {              
            if (ModelState.IsValid) 
            {               
                BusinessLayerResult<PatientUser> res = patientUserManager.RegisterPatientUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt başarılı..",
                    RedirectingUrl="Home/Login",
                    
                };
                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden sisteme giriş yapamazsınız.");
                return View("Ok",notifyObj);
            }
            return View(model);
        }

        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<DoctorUser> res = doctorUserManager.ActivateUser(id);
            BusinessLayerResult<PatientUser> pres = patientUserManager.ActivateUser(id);
            if (res.Errors.Count>0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                { 
                Title="Geçersiz işlem",
                RedirectingUrl="/Home/Index",
                Items=res.Errors
                };
                return View("Error", ErrorNotifyObj);
            }
            if (pres.Errors.Count > 0)
            {
                ErrorViewModel ErrorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz işlem",
                    RedirectingUrl = "/Home/Index",
                    Items = pres.Errors
                };
                return View("Error", ErrorNotifyObj);
            }

            OkViewModel OknotifyObj = new OkViewModel()
            {   
                Title="Hesap aktifleştirildi",
                RedirectingUrl="Home/Login"
            };
            OknotifyObj.Items.Add("Hesabınız aktifleştirildi. Sisteme giriş yapabilirsiniz.");
            return View("Ok",OknotifyObj);
        }
       
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

       
        public ActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult HasError()
        {
            return View();
        }
    }
}