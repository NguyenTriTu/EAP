//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using ATNB.Models;

//namespace ATNB.Areas.Admin.Controllers
//{
//    public class CommentsController : Controller
//    {
//        private BookStoreEntities2 db = new BookStoreEntities2();

//        // GET: Admin/Comments
//        public ActionResult Index()
//        {
//            var tblComments = db.tblComments.Include(t => t.tblBook);
//            return View(tblComments.ToList());
//        }

//        // GET: Admin/Comments/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            tblComment tblComment = db.tblComments.Find(id);
//            if (tblComment == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tblComment);
//        }

//        // GET: Admin/Comments/Create
//        public ActionResult Create()
//        {
//            ViewBag.BookId = new SelectList(db.tblBooks, "BookId", "Title");
//            return View();
//        }

//        // POST: Admin/Comments/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "CommentId,BookId,Content,CreatedDate,IsActive")] tblComment tblComment)
//        {
//            if (ModelState.IsValid)
//            {
//                db.tblComments.Add(tblComment);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.BookId = new SelectList(db.tblBooks, "BookId", "Title", tblComment.BookId);
//            return View(tblComment);
//        }

//        // GET: Admin/Comments/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            tblComment tblComment = db.tblComments.Find(id);
//            if (tblComment == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.BookId = new SelectList(db.tblBooks, "BookId", "Title", tblComment.BookId);
//            return View(tblComment);
//        }

//        // POST: Admin/Comments/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "CommentId,BookId,Content,CreatedDate,IsActive")] tblComment tblComment)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(tblComment).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.BookId = new SelectList(db.tblBooks, "BookId", "Title", tblComment.BookId);
//            return View(tblComment);
//        }

//        // GET: Admin/Comments/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            tblComment tblComment = db.tblComments.Find(id);
//            if (tblComment == null)
//            {
//                return HttpNotFound();
//            }
//            return View(tblComment);
//        }

//        // POST: Admin/Comments/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            tblComment tblComment = db.tblComments.Find(id);
//            db.tblComments.Remove(tblComment);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
