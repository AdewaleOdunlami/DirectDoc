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
    public class ConsultationController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: /Consultation/
        public ActionResult Index()
        {
            var consultations = db.Consultations.Include(c => c.Modality).Include(c => c.Person);
            return View(consultations.ToList());
        }

        // GET: /Consultation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // GET: /Consultation/Create
        public ActionResult Create()
        {
            ViewBag.ModalityID = new SelectList(db.Modalities, "ModalityID", "CodeDescription");
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "FullName");
            return View();
        }

        // POST: /Consultation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ConsultationID,PersonID,ConsultationDate,ConsultationTime,ModalityID,Quantity,ModalityDescription,UnitPrice,SubTotal")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Consultations.Add(consultation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModalityID = new SelectList(db.Modalities, "ModalityID", "CodeDescription", consultation.ModalityID);
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "FullName", consultation.PersonID);
            return View(consultation);
        }

        // GET: /Consultation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModalityID = new SelectList(db.Modalities, "ModalityID", "NappiCode", consultation.ModalityID);
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "Title", consultation.PersonID);
            return View(consultation);
        }

        // POST: /Consultation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ConsultationID,PersonID,ConsultationDate,ConsultationTime,ModalityID,Quantity,ModalityDescription,UnitPrice,SubTotal")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModalityID = new SelectList(db.Modalities, "ModalityID", "NappiCode", consultation.ModalityID);
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "Title", consultation.PersonID);
            return View(consultation);
        }

        // GET: /Consultation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: /Consultation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultation consultation = db.Consultations.Find(id);
            db.Consultations.Remove(consultation);
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
