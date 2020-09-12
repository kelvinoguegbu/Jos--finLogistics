using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using José_finLogistics.Models;

namespace José_finLogistics.Controllers
{
    public class AdminsController : Controller
    {
        private readonly LogisticsContext db = new LogisticsContext();

        // GET: Admins
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                return View(db.AdminTable.ToList());
            }
            return RedirectToAction("Login");
            
        }

        // GET: Admins/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.AdminTable.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return RedirectToAction("Login");
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.AdminTable.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.AdminTable.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return RedirectToAction("Login");
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminId,Name,Email,Username,Password,ConfirmPassword,Phone,Address")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.AdminTable.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return RedirectToAction("Login");
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.AdminTable.Find(id);
            db.AdminTable.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            using (LogisticsContext db = new LogisticsContext())
            {
                try
                {
                    var myadmin = db.AdminTable.Single(a => a.Username == admin.Username && a.Password == admin.Password);
                    if (myadmin != null)
                    {
                        Session["AdminId"] = myadmin.AdminId.ToString();
                        Session["AdminName"] = myadmin.Name.ToString();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
