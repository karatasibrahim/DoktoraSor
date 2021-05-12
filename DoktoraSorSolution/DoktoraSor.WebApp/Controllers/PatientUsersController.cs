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
    public class PatientUsersController : Controller
    {
        PatientUserManager PatientUserManager = new PatientUserManager();

        // GET: PatientUsers
        public ActionResult Index()
        {
            return View(PatientUserManager.List());
        }

        // GET: PatientUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientUser patientUser = PatientUserManager.Find(x=>x.Id==id.Value);
            if (patientUser == null)
            {
                return HttpNotFound();
            }
            return View(patientUser);
        }

        // GET: PatientUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientUser patientUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<PatientUser> res = PatientUserManager.Insert(patientUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(patientUser);
                }
                return RedirectToAction("Index");
            }

            return View(patientUser);
        }

        // GET: PatientUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientUser patientUser = PatientUserManager.Find(x=>x.Id==id.Value);
            if (patientUser == null)
            {
                return HttpNotFound();
            }
            return View(patientUser);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientUser patientUser)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<PatientUser> res = PatientUserManager.Update(patientUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(patientUser);
                }

                return RedirectToAction("Index");
            }
            return View(patientUser);
        }

        // GET: PatientUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientUser patientUser = PatientUserManager.Find(x=>x.Id==id.Value);
            if (patientUser == null)
            {
                return HttpNotFound();
            }
            return View(patientUser);
        }

        // POST: PatientUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientUser patientUser = PatientUserManager.Find(x=>x.Id==id);
            PatientUserManager.Delete(patientUser);
           
            return RedirectToAction("Index");
        }

      
    }
}
