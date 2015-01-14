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
    [HandleError(ExceptionType = typeof(System.Data.DataException))]
    public class MedicalController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: /Medical/
        public ActionResult Index()
        {
            var medicalaids = db.MedicalAids.Include(m => m.Person).Include(m => m.Tariff);
            return View(medicalaids.ToList());
        }

        // GET: /Medical/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalAid medicalaid = db.MedicalAids.Find(id);
            if (medicalaid == null)
            {
                return HttpNotFound();
            }
            return View(medicalaid);
        }

        // GET: /Medical/Create
        public ActionResult Create()
        {
            var sponsors = from s in db.Clients
                           where s.Dependant == false
                           select s;

            ViewBag.PersonID = new SelectList(sponsors, "ID", "FullName");
            ViewBag.TariffID = new SelectList(db.Tariffs, "ID", "TariffType");
            return View();
        }

        // POST: /Medical/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MedicalAidID,PersonID,NameOfAid,PolicyNumber,TariffID")] MedicalAid medicalaid)
        {
            var sponsors = from s in db.Clients
                           where s.Dependant == false
                           select s;

            if (ModelState.IsValid)
            {
                db.MedicalAids.Add(medicalaid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(sponsors, "ID", "FullName", medicalaid.PersonID);
            ViewBag.TariffID = new SelectList(db.Tariffs, "ID", "TariffType", medicalaid.TariffID);
            return View(medicalaid);
        }

        // GET: /Medical/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalAid medicalaid = db.MedicalAids.Find(id);
            if (medicalaid == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "FullName", medicalaid.PersonID);
            ViewBag.TariffID = new SelectList(db.Tariffs, "ID", "TariffType", medicalaid.TariffID);
            return View(medicalaid);
        }

        // POST: /Medical/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MedicalAidID,PersonID,NameOfAid,PolicyNumber,TariffID")] MedicalAid medicalaid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalaid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "FullName", medicalaid.PersonID);
            ViewBag.TariffID = new SelectList(db.Tariffs, "ID", "TariffType", medicalaid.TariffID);
            return View(medicalaid);
        }

        // GET: /Medical/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalAid medicalaid = db.MedicalAids.Find(id);
            if (medicalaid == null)
            {
                return HttpNotFound();
            }
            return View(medicalaid);
        }

        // POST: /Medical/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalAid medicalaid = db.MedicalAids.Find(id);
            db.MedicalAids.Remove(medicalaid);
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
