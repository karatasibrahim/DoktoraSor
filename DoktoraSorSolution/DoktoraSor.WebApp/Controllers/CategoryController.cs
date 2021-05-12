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
using DoktoraSor.WebApp.Filters;
using DoktoraSor.WebApp.Models;

namespace DoktoraSor.WebApp.Controllers
{
    [Auth]
    [AuthAdmin]
    [Exc]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager();

      //CategoryManager gibi business katmanında manager classı oluştur. ardından GetCategories metodunu ve GetCategoryById metodunu yaz
        public ActionResult Index()
        {
            return View(categoryManager.List()); // veritabanından gelen 
           
            // return View(CacheHelper.GetCategoriesFromCache()); // Cache den gelen kategori nesnesi
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(x=>x.Id==id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

  
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                
                categoryManager.Insert(category);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
            }

            return View(category);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                Category cat = categoryManager.Find(x => x.Id == category.Id);
                cat.Title = category.Title;
                cat.Description = category.Description;
                categoryManager.Update(cat);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
            }
            return View(category);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryManager.Find(x => x.Id == id);
            categoryManager.Delete(category);

            CacheHelper.RemoveCategoriesFromCache();

            return RedirectToAction("Index");
        }

       
    }
}
