using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATNB.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ATNB.Areas.Admin.Controllers
{
    public class AuthorsController : Controller
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

        private TblAuthor GetAut(int id)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Authors/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync(id+"").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<List<TblAuthor>>(json);
            return top[0];
        }

        private TblAuthor GetAut(string id)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Authors/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("?name="+id + "").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<List<TblAuthor>>(json);
            return top[0];
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
        // GET: Admin/Authors
        public ActionResult Index()
        {
            var aut = GetAut();
            ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(aut.ToList());
        }

        // GET: Admin/Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aut = GetAut((int)id);
            if (aut == null)
            {
                return HttpNotFound();
            }
            return View(aut);
        }

        // GET: Admin/Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorId,AuthorName,Description")] TblAuthor tblAuthor)
        {
            if (ModelState.IsValid)
            {
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("http://localhost:5383/Api/Authors/");
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(tblAuthor);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = cl.PostAsync("", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }

            return View(tblAuthor);
        }

        // GET: Admin/Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblAuthor = GetAut((int) id);
            if (tblAuthor == null)
            {
                return HttpNotFound();
            }
            return View(tblAuthor);
        }

        // POST: Admin/Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorId,AuthorName,Description")] TblAuthor tblAuthor)
        {
            if (ModelState.IsValid)
            {
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("http://localhost:5383/Api/Authors/");
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(tblAuthor);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = cl.PutAsync("" + tblAuthor.AuthorId, content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Edit", tblAuthor.AuthorId);
            }
            return View(tblAuthor);
        }

        // GET: Admin/Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var  tblAuthor = GetAut((int)id);
            if (tblAuthor == null)
            {
                return HttpNotFound();
            }
            return View(tblAuthor);
        }

        // POST: Admin/Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "AuthorId,AuthorName,Description")] TblAuthor tblAuthor)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Authors/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.DeleteAsync("" + tblAuthor.AuthorId).Result;
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
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
