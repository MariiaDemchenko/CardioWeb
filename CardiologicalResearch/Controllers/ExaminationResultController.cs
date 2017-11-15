using CardiologicalResearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CardiologicalResearch.Controllers
{
    public class ExaminationResultController : Controller
    {

        private readonly IRepository _repository;

        public ExaminationResultController(IRepository repository)
        {
            _repository = repository;
        }

        private int _examinationResult = -1;

        //
        // GET: /ExaminationResult/

        public ActionResult Index()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var examinationResults = _repository.GetCurrentUserExaminationResults(userId);
            return View(examinationResults);
        }

        //
        // GET: /ExaminationResult/Details/5

        public ActionResult Details(int id = 0)
        {
            var examinationResult = _repository.GetExaminationResultById(id);
            if (examinationResult == null)
            {
                return HttpNotFound();
            }
            return View(examinationResult);
        }

        //
        // GET: /ExaminationResult/Create

        public ActionResult Create()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var currentMedicalRecordId = _repository.GetCurrentUserMedicalRecord(userId)?.MedicalRecordId;

            if (currentMedicalRecordId != null)
            {
                var examination = new Examination()
                {
                    ExaminationDate = DateTime.Now,
                    MedicalRecordId = (int)currentMedicalRecordId
                };

                _repository.AddExamination(examination);

                var currentExaminationId = _repository.GetCurrentUserExamination(userId)?.ExaminationId;

                if (currentExaminationId != null)
                {
                    ViewBag.ExaminationId = currentExaminationId.ToString();
                }
            }
            return View();
        }

        //
        // POST: /ExaminationResult/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(List<ExaminationResult> addedExaminationResults)
        {
            var examinationId = addedExaminationResults.FirstOrDefault()?.ExaminationId;
            if (examinationId != null)
            {
                var examinationResults = _repository.GetExaminationResultsByExaminationId((int)examinationId).ToList();

                if (examinationResults.Count == 0)
                {
                    foreach (var examinationResult in addedExaminationResults)
                    {
                        _repository.AddUserExaminationResult(examinationResult);
                    }
                }
            }
            return RedirectToAction("ChooseTest");
        }

        //
        // GET: /ExaminationResult/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var requiredParameters = new List<SelectListItem>();
            var examinationResult = _repository.GetExaminationResultById(id);
            var examinationId = examinationResult?.ExaminationId;
            if (examinationId != null)
            {
                var currentMedicalRecordId = _repository.GetExaminationById((int)examinationId)?.MedicalRecordId;
                if (currentMedicalRecordId != null)
                {
                    var currentMeasurements = _repository.GetMeasurementsByMedicalRecordId((int)currentMedicalRecordId).ToList();

                    switch (examinationResult.TestId)
                    {
                        case TestType.ArmsIndex15:
                            {
                                ViewBag.TestName = Constants.ArmsIndex15Name;
                                foreach (var measurement in currentMeasurements)
                                {

                                    if (measurement.ParameterId == ParameterType.Age ||
                                        measurement.ParameterId == ParameterType.BMI ||
                                        measurement.ParameterId == ParameterType.CfPWV ||
                                        measurement.ParameterId == ParameterType.DBPra ||
                                        measurement.ParameterId == ParameterType.Diabetes ||
                                        measurement.ParameterId == ParameterType.Glucose ||
                                        measurement.ParameterId == ParameterType.Pulse ||
                                        measurement.ParameterId == ParameterType.SBPll ||
                                        measurement.ParameterId == ParameterType.SBPra ||
                                        measurement.ParameterId == ParameterType.SBPrl ||
                                        measurement.ParameterId == ParameterType.Stenocardia ||
                                        measurement.ParameterId == ParameterType.Weight
                                    )
                                    {
                                        if (measurement.Value == null)
                                        {
                                            requiredParameters.Add(new SelectListItem()
                                            {
                                                Text = measurement.ParameterId.ToString(),
                                                Value = string.Empty
                                            });
                                        }
                                        else
                                        {
                                            requiredParameters.Add(new SelectListItem()
                                            {
                                                Text = measurement.ParameterId.ToString(),
                                                Value = measurement.Value.Replace(".", ",")
                                            });
                                        }
                                    }
                                }
                                _examinationResult = ArmsIndex15TestResult(requiredParameters);
                            }
                            break;
                        case TestType.LegsIndex15:
                            {
                                ViewBag.TestName = Constants.LegsIndex15Name;
                                foreach (var measurement in currentMeasurements)
                                {

                                    if (measurement.ParameterId == ParameterType.Age ||
                                        measurement.ParameterId == ParameterType.BaPWV ||
                                        measurement.ParameterId == ParameterType.CfPWV ||
                                        measurement.ParameterId == ParameterType.Glucose ||
                                        measurement.ParameterId == ParameterType.Pulse ||
                                        measurement.ParameterId == ParameterType.SBPra ||
                                        measurement.ParameterId == ParameterType.SBPrl ||
                                        measurement.ParameterId == ParameterType.Height ||
                                        measurement.ParameterId == ParameterType.Diabetes
                                    )
                                    {
                                        if (measurement.Value == null)
                                        {
                                            requiredParameters.Add(new SelectListItem()
                                            {
                                                Text = measurement.ParameterId.ToString(),
                                                Value = string.Empty
                                            });
                                        }
                                        else
                                        {
                                            requiredParameters.Add(new SelectListItem()
                                            {
                                                Text = measurement.ParameterId.ToString(),
                                                Value = measurement.Value.Replace(".", ",")
                                            });
                                        }
                                    }
                                }
                                _examinationResult = LegsIndex15TestResult(requiredParameters);

                            }
                            break;
                        case TestType.Sclerosis:
                            {
                                ViewBag.TestName = Constants.SclerosisName;
                                foreach (var measurement in currentMeasurements)
                                {

                                    if (
                                        measurement.ParameterId == ParameterType.CfPWV10 ||
                                        measurement.ParameterId == ParameterType.Cholesterol ||
                                        measurement.ParameterId == ParameterType.Gender ||
                                        measurement.ParameterId == ParameterType.Glucose ||
                                        measurement.ParameterId == ParameterType.Pulse ||
                                        measurement.ParameterId == ParameterType.Waist ||
                                        measurement.ParameterId == ParameterType.SBPra ||
                                        measurement.ParameterId == ParameterType.DBPra ||
                                        measurement.ParameterId == ParameterType.Height ||
                                        measurement.ParameterId == ParameterType.Weight
                                    )
                                    {
                                        if (measurement.Value == null)
                                        {
                                            requiredParameters.Add(new SelectListItem()
                                            {
                                                Text = measurement.ParameterId.ToString(),
                                                Value = string.Empty
                                            });
                                        }
                                        else
                                        {
                                            requiredParameters.Add(new SelectListItem()
                                            {
                                                Text = measurement.ParameterId.ToString(),
                                                Value = measurement.Value.Replace(".", ",")
                                            });
                                        }
                                    }
                                }
                                _examinationResult = SclerosisTestResult(requiredParameters);
                            }
                            break;
                        case TestType.ABI:
                            {
                                ViewBag.TestName = Constants.ABIName;
                                foreach (var measurement in currentMeasurements)
                                {
                                    if (
                                        measurement.ParameterId == ParameterType.CfPWV ||
                                        measurement.ParameterId == ParameterType.BaPWV ||
                                        measurement.ParameterId == ParameterType.Waist ||
                                        measurement.ParameterId == ParameterType.SCORE ||
                                        measurement.ParameterId == ParameterType.SBPll ||
                                        measurement.ParameterId == ParameterType.SBPrl ||
                                        measurement.ParameterId == ParameterType.BMI
                                    )
                                    {
                                        if (measurement.Value == null)
                                        {
                                            requiredParameters.Add(new SelectListItem()
                                            {
                                                Text = measurement.ParameterId.ToString(),
                                                Value = string.Empty
                                            });
                                        }
                                        else
                                        {
                                            requiredParameters.Add(new SelectListItem()
                                            {
                                                Text = measurement.ParameterId.ToString(),
                                                Value = measurement.Value.Replace(".", ",")
                                            });
                                        }
                                    }
                                }
                                _examinationResult = ABITestResult(requiredParameters);
                            }
                            break;
                    }
                }
            }

            ViewBag.RequiredData = requiredParameters;
            if (examinationResult == null)
            {
                return HttpNotFound();
            }

            if (_examinationResult == -1)
            {
                examinationResult.Result = TestResultType.Unknown;
            }
            else if (_examinationResult == 0)
            {
                examinationResult.Result = TestResultType.Success;
            }
            else if (_examinationResult == 1)
            {
                examinationResult.Result = TestResultType.Danger;
            }
            else if (_examinationResult == 2)
            {
                examinationResult.Result = TestResultType.Error;
            }

            ViewBag.ExaminationResult = _examinationResult.ToString();

            ViewBag.ExaminationId = new SelectList(_repository.GetAllExaminations(), "ExaminationId", "ExaminationId", examinationResult.ExaminationId);
            return View(examinationResult);
        }

        //
        // POST: /ExaminationResult/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExaminationResult examinationResult)
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var examinationResults = _repository.GetCurrentUserExaminationResults(userId);

            if (ModelState.IsValid)
            {
                _repository.EditUserExaminationResult(examinationResult);
                return PartialView("ResultsTable", examinationResults.ToList());
            }

            ViewBag.ExaminationId = new SelectList(_repository.GetAllExaminations().ToList(), "ExaminationId", "ExaminationId", examinationResult.ExaminationId);
            return View(examinationResult);
        }

        //
        // GET: /ExaminationResult/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var examinationResult = _repository.GetExaminationResultById(id);
            if (examinationResult == null)
            {
                return HttpNotFound();
            }
            return View(examinationResult);
        }

        //
        // POST: /ExaminationResult/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var examinationResult = _repository.GetExaminationResultById(id);
            if (examinationResult != null)
            {
                _repository.DeleteExaminationResult(examinationResult);
            }
            return RedirectToAction("Index");
        }

        public ActionResult StaticTable()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var examinationResults = _repository.GetCurrentUserExaminationResults(userId);
            return PartialView("StaticTable", examinationResults);
        }

        public ActionResult ChooseTest()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var currentExaminationId = _repository.GetCurrentUserExamination(userId).ExaminationId;
            ViewBag.ExaminationId = currentExaminationId;

            var examinationResults = _repository.GetExaminationResultsByExaminationId(currentExaminationId).ToList();

            return View(examinationResults);
        }

        public ActionResult ResultsTable()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var currentExaminationId = _repository.GetCurrentUserExamination(userId).ExaminationId;
            var examinationResults = _repository.GetExaminationResultsByExaminationId(currentExaminationId);
            return PartialView("ResultsTable", examinationResults);
        }

        public int ArmsIndex15TestResult(List<SelectListItem> data)
        {
            var Stenocardia = false;
            var Diabetes = -1;


            Double.TryParse(data[0].Value.Replace(".", ","), out double age);
            if (age == 0) return _examinationResult = 2;

            Double.TryParse(data[1].Value.Replace(".", ","), out double weight);
            if (weight == 0) return _examinationResult = 2;

            Double.TryParse(data[2].Value.Replace(".", ","), out double SBPra);
            if (SBPra == 0) return _examinationResult = 2;

            Double.TryParse(data[3].Value.Replace(".", ","), out double DBPra);
            if (DBPra == 0) return _examinationResult = 2;

            Double.TryParse(data[4].Value.Replace(".", ","), out double SBPll);
            if (SBPll == 0) return _examinationResult = 2;

            Double.TryParse(data[5].Value.Replace(".", ","), out double SBPrl);
            if (SBPrl == 0) return _examinationResult = 2;

            Double.TryParse(data[6].Value.Replace(".", ","), out double pulse);
            if (pulse == 0) return _examinationResult = 2;

            Double.TryParse(data[7].Value.Replace(".", ","), out double cfPWV);
            if (cfPWV == 0) return _examinationResult = 2;

            Double.TryParse(data[8].Value.Replace(".", ","), out double baPWV);
            if (baPWV == 0) return _examinationResult = 2;

            Double.TryParse(data[9].Value.Replace(".", ","), out double glucose);
            if (glucose == 0) return _examinationResult = 2;


            if (data[10].Value == "нет") Stenocardia = false;
            else if (data[10].Value == "да") Stenocardia = true;
            else if (String.IsNullOrEmpty(data[10].Value)) return _examinationResult = 2;

            if (data[11].Value == "нет") Diabetes = 0;
            else if (data[11].Value == "возможный") Diabetes = 1;
            else if (data[11].Value == "да") Diabetes = 2;
            else if (String.IsNullOrEmpty(data[11].Value)) return _examinationResult = 2;

            if (weight <= 111)
            {
                if (DBPra <= 114.5)
                {
                    if (pulse <= 107)
                    {
                        if (SBPra <= 92.5)
                            _examinationResult = 1;
                        else if (SBPra > 92.5)
                        {
                            if (age <= 64.5)
                            {
                                if (SBPll <= 167.5)
                                {
                                    if (DBPra <= 105.5)
                                    {
                                        if (weight <= 59.5)
                                        {
                                            if (weight <= 58.5)
                                                _examinationResult = 0;
                                            else if (weight > 58.5)
                                            {
                                                if (cfPWV <= 6.6)
                                                    _examinationResult = 0;
                                                else if (cfPWV > 6.6)
                                                    _examinationResult = 1;
                                            }
                                        }
                                        else if (weight > 59.5)
                                        {
                                            _examinationResult = 0;
                                        }
                                    }
                                    else if (DBPra > 105.5)
                                    {
                                        if (SBPrl <= 158.5)
                                            _examinationResult = 1;
                                        else if (SBPrl > 158.5)
                                            _examinationResult = 0;
                                    }
                                }
                                else if (SBPll > 167.5)
                                {
                                    if (DBPra <= 81.5)
                                    {
                                        if (cfPWV <= 7.65)
                                            _examinationResult = 0;
                                        else if (cfPWV > 7.65)
                                            _examinationResult = 1;
                                    }
                                    else if (DBPra > 81.5)
                                    {
                                        if (SBPra <= 144)
                                        {
                                            if (pulse <= 81)
                                                _examinationResult = 0;
                                            else if (pulse > 81)
                                                _examinationResult = 1;
                                        }

                                        else if (SBPra > 144)
                                        {
                                            if (SBPra <= 161.5)
                                                _examinationResult = 0;
                                            else if (SBPra > 161.5)
                                            {
                                                if (SBPrl <= 181.5)
                                                {
                                                    if (glucose <= 5.155)
                                                        _examinationResult = 0;
                                                    else if (glucose > 5.155)
                                                    {
                                                        if (glucose <= 5.49)
                                                            _examinationResult = 1;
                                                        else if (glucose > 5.49)
                                                            _examinationResult = 0;
                                                    }
                                                }
                                                else if (SBPrl > 181.5)
                                                    _examinationResult = 0;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (age > 64.5)
                            {
                                if (SBPra <= 139)
                                {
                                    if (weight <= 84.5)
                                    {
                                        if (pulse <= 78)
                                        {
                                            if (pulse <= 59)
                                                _examinationResult = 0;
                                            else if (pulse > 59)
                                            {
                                                if (Diabetes <= 0.1)
                                                {
                                                    if (DBPra <= 85.5)
                                                        _examinationResult = 1;
                                                    else if (DBPra > 85.5)
                                                    {
                                                        if (baPWV <= 14.8)
                                                            _examinationResult = 0;
                                                        else if (baPWV > 14.8)
                                                            _examinationResult = 1;
                                                    }
                                                }
                                                else if (Diabetes > 0.1)
                                                    _examinationResult = 0;
                                            }
                                        }
                                        else if (pulse > 78)
                                            _examinationResult = 0;
                                    }
                                    else if (weight > 84.5)
                                        _examinationResult = 0;
                                }
                                else if (SBPra > 139)
                                {
                                    if (age <= 65.5)
                                    {
                                        if (Diabetes <= 0.5)
                                            _examinationResult = 0;
                                        else if (Diabetes > 0.5)
                                            _examinationResult = 1;
                                    }
                                    else if (age > 65.5)
                                        _examinationResult = 0;
                                }
                            }
                        }
                    }
                    else if (pulse > 107)
                        _examinationResult = 1;
                }
                else if (DBPra > 114.5)
                {
                    if (weight <= 92.5)
                    {
                        if (SBPrl <= 184.5)
                        {
                            if (pulse <= 81)
                                _examinationResult = 0;
                            else if (pulse > 81)
                                _examinationResult = 1;
                        }
                        else if (SBPrl > 184.5)
                            _examinationResult = 0;
                    }
                    else if (weight > 92.5)
                    {
                        if (SBPra <= 176.5)
                            _examinationResult = 0;
                        else if (SBPra > 176.5)
                            _examinationResult = 1;
                    }
                }
            }
            else if (weight > 111)
            {
                if (SBPll <= 125)
                    _examinationResult = 0;
                else if (SBPll > 125)
                {
                    if (DBPra <= 106)
                    {
                        if (SBPra <= 159.5)
                        {
                            if (Diabetes <= 0.5)
                            {
                                if (age <= 42)
                                    _examinationResult = 1;
                                else if (age > 42)
                                    _examinationResult = 0;
                            }
                            else if (Diabetes > 0.5)
                                _examinationResult = 1;
                        }
                        else if (SBPra > 159.5)
                            _examinationResult = 0;
                    }
                    else if (DBPra > 106)
                    {
                        if (!Stenocardia)
                            _examinationResult = 1;
                        else if (Stenocardia)
                            _examinationResult = 0;
                    }
                }
            }
            return _examinationResult;
        }

        public int LegsIndex15TestResult(List<SelectListItem> data)
        {
            var diabetes = -1;

            Double.TryParse(data[0].Value.Replace(".", ","), out double age);
            if (age == 0)
            {
                return _examinationResult = 2;
            }

            Double.TryParse(data[1].Value.Replace(".", ","), out double height);
            if (height == 0)
            {
                return _examinationResult = 2;
            }

            Double.TryParse(data[2].Value.Replace(".", ","), out double SBPra);
            if (SBPra == 0)
            {
                return _examinationResult = 2;
            }

            Double.TryParse(data[3].Value.Replace(".", ","), out double SBPrl);
            if (SBPrl == 0)
            {
                return _examinationResult = 2;
            }

            Double.TryParse(data[4].Value.Replace(".", ","), out double pulse);
            if (pulse == 0)
            {
                return _examinationResult = 2;
            }

            Double.TryParse(data[5].Value.Replace(".", ","), out double cfPWV);
            if (cfPWV == 0)
            {
                return _examinationResult = 2;
            }

            Double.TryParse(data[6].Value.Replace(".", ","), out double baPWV);
            if (baPWV == 0)
            {
                return _examinationResult = 2;
            }

            Double.TryParse(data[7].Value.Replace(".", ","), out double glucose);
            if (glucose == 0)
            {
                return _examinationResult = 2;
            }

            if (data[8].Value == "да")
            {
                diabetes = 2;
            }
            else if (data[8].Value == "нет")
            {
                diabetes = 0;
            }
            else if (data[8].Value == "возможный")
            {
                diabetes = 1;
            }
            else if (String.IsNullOrEmpty(data[8].Value))
            {
                return _examinationResult = 2;
            }

            if (baPWV <= 16.25)
            {
                if (glucose <= 13.775)
                {
                    if (SBPrl <= 107.5)
                        _examinationResult = 1;
                    else if (SBPrl > 107.5)
                    {
                        if (pulse <= 137.5)
                        {
                            if (height <= 174.5)
                            {
                                if (glucose <= 7.37)
                                {
                                    if (SBPrl <= 192)
                                    {
                                        if (pulse <= 81.5)
                                        {
                                            if (height <= 157.5)
                                            {
                                                if (cfPWV <= 7.55)
                                                {
                                                    if (SBPrl <= 156)
                                                        _examinationResult = 0;
                                                    else if (SBPrl > 156)
                                                        _examinationResult = 1;
                                                }
                                                else if (cfPWV > 7.55)
                                                    _examinationResult = 0;
                                            }
                                            else if (height > 157.5)
                                            {
                                                if (glucose <= 5.365)
                                                    _examinationResult = 0;
                                                else if (glucose > 5.365)
                                                {
                                                    if (glucose <= 5.39)
                                                    {
                                                        if (SBPra <= 134)
                                                        {
                                                            if (SBPra <= 132.5)
                                                                _examinationResult = 0;
                                                            else if (SBPra > 132.5)
                                                                _examinationResult = 1;
                                                        }
                                                        else if (SBPra > 134)
                                                            _examinationResult = 0;
                                                    }
                                                    else if (glucose > 5.39)
                                                        _examinationResult = 0;
                                                }
                                            }
                                        }
                                        else if (pulse > 81.5)
                                        {
                                            if (pulse <= 82.5)
                                            {
                                                if (SBPrl <= 146.5)
                                                    _examinationResult = 1;
                                                else if (SBPrl > 146.5)
                                                    _examinationResult = 0;
                                            }
                                            else if (pulse > 82.5)
                                                _examinationResult = 0;
                                        }
                                    }
                                    else if (SBPrl > 192)
                                    {
                                        if (age <= 52)
                                        {
                                            if (pulse <= 68.5)
                                                _examinationResult = 0;
                                            else if (pulse > 68.5)
                                                _examinationResult = 1;
                                        }
                                        else if (age > 52)
                                            _examinationResult = 0;
                                    }
                                }
                                else if (glucose > 7.37)
                                {
                                    if (pulse <= 59.5)
                                        _examinationResult = 1;
                                    else if (pulse > 59.5)
                                        _examinationResult = 0;
                                }
                            }
                            else if (height > 174.5)
                            {
                                if (glucose <= 4.54)
                                {
                                    if (pulse <= 65.5)
                                    {
                                        if (glucose <= 4.235)
                                            _examinationResult = 0;
                                        else if (glucose > 4.235)
                                            _examinationResult = 1;
                                    }
                                    else if (pulse > 65.5)
                                        _examinationResult = 0;
                                }
                                else if (glucose > 4.54)
                                {
                                    if (age <= 18.5)
                                        _examinationResult = 0;
                                    else if (age > 18.5)
                                    {
                                        if (cfPWV <= 10.75)
                                            _examinationResult = 0;
                                        else if (cfPWV > 10.75)
                                        {
                                            if (diabetes <= 0.1)
                                                _examinationResult = 1;
                                            else if (diabetes > 0.1)
                                                _examinationResult = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (glucose > 13.775)
                {
                    if (glucose <= 15.6)
                        _examinationResult = 1;
                    else if (glucose > 15.6)
                        _examinationResult = 0;
                }
            }
            else if (baPWV > 16.25)
            {
                if (SBPra <= 146.5)
                    _examinationResult = 0;
                else if (SBPra > 146.5)
                {
                    if (SBPrl <= 180.5)
                    {
                        if (age <= 75.5)
                        {
                            if (glucose <= 4.725)
                                _examinationResult = 0;
                            else if (glucose > 4.725)
                            {
                                if (pulse <= 101.5)
                                {
                                    if (baPWV <= 17.75)
                                    {
                                        if (cfPWV <= 11.85)
                                            _examinationResult = 1;
                                        else if (cfPWV > 11.85)
                                            _examinationResult = 0;
                                    }
                                    else if (baPWV > 17.75)
                                    {
                                        if (cfPWV <= 13.9)
                                            _examinationResult = 1;
                                        else if (cfPWV > 13.9)
                                        {
                                            if (glucose <= 6.695)
                                                _examinationResult = 1;
                                            else if (glucose > 6.695)
                                                _examinationResult = 0;
                                        }
                                    }
                                }
                                else if (pulse > 101.5)
                                    _examinationResult = 0;
                            }
                        }
                        else if (age > 75.5)
                            _examinationResult = 0;
                    }
                    else if (SBPrl > 180.5)
                    {
                        if (glucose <= 4.7)
                        {
                            if (height <= 161)
                            {
                                if (glucose <= 4.16)
                                    _examinationResult = 1;
                                else if (glucose > 4.16)
                                    _examinationResult = 0;
                            }
                            else if (height > 161)
                                _examinationResult = 1;
                        }
                        else if (glucose > 4.7)
                            _examinationResult = 0;
                    }
                }
            }
            return _examinationResult;
        }

        public int SclerosisTestResult(List<SelectListItem> data)
        {
            var cfPWV10 = false;

            var gender = data[0].Value;

            if (gender != "мужской" && gender != "женский") return _examinationResult = 2;

            Double.TryParse(data[1].Value.Replace(".", ","), out double weight);
            if (weight == 0) return _examinationResult = 2;

            Double.TryParse(data[2].Value.Replace(".", ","), out double height);
            if (height == 0) return _examinationResult = 2;

            Double.TryParse(data[3].Value.Replace(".", ","), out double waist);
            if (waist == 0) return _examinationResult = 2;

            Double.TryParse(data[4].Value.Replace(".", ","), out double SBPra);
            if (SBPra == 0) return _examinationResult = 2;

            Double.TryParse(data[5].Value.Replace(".", ","), out double DBPra);
            if (DBPra == 0) return _examinationResult = 2;

            Double.TryParse(data[6].Value.Replace(".", ","), out double pulse);
            if (pulse == 0) return _examinationResult = 2;

            Double.TryParse(data[7].Value.Replace(".", ","), out double glucose);
            if (glucose == 0) return _examinationResult = 2;

            Double.TryParse(data[8].Value.Replace(".", ","), out double cholesterol);
            if (cholesterol == 0) return _examinationResult = 2;

            if (data[9].Value == "да") cfPWV10 = true;
            else if (data[9].Value == "нет") cfPWV10 = false;
            else if (String.IsNullOrEmpty(data[9].Value)) return _examinationResult = 2;

            if (SBPra <= 110.5)
            {
                if (glucose <= 5)
                    _examinationResult = 0;
                else if (glucose > 5)
                {
                    if (pulse <= 69.5)
                        _examinationResult = 1;
                    else if (pulse > 69.5)
                        _examinationResult = 0;
                }
            }
            else if (SBPra > 110.5)
            {
                if (cholesterol <= 5.17)
                {
                    if (cholesterol <= 5.14)
                    {
                        if (!cfPWV10)
                        {
                            if (weight <= 51.5)
                            {
                                if (gender == "мужской")
                                    _examinationResult = 1;
                                else if (gender == "женский")
                                    _examinationResult = 0;
                            }
                            else if (weight > 51.5)
                                _examinationResult = 0;
                        }
                        else if (cfPWV10)
                        {
                            if (SBPra <= 138.5)
                            {
                                if (pulse <= 72.5)
                                    _examinationResult = 0;
                                else if (pulse > 72.5)
                                {
                                    if (height <= 169.5)
                                        _examinationResult = 1;
                                    else if (height > 169.5)
                                        _examinationResult = 0;
                                }
                            }
                            else if (SBPra > 138.5)
                            {
                                if (waist <= 78.5)
                                    _examinationResult = 1;
                                else if (waist > 78.5)
                                    _examinationResult = 0;
                            }
                        }
                    }
                    else if (cholesterol > 5.14)
                    {
                        if (waist <= 95)
                            _examinationResult = 0;
                        else if (waist > 95)
                            _examinationResult = 1;
                    }
                }
                else if (cholesterol > 5.17)
                {
                    if (DBPra <= 74.5)
                    {
                        if (height <= 153.5)
                            _examinationResult = 1;
                        else if (height > 153.5)
                            _examinationResult = 0;
                    }
                    else if (DBPra > 74.5)
                        _examinationResult = 0;
                }
            }
            return _examinationResult;
        }

        public int ABITestResult(List<SelectListItem> data)
        {
            int result = -1;

            Double.TryParse(data[0].Value.Replace(".", ","), out double waist);
            if (waist == 0) return _examinationResult = 2;

            Double.TryParse(data[1].Value.Replace(".", ","), out double SBPll);
            if (SBPll == 0) return _examinationResult = 2;

            Double.TryParse(data[2].Value.Replace(".", ","), out double SBPrl);
            if (SBPrl == 0) return _examinationResult = 2;

            Double.TryParse(data[3].Value.Replace(".", ","), out double cfPWV);
            if (cfPWV == 0) return _examinationResult = 2;

            Double.TryParse(data[4].Value.Replace(".", ","), out double baPWV);
            if (baPWV == 0) return _examinationResult = 2;

            if (String.IsNullOrEmpty(data[6].Value)) return 2;
            Int32.TryParse(data[6].Value, out int SCORE);

            Double.TryParse(data[5].Value.Replace(".", ","), out double BMI);
            if (BMI == 0) return _examinationResult = 2;

            if (SCORE <= 1) SCORE = 0;
            else if (SCORE > 1 && SCORE <= 5) SCORE = 1;
            else if (SCORE > 5 && SCORE <= 10) SCORE = 2;
            else if (SCORE > 10) SCORE = 3;

            if (SBPll <= 124.5)
            {
                if (cfPWV <= 7.6)
                    result = 0;
                else if (cfPWV > 7.6)
                {
                    if (BMI <= 26.233)
                        result = 0;
                    else if (BMI > 26.233)
                        result = 1;
                }
            }
            else if (SBPll > 124.5)
            {
                if (baPWV <= 18.205)
                {
                    if (SBPrl <= 199.5)
                    {
                        if (SBPrl <= 111.5)
                            result = 1;
                        else if (SBPrl > 111.5)
                        {
                            if (BMI <= 25.769)
                            {
                                if (BMI <= 25.704)
                                    result = 0;
                                else if (BMI > 25.704)
                                {
                                    if (SCORE <= 2)
                                        result = 1;
                                    else if (SCORE > 2)
                                        result = 0;
                                }
                            }
                            else if (BMI > 25.769)
                                result = 0;
                        }
                    }
                    else if (SBPrl > 199.5)
                    {
                        if (cfPWV <= 8.85)
                        {
                            if (cfPWV <= 8.7)
                                result = 0;
                            else if (cfPWV > 8.7)
                                result = 1;
                        }
                        else if (cfPWV > 8.85)
                            result = 0;
                    }
                }
                else if (baPWV > 18.205)
                {
                    if (SBPll <= 170.5)
                    {
                        if (waist <= 91)
                            result = 0;
                        else if (waist > 91)
                        {
                            if (cfPWV <= 14.6)
                                result = 1;
                            else if (cfPWV > 14.6)
                                result = 0;
                        }
                    }
                    else if (SBPll > 170.5)
                        result = 0;
                }
            }
            return result;
        }
    }
}