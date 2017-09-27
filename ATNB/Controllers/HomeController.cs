using ATNB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ATNB.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var books = JsonConvert.DeserializeObject<List<TblBook>>(json);

            Session["aut"] = GetAut();
            Session["cat"] = GetTop();
            Session["pub"] = GetPub();
            Session["pro"] = books;
            return View(Session["cart"]);
        }
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

        private TblBook GetPro(int id)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync(id + "").Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var pub = JsonConvert.DeserializeObject<TblBook>(json);
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

        private List<TblBook> SearchPro(string chuoi, int number)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Books/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("?name=" + chuoi + "&number=" + number).Result;
            if (chuoi == "")
            {
                resp = cl.GetAsync("").Result;
            }
            string json = resp.Content.ReadAsStringAsync().Result;
            var pub = JsonConvert.DeserializeObject<List<TblBook>>(json);
            return pub;
        }

        public ActionResult ShowPro(string name)
        {
            var lis = SearchPro(name, 4);
            Session["aut"] = GetAut();
            Session["cat"] = GetTop();
            Session["pub"] = GetPub();
            Session["pro"] = GetPro();
            return View("Show", lis.ToList());
        }

        public ActionResult ShowAuthor(int? id)
        {
            var lis = SearchPro(id + "", 1);
            Session["aut"] = GetAut();
            Session["cat"] = GetTop();
            Session["pub"] = GetPub();
            Session["pro"] = GetPro();
            return View("Show", lis.ToList());
        }

        public ActionResult ShowPub(int? id)
        {
            var lis = SearchPro(id + "", 3);
            Session["aut"] = GetAut();
            Session["cat"] = GetTop();
            Session["pub"] = GetPub();
            Session["pro"] = GetPro();
            return View("Show", lis.ToList());
        }

        public ActionResult ShowCate(int? id)
        {
            var lis = SearchPro(id + "", 2);
            Session["aut"] = GetAut();
            Session["cat"] = GetTop();
            Session["pub"] = GetPub();
            Session["pro"] = GetPro();
            return View("Show", lis.ToList());
        }
        public ActionResult Buy(int id)
        {
            float ammount = 0;
            List<TblBook> list = new List<TblBook>();
            if (Session["cart"] != null)
            {
                list = (List<TblBook>)Session["cart"];
            }

            var pro = GetPro(id);
            pro.Quantity = 1;
            bool check = true;
            foreach (var item in list)
            {
                if (item.BookId == pro.BookId)
                {
                    check = false;
                    item.Quantity++;
                }
            }

            if (check)
            {
                list.Add(pro);
            }

            foreach (var item in list)
            {
                ammount += item.Price * item.Quantity;
            }

            Session["cart"] = list;
            Session["aut"] = GetAut();
            Session["cat"] = GetTop();
            Session["pub"] = GetPub();
            Session["pro"] = GetPro();

            Session["ammount"] = ammount;
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            float ammount = 0;
            List<TblBook> list = new List<TblBook>();
            if (Session["cart"] != null)
            {
                list = (List<TblBook>)Session["cart"];
            }
            int check = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].BookId == id)
                {
                    check = i;
                }
                else
                    ammount += list[i].Quantity * list[i].Price;
            }
            list.RemoveAt(check);
            Session["cart"] = null;
            Session["cart"] = list;
            Session["aut"] = GetAut();
            Session["cat"] = GetTop();
            Session["pub"] = GetPub();
            Session["pro"] = GetPro();

            Session["ammount"] = ammount;
            return RedirectToAction("Index");
        }

        public ActionResult Pay()
        {
            return View(Session["cart"]);
        }

        public ActionResult Payment()
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Orders/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var list =(List<TblBook>) Session["cart"];
            string json = JsonConvert.SerializeObject(list, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = cl.PostAsync("", content).Result;
            if (resp.IsSuccessStatusCode)
            {
                Session["cart"] = null;
                return RedirectToAction("Index");
            }
            else
               
            return View("Pay",Session["cart"]);
        }

        [HttpPost]
        public ActionResult Update(IEnumerable<TblBook> list)
        {
            float ammount = 0;

            //foreach (var item in list)
            //{
            //    ammount += item.Quantity * item.Price;
            //}
            //Session["cart"] = null;
            //Session["cart"] = list;
            //Session["aut"] = GetAut();
            //Session["cat"] = GetTop();
            //Session["pub"] = GetPub();
            //Session["pro"] = GetPro();

            ////Session["ammount"] = ammount;
            //return RedirectToAction("Index");
            return Content(list.First().BookId+"");
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

        private User GetUser(string user, string pass)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:5383/Api/Users/");
            cl.DefaultRequestHeaders.Accept.Clear();
            cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var resp = cl.GetAsync("?user=" + user + "&pass=" + pass).Result;
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
            var resp = cl.GetAsync("?name=" + name).Result;
            string json = resp.Content.ReadAsStringAsync().Result;
            var top = JsonConvert.DeserializeObject<User>(json);
            return top;
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User users)
        {
            users = GetUser(users.UserName, users.Password);
            Session["user"] = users;
            if (users != null)
            {
                return RedirectToAction("Index", "Admin/Books", null);
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return View("Index");
        }

        public ActionResult Search(string id)
        {
            //  var list = db.tblBooks.Where(s => s.Title.Contains(id) || s.tblAuthor.AuthorName.Contains(id) || s.tblCategory.CategoryName.Contains(id) || s.tblPublisher.PublisherName.Contains(id));
            return View("Show");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}