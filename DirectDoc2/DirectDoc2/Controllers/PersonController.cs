﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DirectDoc2.Models;
using DirectDoc2.DAL;
using DirectDoc2.ViewModels;
using System;

namespace DirectDoc2.Controllers
{
    public class PersonController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: /Person/
        public ActionResult Index(string sortOrder, string searchText, int? id, int? phoneID, int? sponsorID, int? addressID)
        {
            var viewModel = new PatientData();

            viewModel.Patients = db.Clients
                .Include(p => p.Dependants)
                                    .Include(p => p.Dependants)
                                    .Include(p => p.PhoneNumbers)
                                    .Include(p => p.PostalAddresses);
                //.Include(p => p.Consultations)
                                   // .OrderBy(p => p.LastName);
            
            if(id != null)
            {
                ViewBag.PatientID = id.Value;
                viewModel.PhoneNumbers = viewModel.Patients.Where(
                    p => p.ID == id.Value).Single().PhoneNumbers;

                viewModel.PostalAddresses = viewModel.Patients.Where(
                    p => p.ID == id.Value).Single().PostalAddresses;

                viewModel.Dependants = viewModel.Patients.Where(
                    p => p.ID == id.Value).Single().Dependants.Where(d => d.SponsorID == id.Value);
            }
            if (phoneID != null)
            {
                ViewBag.PhoneID = phoneID.Value;
                viewModel.PhoneNumbers = viewModel.Patients.Where(
                    p => p.ID == id.Value).Single().PhoneNumbers;
            }
            if (addressID != null)
            {
                ViewBag.AddressID = addressID.Value;
                viewModel.PostalAddresses = viewModel.Patients.Where(
                    p => p.ID == id.Value).Single().PostalAddresses;
            }

            //Conduct search on Index
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "";

            if (!String.IsNullOrEmpty(searchText))
            {
                viewModel.Patients = db.Clients.Where(p => p.LastName.Contains(searchText)
                                                            || p.FirstName.Contains(searchText));
            }

            switch (sortOrder)
            {
                case "first_name_desc":
                    viewModel.Patients = db.Clients.OrderByDescending(p => p.FirstName);
                    break;
                case "last_name_desc":
                    viewModel.Patients = db.Clients.OrderByDescending(s => s.LastName);
                    break;
                default:
                    viewModel.Patients = db.Clients.OrderBy(s => s.LastName);
                    break;
            }



            //return View(clients.ToList());
            return View(viewModel);
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
            var mainMember = from individual in db.Clients
                             where individual.IsDependant == false
                             select individual;

            ViewBag.SponsorID = new SelectList(mainMember, "ID", "FullName");
            return View();
        }

        // POST: /Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SponsorID,Title,FirstName,Initials,LastName,DateOfBirth,IsDependant")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var mainMember = from individual in db.Clients
                             where individual.IsDependant == false
                             select individual;

            ViewBag.SponsorID = new SelectList(mainMember, "ID", "FullName", person.SponsorID);
            return View(person);
        }

        // GET: /Person/Edit/5
        public ActionResult Edit(int? id)
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

            var mainMember = from mainmember in db.Clients
                             where mainmember.IsDependant == false && mainmember.ID != id
                             select mainmember;

            ViewBag.SponsorID = new SelectList(mainMember, "ID", "FullName", person.SponsorID);
            return View(person);
        }

        // POST: /Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SponsorID,Title,FirstName,Initials,LastName,DateOfBirth,IsDependant")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var mainMember = from mainmember in db.Clients
                             where mainmember.IsDependant == false && mainmember.ID != person.ID
                             select mainmember;

            ViewBag.SponsorID = new SelectList(mainMember, "ID", "FullName", person.SponsorID);
            return View(person);
        }

        // GET: /Person/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Clients.Find(id);
            db.Clients.Remove(person);
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
