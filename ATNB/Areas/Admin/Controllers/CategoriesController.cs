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
    public class CategoriesController : Controller
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

        private TblCategory GetTop(int id)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Categories/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync(id+"").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<TblCategory>(json);
            return top;
        }

        private TblCategory GetTop(string name)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Categories/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("?name="+name).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<TblCategory>(json);
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
            var resp = cl.GetAsync(id + "").Result;
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
            var resp = cl.GetAsync("?name=" + id + "").Result;
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
        // GET: Admin/Categories
        public ActionResult Index()
        {
            var cat = GetTop();
            ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
            //if (Session["user"] == null)
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            return View(cat.ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aut = GetTop((int)id);
            if (aut == null)
            {
                return HttpNotFound();
            }
            return View(aut);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName,Description")] TblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("http://localhost:5383/Api/Categories/");
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(tblCategory);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = cl.PostAsync("", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }

            return View(tblCategory);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblCate = GetTop((int)id);
            if (tblCate == null)
            {
                return HttpNotFound();
            }
            return View(tblCate);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,Description")] TblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("http://localhost:5383/Api/Categories/");
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(tblCategory);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = cl.PutAsync("" + tblCategory.CategoryId, content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Edit", tblCategory.CategoryId);
            }
            return View(tblCategory);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblCategory = GetTop((int)id);
            if (tblCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblCategory);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "CategoryId,CategoryName,Description")] TblCategory tblCategory)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Categories/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.DeleteAsync("" + tblCategory.CategoryId).Result;
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
              //  db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
