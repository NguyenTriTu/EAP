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
    public class UsersController : Controller
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

        private List<User> GetUser()
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Users/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<List<User>>(json);
            return top;
        }

        private User GetUser(string user,string pass)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Users/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("?user="+user+"&pass="+pass).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<User>(json);
            return top;
        }

        private User GetUser(string name)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Users/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync(name).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<User>(json);
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

        // GET: Admin/Users
        public ActionResult Index()
        {
            var us = GetUser();
            ViewBag.AuthorId = new SelectList(GetAut(), "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(GetTop(), "CategoryId", "CategoryName");
            ViewBag.PublisherId = new SelectList(GetPub(), "PublisherId", "PublisherName");
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(us.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblUser = GetUser(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Password,Email,IsActive")] User tblUser)
        {
            if (ModelState.IsValid)
            {
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("http://localhost:5383/Api/Users/");
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(tblUser);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = cl.PostAsync("", content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }

            return View();
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblUser = GetUser(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Password,Email,IsActive")] User tblUser)
        {
            if (ModelState.IsValid)
            {
                HttpClient cl = new HttpClient();
                cl.BaseAddress = new Uri("http://localhost:5383/Api/Users/");
                cl.DefaultRequestHeaders.Accept.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(tblUser);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = cl.PutAsync("" + tblUser.UserName, content).Result;
                if (resp.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Edit", tblUser.UserName);
            }
            return View(tblUser);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete([Bind(Include = "UserName,Password,Email,IsActive")] User tblUser)
        {
            if (tblUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser = GetUser(tblUser.UserName);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "UserName,Password,Email,IsActive")] User tblUser)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Users/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.DeleteAsync("" + tblUser.UserName).Result;
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
