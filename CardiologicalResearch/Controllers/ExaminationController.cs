using CardiologicalResearch.Models;
using System.Linq;
using System.Web.Mvc;

namespace CardiologicalResearch.Controllers
{
    public class ExaminationController : Controller
    {
        private readonly IRepository _repository;

        public ExaminationController(IRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /Testing/

        public ActionResult Index()
        {
            var testings = _repository.GetAllExamination2MedicalRecords();
            return View(testings.ToList());
        }

        //
        // GET: /Testing/Details/5

        public ActionResult Details(int id = 0)
        {
            var testing = _repository.GetExaminationById(id);
            if (testing == null)
            {
                return HttpNotFound();
            }
            return View(testing);
        }

        //
        // GET: /Testing/Create

        public ActionResult Create()
        {
            ViewBag.MedicalRecordId = new SelectList(_repository.GetAllMedicalRecords(), "MedicalRecordId", "MedicalRecordId");
            return View();
        }

        //
        // POST: /Testing/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Examination testing)
        {
            if (ModelState.IsValid)
            {
                _repository.AddExamination(testing);
                return RedirectToAction("Index");
            }

            ViewBag.MedicalRecordId = new SelectList(_repository.GetAllMedicalRecords(), "MedicalRecordId", "MedicalRecordId", testing.MedicalRecordId);
            return View(testing);
        }

        //
        // GET: /Testing/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var testing = _repository.GetExaminationById(id);
            if (testing == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicalRecordId = new SelectList(_repository.GetAllMedicalRecords(), "MedicalRecordId", "MedicalRecordId", testing.MedicalRecordId);
            return View(testing);
        }

        //
        // POST: /Testing/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Examination testing)
        {
            if (ModelState.IsValid)
            {
                _repository.EditExamination(testing);
                return RedirectToAction("Index");
            }
            ViewBag.MedicalRecordId = new SelectList(_repository.GetAllMedicalRecords(), "MedicalRecordId", "MedicalRecordId", testing.MedicalRecordId);
            return View(testing);
        }

        //
        // GET: /Testing/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var testing = _repository.GetExaminationById(id);
            if (testing == null)
            {
                return HttpNotFound();
            }
            return View(testing);
        }

        //
        // POST: /Testing/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var testing = _repository.GetExaminationById(id);
            _repository.DeleteExamination(testing);
            return RedirectToAction("Index", "ExaminationResult");
        }
    }
}