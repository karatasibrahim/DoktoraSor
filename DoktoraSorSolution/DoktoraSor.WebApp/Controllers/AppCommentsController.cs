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
    public class AppCommentsController : Controller
    {
         
        AppCommentManager appCommentManager = new AppCommentManager();
      
        public ActionResult Index()
        {
            return View(appCommentManager.List());
        }
 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppComment appComment = appCommentManager.Find(x=>x.Id==id.Value);
            if (appComment == null)
            {
                return HttpNotFound();
            }
            return View(appComment);
        }

     
        public ActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AppComment appComment)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                
                appCommentManager.Insert(appComment);
                
                return RedirectToAction("Index");
            }

            return View(appComment);
        }

        // GET: AppComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppComment appComment = appCommentManager.Find(x=>x.Id==id.Value);
            if (appComment == null)
            {
                return HttpNotFound();
            }
            return View(appComment);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AppComment appComment)
        {
            ModelState.Remove("CreateOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                AppComment appc = appCommentManager.Find(x=>x.Id==appComment.Id);
                appc.Text = appComment.Text;
                appc.IsApproval = appComment.IsApproval;
                appc.PatientOwner.Id = appComment.PatientOwner.Id;
                appCommentManager.Update(appc);
                return RedirectToAction("Index");
            }
            return View(appComment);
        }

        // GET: AppComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppComment appComment = appCommentManager.Find(x=>x.Id==id.Value);
            if (appComment == null)
            {
                return HttpNotFound();
            }
            return View(appComment);
        }

        // POST: AppComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppComment appComment = appCommentManager.Find(x => x.Id == id);
            appCommentManager.Delete(appComment);
          
            return RedirectToAction("Index");
        }

        
    }
}
