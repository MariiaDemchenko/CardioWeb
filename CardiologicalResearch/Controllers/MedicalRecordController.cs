using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardiologicalResearch.Models;

namespace CardiologicalResearch.Controllers
{
    public class MedicalRecordController : Controller
    {
        //private CardioContext db = new CardioContext();

        private IRepository _repository;

        public MedicalRecordController(IRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /MedicalRecord/

        public ActionResult Index()
        {
            var MedicalRecords = _repository.GetAllMedicalRecord2Users();
            return View(MedicalRecords.ToList());
        }

        //
        // GET: /MedicalRecord/Details/5

        public ActionResult Details(int id = 0)
        {
            MedicalRecord MedicalRecord = _repository.GetMedicalRecordById(id);
            if (MedicalRecord == null)
            {
                return HttpNotFound();
            }
            return View(MedicalRecord);
        }

        //
        // GET: /MedicalRecord/Create

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(_repository.GetAllUsers(), "UserId", "UserName");
            return View();
        }

        //
        // POST: /MedicalRecord/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicalRecord MedicalRecord)
        {
            if (ModelState.IsValid)
            {
                _repository.AddMedicalRecord(MedicalRecord);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_repository.GetAllUsers(), "UserId", "UserName", MedicalRecord.UserId);
            return View(MedicalRecord);
        }

        //
        // GET: /MedicalRecord/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MedicalRecord MedicalRecord = _repository.GetMedicalRecordById(id);
            if (MedicalRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(_repository.GetAllUsers(), "UserId", "UserName", MedicalRecord.UserId);
            return View(MedicalRecord);
        }

        //
        // POST: /MedicalRecord/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicalRecord MedicalRecord)
        {
            if (ModelState.IsValid)
            {
                _repository.EditMedicalRecord(MedicalRecord);
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(_repository.GetAllUsers(), "UserId", "UserName", MedicalRecord.UserId);
            return View(MedicalRecord);
        }

        //
        // GET: /MedicalRecord/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MedicalRecord MedicalRecord = _repository.GetMedicalRecordById(id);
            if (MedicalRecord == null)
            {
                return HttpNotFound();
            }
            return View(MedicalRecord);
        }

        //
        // POST: /MedicalRecord/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalRecord MedicalRecord = _repository.GetMedicalRecordById(id);
            _repository.DeleteMedicalRecord(MedicalRecord);
            return RedirectToAction("Index", "Measurement");
        }
    }
}