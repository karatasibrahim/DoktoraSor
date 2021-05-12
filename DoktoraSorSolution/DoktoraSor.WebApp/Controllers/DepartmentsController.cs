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
    public class DepartmentsController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();

        // GET: Departments
        public ActionResult Index()
        {
            return View(departmentManager.List());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = departmentManager.Find(x=>x.Id==id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                departmentManager.Insert(department);
           
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = departmentManager.Find(x=>x.Id==id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Department department)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                Department dp = departmentManager.Find(x=>x.Id==department.Id);
                dp.Title = department.Title;
                
                departmentManager.Update(dp);
                
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = departmentManager.Find(x=>x.Id==id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = departmentManager.Find(x=>x.Id==id);
            departmentManager.Delete(department);
       
            return RedirectToAction("Index");
        }

      
    }
}
