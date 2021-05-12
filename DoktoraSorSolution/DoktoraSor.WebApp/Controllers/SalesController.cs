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
    public class SalesController : Controller
    {
        SalesManager salesManager = new SalesManager();

        // GET: Sales
        public ActionResult Index()
        {
            return View(salesManager.List());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = salesManager.Find(x=>x.Id==id.Value);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Sales sales)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                
                salesManager.Insert(sales);
              
                return RedirectToAction("Index");
            }

            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = salesManager.Find(x=>x.Id==id.Value);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sales sales)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                Sales sal = salesManager.Find(x => x.Id == sales.Id);
                sal.IsPay = sales.IsPay;
                sal.Product = sales.Product;
                sal.PayCount = sales.PayCount;
                sal.PatientUser.Id = sales.PatientUser.Id;
                sal.MessageAndQuestionCount = sales.MessageAndQuestionCount;
                sal.DoctorUser.Id = sales.DoctorUser.Id;
                salesManager.Update(sal);
               
                return RedirectToAction("Index");
            }
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = salesManager.Find(x=>x.Id==id.Value);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = salesManager.Find(x=>x.Id==id);
            salesManager.Delete(sales);
           
            return RedirectToAction("Index");
        }
 
    }
}
