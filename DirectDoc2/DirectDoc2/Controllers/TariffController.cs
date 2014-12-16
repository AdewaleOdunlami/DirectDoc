using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DirectDoc2.Models;
using DirectDoc2.DAL;

namespace DirectDoc2.Controllers
{
    [Authorize]
    public class TariffController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: /Tariff/
        public ActionResult Index()
        {
            return View(db.Tariffs.ToList());
        }

        // GET: /Tariff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tariff tariff = db.Tariffs.Find(id);
            if (tariff == null)
            {
                return HttpNotFound();
            }
            return View(tariff);
        }

        // GET: /Tariff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Tariff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,TariffType")] Tariff tariff)
        {
            if (ModelState.IsValid)
            {
                db.Tariffs.Add(tariff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tariff);
        }

        // GET: /Tariff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tariff tariff = db.Tariffs.Find(id);
            if (tariff == null)
            {
                return HttpNotFound();
            }
            return View(tariff);
        }

        // POST: /Tariff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,TariffType")] Tariff tariff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tariff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tariff);
        }

        // GET: /Tariff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tariff tariff = db.Tariffs.Find(id);
            if (tariff == null)
            {
                return HttpNotFound();
            }
            return View(tariff);
        }

        // POST: /Tariff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tariff tariff = db.Tariffs.Find(id);
            db.Tariffs.Remove(tariff);
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
