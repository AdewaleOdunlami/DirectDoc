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
using System.Collections;

namespace DirectDoc2.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private ClinicContext db = new ClinicContext(); 
        
        // GET: /Person/
        public ActionResult Index(string sortOrder, string searchText)
        {
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "";
            //ViewBag.SponsorSortParm = String.IsNullOrEmpty(sortOrder) ? "sponsor_name_desc" : "";

            var clients = from c in db.Clients
                          select c;

            if (!String.IsNullOrEmpty(searchText))
            {
                clients = clients.Where(c => c.LastName.Contains(searchText)
                                || c.FirstName.Contains(searchText));
            }

            switch (sortOrder)
            {
                case "first_name_desc":
                    clients = clients.OrderByDescending(s => s.FirstName);
                    break;
                case "last_name_desc":
                    clients = clients.OrderByDescending(s => s.LastName);
                    break;
                //case "sponsor_name_desc":
                //    clients = clients.OrderByDescending(s => s.Sponsor);
                //    break;
                default:
                    clients = clients.OrderBy(s => s.LastName);
                    break;
            }
            //var clients = db.Clients.Include(p => p.Sponsor);
            return View(clients.ToList());
        }

        // GET: /Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Clients.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: /Person/Create
        public ActionResult Create()
        {
            var sponsors = from d in db.Clients
                           where d.Dependant == false
                           select d;

            //ViewBag.SponsorID = new SelectList(db.Clients, "ID", "FullName");
            ViewBag.SponsorID = new SelectList(sponsors,"ID","FullName");

            return View();
        }

        // POST: /Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SponsorID,Title,FirstName,Initials,LastName,DateOfBirth,Dependant")] Person person)
        {
            var sponsors = from d in db.Clients
                            where d.Dependant == false
                            select d;

            if (ModelState.IsValid)
            {
                db.Clients.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.SponsorID = new SelectList(db.Clients, "ID", "FullName", person.ID);
            ViewBag.SponsorID = new SelectList(sponsors,"ID","FullName", person.ID);
            return View(person);
        }

        // GET: /Person/Edit/5
        public ActionResult Edit(int? id)
        {
            var sponsors = from d in db.Clients
                           where d.Dependant == false && d.ID != id
                           select d;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Clients.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.SponsorID = new SelectList(sponsors, "ID", "FullName", person.SponsorID);
            return View(person);
        }

        // POST: /Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SponsorID,Title,FirstName,Initials,LastName,DateOfBirth,Dependant")] Person person)
        {
            var sponsors = from d in db.Clients
                           where d.Dependant == false && d.ID != person.ID
                           select d;

            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SponsorID = new SelectList(sponsors, "ID", "FullName", person.SponsorID);
            return View(person);
        }

        // GET: /Person/Delete/5
        //[HandleError(ExceptionType = typeof(System.Data.DataException), View = "Error")]
        [HandleError(ExceptionType = typeof(System.Data.DataException))]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Clients.Find(id);

            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // POST: /Person/Delete/5
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(System.Data.DataException))]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Person person = db.Clients.Find(id);
                db.Clients.Remove(person);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
            
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
