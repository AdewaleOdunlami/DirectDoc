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
    //[HandleError(ExceptionType = typeof(System.Data.DataException))]
    public class PhoneController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: /Phone/
        public ActionResult Index()
        {
            var phones = db.Phones.Include(p => p.Person);
            return View(phones.ToList());
        }

        // GET: /Phone/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: /Phone/Create
        public ActionResult Create()
        {
            var mainMember = from individual in db.Clients
                          where individual.IsDependant == false
                          select individual;

            ViewBag.PersonID = new SelectList(mainMember, "ID", "FullName");
            return View();
        }

        // POST: /Phone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PhoneID,PersonID,PhoneType,AreaCode,Number")] Phone phone)
        {
            var mainMember = from individual in db.Clients
                             where individual.IsDependant == false
                             select individual;

            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(mainMember, "ID", "FullName", phone.PersonID);
            return View(phone);
        }

        // GET: /Phone/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "FullName", phone.PersonID);
            return View(phone);
        }

        // POST: /Phone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PhoneID,PersonID,PhoneType,AreaCode,Number")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.Clients, "ID", "FullName", phone.PersonID);
            return View(phone);
        }

        // GET: /Phone/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: /Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phone phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
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
