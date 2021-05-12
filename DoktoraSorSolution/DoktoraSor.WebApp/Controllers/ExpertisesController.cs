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
    public class ExpertisesController : Controller
    {
         ExpertisesManager expertisesManager = new ExpertisesManager();
     private   DepartmentManager departmentManager = new DepartmentManager();
         
        // GET: Expertises
        public ActionResult Index()
        { 
            return View(expertisesManager.List());
        }
        
         
        // GET: Expertises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertise expertise = expertisesManager.Find(x=>x.Id==id.Value);
            if (expertise == null)
            {
                return HttpNotFound();
            }
            return View(expertise);
        }

        // GET: Expertises/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult Create( Expertise expertise)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                expertisesManager.Insert(expertise);
        
                return RedirectToAction("Index");
            }
           // ViewBag.DepartmentId = new SelectList(departmentManager.List(), "Id", "Title", expertise.Department.Id); TODO: BAKILACAK
            return View(expertise);
        }

        // GET: Expertises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertise expertise = expertisesManager.Find(x=>x.Id==id.Value);
            if (expertise == null)
            {
                return HttpNotFound();
            }
            return View(expertise);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Expertise expertise)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                Expertise ex = expertisesManager.Find(x=>x.Id==expertise.Id);
                ex.Title = expertise.Title;
                
                
                expertisesManager.Update(ex);
         
                return RedirectToAction("Index");
            }
            return View(expertise);
        }

        // GET: Expertises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertise expertise = expertisesManager.Find(x=>x.Id==id.Value);
            if (expertise == null)
            {
                return HttpNotFound();
            }
            return View(expertise);
        }

        // POST: Expertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expertise expertise = expertisesManager.Find(x => x.Id == id);
            expertisesManager.Delete(expertise);

            return RedirectToAction("Index");
        }
       
    }
}
