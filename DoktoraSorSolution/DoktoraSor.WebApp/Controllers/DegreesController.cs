using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoktoraSor.BusinessLayer;
using DoktoraSor.Entities;
using DoktoraSor.WebApp.Data;

namespace DoktoraSor.WebApp.Controllers
{
    public class DegreesController : Controller
    {
        DegreeManager degreeManager = new DegreeManager();

        // GET: Degrees
        public ActionResult Index()
        {
            return View(degreeManager.List());
        }

        // GET: Degrees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Degree degree = degreeManager.Find(x=>x.Id==id.Value);
            if (degree == null)
            {
                return HttpNotFound();
            }
            return View(degree);
        }

        // GET: Degrees/Create
        public ActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Degree degree)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                degreeManager.Insert(degree);
   ;
                return RedirectToAction("Index");
            }

            return View(degree);
        }

        // GET: Degrees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Degree degree = degreeManager.Find(x=>x.Id==id.Value);
            if (degree == null)
            {
                return HttpNotFound();
            }
            return View(degree);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(  Degree degree)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                Degree dg = degreeManager.Find(x=>x.Id==degree.Id);
                dg.Title = degree.Title;
                degreeManager.Update(dg);
          
                return RedirectToAction("Index");
            }
            return View(degree);
        }

        // GET: Degrees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Degree degree = degreeManager.Find(x=>x.Id==id.Value);
            if (degree == null)
            {
                return HttpNotFound();
            }
            return View(degree);
        }

        // POST: Degrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Degree degree = degreeManager.Find(x => x.Id == id);
            degreeManager.Delete(degree);
           
            return RedirectToAction("Index");
        }

    }
}
