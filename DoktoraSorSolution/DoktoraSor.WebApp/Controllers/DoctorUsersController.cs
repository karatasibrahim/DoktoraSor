using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoktoraSor.BusinessLayer;
using DoktoraSor.BusinessLayer.Results;
using DoktoraSor.Entities;
using DoktoraSor.WebApp.Data;
using DoktoraSor.WebApp.Filters;

namespace DoktoraSor.WebApp.Controllers
{
    [Auth]
    [AuthAdmin]
    [Exc]
    public class DoctorUsersController : Controller
    {
        private DoctorUserManager DoctorUserManager = new DoctorUserManager();

        // GET: DoctorUsers
        public ActionResult Index()
        {
            return View(DoctorUserManager.List());
        }

        // GET: DoctorUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorUser doctorUser = DoctorUserManager.Find(x=>x.Id==id.Value);
            if (doctorUser == null)
            {
                return HttpNotFound();
            }
            return View(doctorUser);
        }

        // GET: DoctorUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DoctorUser doctorUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("DoctorLikeCount");
            
            if (ModelState.IsValid)
            { 
             BusinessLayerResult<DoctorUser> res = DoctorUserManager.Insert(doctorUser);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(doctorUser);
                }
                return RedirectToAction("Index");
            }

            return View(doctorUser);
        }

        // GET: DoctorUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorUser doctorUser = DoctorUserManager.Find(x => x.Id == id.Value);
            if (doctorUser == null)
            {
                return HttpNotFound();
            }
            return View(doctorUser);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorUser doctorUser)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            ModelState.Remove("DoctorLikeCount");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<DoctorUser> res = DoctorUserManager.Update(doctorUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(doctorUser);
                }
                 
                return RedirectToAction("Index");
            }
            return View(doctorUser);
        }

        // GET: DoctorUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorUser doctorUser = DoctorUserManager.Find(x => x.Id == id.Value);
            if (doctorUser == null)
            {
                return HttpNotFound();
            }
            return View(doctorUser);
        }

         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorUser doctorUser = DoctorUserManager.Find(x => x.Id == id);
            DoctorUserManager.Delete(doctorUser);
         
            return RedirectToAction("Index");
        }

     
    }
}
