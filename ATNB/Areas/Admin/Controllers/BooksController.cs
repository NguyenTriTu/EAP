using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATNB.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace ATNB.Areas.Admin.Controllers
{
    public class BooksController : Controller
    {
        private List<TblBook> GetPro()
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var pub = JsonConvert.DeserializeObject<List<TblBook>>(json);
            return pub;
        }

        private List<TblPublisher> GetPub()
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Publishers/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var pub = JsonConvert.DeserializeObject<List<TblPublisher>>(json);
            return pub;
        }

        private List<TblCategory> GetTop()
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Categories/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<List<TblCategory>>(json);
            return top;
        }

        private List<TblAuthor> GetAut()
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Authors/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<List<TblAuthor>>(json);
            return top;
        }

        private List<TblBook> SearchPro(string chuoi)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("?name=" + chuoi).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var pub = JsonConvert.DeserializeObject<List<TblBook>>(json);
            return pub;
        }
        // GET: Admin/Books
        public ActionResult Index()
        {
            var tblBooks = GetPro();
            ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "../Home");
            }
            return View(tblBooks.ToList());
        }

       // GET: Admin/Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblBook = SearchPro(id+"");
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook[0]);
        }

        // GET: Admin/Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
            return View();
        }

        // POST: Admin/Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,CategoryId,PublisherId,AuthorId,Summary,ImgUrl,Price,Quantity,CreatedDate,ModifieldDate,IsActive")] TblBook tblBook, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string fn = System.IO.Path.GetFileName(upload.FileName);
                    string SaveLocation = Server.MapPath("~/Content/Images/Products/") + fn;
                    upload.SaveAs(SaveLocation);
                    tblBook.ImgUrl = fn;
                }
                else
                {
                    tblBook.ImgUrl = "noimage.gif";
                }
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(tblBook);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = cl.PostAsync("", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }

            ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
            return View(tblBook);
        }

        // GET: Admin/Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("" + id).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var books = JsonConvert.DeserializeObject<TblBook>(json);

            if (books == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
            return View(books);
        }

        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,CategoryId,PublisherId,AuthorId,Summary,ImgUrl,Price,Quantity,CreatedDate,ModifieldDate,IsActive")] TblBook tblBook, HttpPostedFileBase upload1)
        {
            if (ModelState.IsValid)
            {
                if (upload1 != null && upload1.ContentLength > 0)
                {
                    string fn = System.IO.Path.GetFileName(upload1.FileName);
                    string SaveLocation = Server.MapPath("~/Content/Images/Products/") + fn;
                    upload1.SaveAs(SaveLocation);
                    tblBook.ImgUrl = fn;
                }
                
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(tblBook);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = cl.PutAsync(tblBook.BookId+"", content).Result;
                //if (resp.IsSuccessStatusCode)
                //{
                    return RedirectToAction("Index");
                //}
                //else
                //{
                    //ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
                    //ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
                    //ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
                    //return View(tblBook);
               // }
            }
            ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
            return View(tblBook);
        }

        // GET: Admin/Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("" + id).Result;
            var book = JsonConvert.DeserializeObject<TblBook>(resp.Content.ReadAsStringAsync().Result);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "BookId,Title,CategoryId,PublisherId,AuthorId,Summary,ImgUrl,Price,Quantity,CreatedDate,ModifieldDate,IsActive")] TblBook tblBook)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.DeleteAsync("" + tblBook.BookId).Result;
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
