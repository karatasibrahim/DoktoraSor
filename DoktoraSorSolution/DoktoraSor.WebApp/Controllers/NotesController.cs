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
using DoktoraSor.WebApp.Filters;
using DoktoraSor.WebApp.Models;

namespace DoktoraSor.WebApp.Controllers
{
    [Exc]
    public class NotesController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private LikedManager likedManager = new LikedManager();


        [Auth]
        public ActionResult Index() //TODO: Bakılacak
        {


            var notes = noteManager.ListIQueryable().Include("Category").Include("DoctorOwner").Where(
                x => x.DoctorOwner.Id == CurrentSession.UserDoctor.Id).OrderByDescending(x => x.ModifiedOn);
            return View(notes.ToList());
        }
        [Auth]
        public ActionResult MyLikedNotes()
        {
            if (CurrentSession.UserDoctor != null)
            {
                var notes = likedManager.ListIQueryable().Include("LikedDoctorUser").Include("Note").Where(
               x => x.LikedDoctorUser.Id == CurrentSession.UserDoctor.Id).Select(
               x => x.Note).Include("Category").Include("DoctorOwner").OrderByDescending(
               x => x.ModifiedOn);
                return View("Index", notes.ToList());
            }
            else
            {
                var notes = likedManager.ListIQueryable().Include("LikedPatientUser").Include("Note").Where(
               x => x.LikedPatientUser.Id == CurrentSession.UserPatient.Id).Select(
               x => x.Note).Include("Category").OrderByDescending(
               x => x.ModifiedOn);
                return View("Index", notes.ToList());
            }



        }


        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [Auth]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categoryManager.List(), "Id", "Title");
            return View();
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                note.DoctorOwner = CurrentSession.UserDoctor;
                noteManager.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId); //Dropdown kontrol için 
            return View(note);
        }

        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {
                Note db_note = noteManager.Find(x => x.Id == note.Id);
                db_note.IsDraft = note.IsDraft;
                db_note.CategoryId = note.CategoryId;
                db_note.Text = note.Text;
                db_note.Title = note.Title;
                noteManager.Update(db_note);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteManager.Find(x => x.Id == id);
            noteManager.Delete(note);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetLiked(int[] ids) //TODO: Daha sonra doktor liked eklenecek
        {
            if (CurrentSession.UserPatient!=null)
            {
                int userid = CurrentSession.UserPatient.Id;
                List<int> likedNoteIds = new List<int>();
                if (ids!=null)
                {
                    likedNoteIds = likedManager.List(
                                x => x.LikedPatientUser.Id == CurrentSession.UserPatient.Id && ids.Contains(x.Note.Id)).Select(
                                x => x.Note.Id).ToList();
                }
                else
                {
                    likedNoteIds = likedManager.List(
                        x => x.LikedPatientUser.Id == userid).Select(
                        x => x.Note.Id).ToList();
                }
                return Json(new { result = likedNoteIds });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }

               
           
           

        }
        [HttpPost]

        public ActionResult SetLikeState(int noteid, bool liked)
        {
            int res = 0;

            if (CurrentSession.UserPatient==null || CurrentSession.UserDoctor==null)
            {
                return Json(new { hasError = true, errorMessage = "Beğenme işlemi için giriş yapmalısınız.", result = 0 });
            }

            Liked like = likedManager.Find(x => x.Note.Id == noteid && x.LikedPatientUser.Id == CurrentSession.UserPatient.Id);
            Note note = noteManager.Find(x => x.Id == noteid);

            if (like!= null && liked==false)
            {
              res=  likedManager.Delete(like);

            }
            else if(like==null && liked==true)
            {
                res = likedManager.Insert(new Liked()
                {
                    LikedPatientUser = CurrentSession.UserPatient,
                    Note = note
                });
            }

            if (res>0)
            {
                if (liked)
                {
                    note.LikeCount++;

                }
                else
                {
                    note.LikeCount--;
                }
               res= noteManager.Update(note);
                return Json(new { hasError = false, errorMessage = string.Empty, result = note.LikeCount });
            }

            return Json(new { hasError = true, errorMessage = "Beğenme işlemi gerçekleştirilemedi.", result = note.LikeCount });
        }

        public ActionResult GetNoteText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialNoteText", note);

        }

    }
}
