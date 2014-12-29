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
    public class AddressController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: /Address/
        public ActionResult Index(string sortOrder, string searchText)
        {
            var postaladdresses = db.PostalAddresses.Include(p => p.Person);
            //var postaladdresses = postaladdress.Include(p => p.Person);
            return View(postaladdresses.ToList());
        }

        // GET: /Address/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PostalAddress postaladdress = db.PostalAddresses.Find(id);
            
            if (postaladdress == null)
            {
                return HttpNotFound();
            }

            return View(postaladdress);
        }

        // GET: /Address/Create
        public ActionResult Create()
        {
            var sponsors = from c in db.Clients
                            where c.Dependant == false
                            select c;

            ViewBag.PersonID = new SelectList(sponsors, "ID", "FullName");
            return View();
        }

        // POST: /Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PostalAddressID,PersonID,BoxNumber,City,Country")] PostalAddress postaladdress)
        {
            var sponsors = from c in db.Clients
                            where c.Dependant == false
                            select c;

            if (ModelState.IsValid)
            {
                db.PostalAddresses.Add(postaladdress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(sponsors, "ID", "FullName", postaladdress.PersonID);
            return View(postaladdress);
        }

        // GET: /Address/Edit/5
        public ActionResult Edit(int? id)
        {
            var sponsors = from d in db.Clients
                           where d.Dependant == false
                           select d;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostalAddress postaladdress = db.PostalAddresses.Find(id);
            if (postaladdress == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(sponsors, "ID", "FullName", postaladdress.PersonID);
            return View(postaladdress);
        }

        // POST: /Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PostalAddressID,PersonID,BoxNumber,City,Country")] PostalAddress postaladdress)
        {
            var sponsors = from d in db.Clients
                           where d.Dependant == false
                           select d;

            if (ModelState.IsValid)
            {
                db.Entry(postaladdress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "FullName", postaladdress.PersonID);
            return View(postaladdress);
        }

        // GET: /Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostalAddress postaladdress = db.PostalAddresses.Find(id);
            if (postaladdress == null)
            {
                return HttpNotFound();
            }
            return View(postaladdress);
        }

        // POST: /Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostalAddress postaladdress = db.PostalAddresses.Find(id);
            db.PostalAddresses.Remove(postaladdress);
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
