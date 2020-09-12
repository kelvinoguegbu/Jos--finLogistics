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
    public class PackagesController : Controller
    {
        private readonly LogisticsContext db = new LogisticsContext();

        // GET: Packages
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                var packageTable = db.PackageTable.Include(p => p.Client);
                return View(packageTable.ToList());
            }
            return RedirectToAction("Login", "Admins");
        }

        public ActionResult FindPackage(string searchString)
        {
            var packages = from s in db.PackageTable
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                packages = packages.Where(s => s.TrackingId.Contains(searchString));
                ViewBag.Checker = "Searchfound";
            }
            var packageTable = db.PackageTable.Include(p => p.Client);
                return View(packages.ToList());
        }

        // GET: Packages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.PackageTable.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.ClientId = new SelectList(db.ClientTable, "ClientId", "FirstName");
                return View();
            }
            return RedirectToAction("Login", "Admins");
            
        }

        // POST: Packages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PackageId,TrackingId,PackageStatus,ClientId")] Package package)
        {
            if (ModelState.IsValid)
            {
                package.TrackingId = Guid.NewGuid().ToString().Substring(0, 8);
                db.PackageTable.Add(package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.ClientTable, "ClientId", "FirstName", package.ClientId);
            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Package package = db.PackageTable.Find(id);
                if (package == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ClientId = new SelectList(db.ClientTable, "ClientId", "FirstName", package.ClientId);
                return View(package);
            }
            return RedirectToAction("Login", "Admins");
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PackageId,TrackingId,PackageStatus,ClientId")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.ClientTable, "ClientId", "FirstName", package.ClientId);
            return View(package);
        }

        // GET: Packages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Package package = db.PackageTable.Find(id);
                if (package == null)
                {
                    return HttpNotFound();
                }
                return View(package);
            }
            return RedirectToAction("Login", "Admins");
            
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.PackageTable.Find(id);
            db.PackageTable.Remove(package);
            db.SaveChanges();
            return RedirectToAction("Index");
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
