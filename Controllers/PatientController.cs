using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PatientController : Controller
    {

        private HospitalContext db = new HospitalContext();
        // GET: Patient
        public ActionResult Index(string searchString)
        {
            var patients = from p in db.Patients.Include(d=>d.Doctors)
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p => p.Name.Contains(searchString));
            }
            return View(patients.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        public ActionResult Create()
        {
           // DoctorsDropDownList();
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Status,DayOfBirth,TaxCode,Doctor")] Patient patient)
        {
            if (ModelState.IsValid)
            {
               
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //DoctorsDropDownList(patient.Doctors);
            ViewBag.DoctorsList= from d in db.Doctors
                                 orderby d.Name
                                 select d;
            return View(patient);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patient = db.Patients
                .Include(d => d.Doctors)
                   .Where(i => i.Id == id).Single();
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status,DayOfBirth,TaxCode,Doctors")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // DoctorsDropDownList(patient.Doctors);
            return View(patient);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void DoctorsDropDownList(ICollection<Doctor> selectedDoctors)
        {
            var doctorsQuery = from d in db.Doctors
                                   orderby d.Name
                                   select d;
            ViewBag.DoctorsList = new SelectList(doctorsQuery, "Name", selectedDoctors);
        }


    }
}