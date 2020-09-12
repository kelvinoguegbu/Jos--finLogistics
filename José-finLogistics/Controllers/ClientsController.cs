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
    public class ClientsController : Controller
    {
        private readonly LogisticsContext db = new LogisticsContext();

        // GET: Clients
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                return View(db.ClientTable.ToList());
            }
            return RedirectToAction("Login", "Admins");
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = db.ClientTable.Find(id);
                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(client);
            }
            return RedirectToAction("Login", "Admins");
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,LastName,Phone,PickupAddress,ItemDescription,ItemQuantity,FullName,ReceiverPhone,ReceiverAddress")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.ClientTable.Add(client);
                db.SaveChanges();
                ViewBag.Message = "Your delivery request has been successfully created";
                ModelState.Clear();
                return View();
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = db.ClientTable.Find(id);
                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(client);
            }
            return RedirectToAction("Login", "Admins");
            
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,LastName,Phone,PickupAddress,ItemDescription,ItemQuantity,FullName,ReceiverPhone,ReceiverAddress")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Client client = db.ClientTable.Find(id);
                if (client == null)
                {
                    return HttpNotFound();
                }
                return View(client);
            }
            return RedirectToAction("Login", "Admins");
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.ClientTable.Find(id);
            db.ClientTable.Remove(client);
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
