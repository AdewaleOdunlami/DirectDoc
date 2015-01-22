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
    public class InvoiceController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: /Invoice/
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.InvoiceTo).Include(i => i.Patient);
            return View(invoices.ToList());
        }

        // GET: /Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: /Invoice/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceToID = new SelectList(db.Clients, "ID", "Title");
            ViewBag.PatientID = new SelectList(db.Clients, "ID", "Title");
            return View();
        }

        // POST: /Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="InvoiceID,InvoiceNumber,InvoiceDate,PatientID,PatientBirthDate,InvoiceToID,InsuranceName,PolicyNumber,Address,Total")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceToID = new SelectList(db.Clients, "ID", "Title", invoice.InvoiceToID);
            ViewBag.PatientID = new SelectList(db.Clients, "ID", "Title", invoice.PatientID);
            return View(invoice);
        }

        // GET: /Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceToID = new SelectList(db.Clients, "ID", "Title", invoice.InvoiceToID);
            ViewBag.PatientID = new SelectList(db.Clients, "ID", "Title", invoice.PatientID);
            return View(invoice);
        }

        // POST: /Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="InvoiceID,InvoiceNumber,InvoiceDate,PatientID,PatientBirthDate,InvoiceToID,InsuranceName,PolicyNumber,Address,Total")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceToID = new SelectList(db.Clients, "ID", "Title", invoice.InvoiceToID);
            ViewBag.PatientID = new SelectList(db.Clients, "ID", "Title", invoice.PatientID);
            return View(invoice);
        }

        // GET: /Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: /Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
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
