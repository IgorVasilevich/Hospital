using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class DoctorsWithPatientsController : Controller
    {
        private HospitalContext db = new HospitalContext();
        // GET: DoctorsWithPatients
        public ActionResult Index()
        {
            return View(db.Doctors.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(DoctorsWithPatients doctorsWithPatients)
        {
            var patient = db.Patients.Find(doctorsWithPatients.PatientName);
            var doctor = db.Doctors.Find(doctorsWithPatients.DoctorName);
            patient.Doctors.Add(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}