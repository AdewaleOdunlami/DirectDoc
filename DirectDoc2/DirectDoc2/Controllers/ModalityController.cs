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
    public class ModalityController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: /Modality/
        public ActionResult Index()
        {
            var modalities = db.Modalities.Include(m => m.Tariff);
            return View(modalities.ToList());
        }

        // GET: /Modality/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modality modality = db.Modalities.Find(id);
            if (modality == null)
            {
                return HttpNotFound();
            }
            return View(modality);
        }

        // GET: /Modality/Create
        public ActionResult Create()
        {
            ViewBag.TariffID = new SelectList(db.Tariffs, "ID", "TariffType");
            return View();
        }

        // POST: /Modality/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ModalityID,TariffID,ModalityCode,Description,Price")] Modality modality)
        {
            if (ModelState.IsValid)
            {
                db.Modalities.Add(modality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TariffID = new SelectList(db.Tariffs, "ID", "TariffType", modality.TariffID);
            return View(modality);
        }

        // GET: /Modality/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modality modality = db.Modalities.Find(id);
            if (modality == null)
            {
                return HttpNotFound();
            }
            ViewBag.TariffID = new SelectList(db.Tariffs, "ID", "TariffType", modality.TariffID);
            return View(modality);
        }

        // POST: /Modality/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ModalityID,TariffID,ModalityCode,Description,Price")] Modality modality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TariffID = new SelectList(db.Tariffs, "ID", "TariffType", modality.TariffID);
            return View(modality);
        }

        // GET: /Modality/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modality modality = db.Modalities.Find(id);
            if (modality == null)
            {
                return HttpNotFound();
            }
            return View(modality);
        }

        // POST: /Modality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modality modality = db.Modalities.Find(id);
            db.Modalities.Remove(modality);
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
