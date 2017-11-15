using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using CardiologicalResearch.Models;
using WebMatrix.WebData;

namespace CardiologicalResearch.Controllers
{
    public class MeasurementController : Controller
    {
        private IRepository _repository;

        public MeasurementController(IRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /Data/
        public ActionResult Index()
        {

            var userId = WebSecurity.GetUserId(User.Identity.Name);

            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }

            var measurements = _repository.GetCurrentUserMeasurements(userId);
            return View(measurements);
        }

        //
        // GET: /Data/Details/5

        public ActionResult Details(int id = 0)
        {
            var measurement = _repository.GetMeasurementById(id);

            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        //
        // GET: /Data/Create

        public ActionResult Create()
        {
            var update = true;
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var userMedicalRecords = _repository.GetAllUserMedicalRecords(userId);
            if (userMedicalRecords.Count == 0)
            {
                update = false;
            }

            if (update)
            {
                ViewBag.lastData = _repository.GetCurrentUserMeasurements(userId);
            }
            else
            {
                ViewBag.Datas = null;
            }

            _repository.AddUserMedicalRecord(userId);
            ViewBag.MedicalRecordId = _repository.GetCurrentUserMedicalRecord(userId)?.MedicalRecordId.ToString();

            return View();
        }

        private double Format(string sourceValue)
        {
            double targetValue;
            double.TryParse(sourceValue.Replace(Constants.Point, Constants.Comma), out targetValue);
            return targetValue;
        }

        // POST: /Data/Create

        [HttpPost]
        public ActionResult Create(List<Measurement> addedMeasurements)
        {
            var weight = 0.0;
            var height = 0.0;
            var cfPWV = 0.0;
            var cfPWV10 = string.Empty;
            var SBPra = 0.0;
            var gender = GenderValueType.Male;
            var smoker = YesNoValueType.Unknown;
            var age = 0.0;
            var cholesterol = 0.0;

            var medicalRecordId = addedMeasurements.FirstOrDefault()?.MedicalRecordId;

            if (medicalRecordId != null)
            {
                var measurements = _repository.GetMeasurementsByMedicalRecordId((int)medicalRecordId);
                if (measurements.Count != 0)
                {
                    return RedirectToAction("Index");
                }
                foreach (var addedMeasurement in addedMeasurements)
                {
                    if (addedMeasurement.ParameterId == ParameterType.Weight &&
                        !String.IsNullOrEmpty(addedMeasurement.Value))
                    {
                        weight = Format(addedMeasurement.Value);
                    }
                    else if (addedMeasurement.ParameterId == ParameterType.Age &&
                             !String.IsNullOrEmpty(addedMeasurement.Value))
                    {
                        age = Format(addedMeasurement.Value);
                    }
                    else if (addedMeasurement.ParameterId == ParameterType.SBPra &&
                             !String.IsNullOrEmpty(addedMeasurement.Value))
                    {
                        SBPra = Format(addedMeasurement.Value);
                    }
                    else if (addedMeasurement.ParameterId == ParameterType.Height &&
                             !String.IsNullOrEmpty(addedMeasurement.Value))
                    {
                        height = Format(addedMeasurement.Value);
                    }
                    else if (addedMeasurement.ParameterId == ParameterType.Cholesterol &&
                             !String.IsNullOrEmpty(addedMeasurement.Value))
                    {
                        cholesterol = Format(addedMeasurement.Value);
                    }
                    else if (addedMeasurement.ParameterId == ParameterType.CfPWV &&
                             !String.IsNullOrEmpty(addedMeasurement.Value))
                    {
                        cfPWV = Format(addedMeasurement.Value);
                    }
                    else if (addedMeasurement.ParameterId == ParameterType.Age &&
                             !String.IsNullOrEmpty(addedMeasurement.Value))
                    {
                        if (addedMeasurement.Value == Constants.Female)
                        {
                            gender = GenderValueType.Female;
                        }
                        else if (addedMeasurement.Value == Constants.Male)
                        {
                            gender = GenderValueType.Male;
                        }
                    }
                    else if (addedMeasurement.ParameterId == ParameterType.Smoker &&
                             !String.IsNullOrEmpty(addedMeasurement.Value))
                    {
                        if (addedMeasurement.Value == Constants.Yes)
                        {
                            smoker = YesNoValueType.Yes;
                        }
                        else if (addedMeasurement.Value == Constants.No)
                        {
                            smoker = YesNoValueType.No;
                        }
                    }

                    _repository.AddMeasurement(addedMeasurement);
                }

                var bmiResult = CountBMI(weight, height);
                var bmiMeasurement = new Measurement
                {
                    ParameterId = ParameterType.BMI,
                    MedicalRecordId = (int)medicalRecordId,
                    Value = bmiResult > 0 ? bmiResult.ToString("F2") : String.Empty
                };

                _repository.AddMeasurement(bmiMeasurement);

                var cfPWV10Measurement = new Measurement
                {
                    //ParameterId = Constants.ParameterType.cfPWV10,
                    ParameterId = ParameterType.CfPWV10,
                    MedicalRecordId = (int)medicalRecordId
                };

                if (cfPWV > 10)
                {
                    cfPWV10 = Constants.Yes;
                }
                else if (cfPWV > 0 && cfPWV <= 10)
                {
                    cfPWV10 = Constants.No;
                }

                cfPWV10Measurement.Value = cfPWV10;

                _repository.AddMeasurement(cfPWV10Measurement);

                double scoreResult = CountSCORE(gender, smoker, age, SBPra, cholesterol);

                var scoreMeasurement = new Measurement
                {
                    ParameterId = ParameterType.SCORE,
                    //ParameterId = Constants.ParameterType.SCORE,
                    MedicalRecordId = (int)medicalRecordId,
                    Value = scoreResult >= 0 ? scoreResult.ToString() : string.Empty
                };

                _repository.AddMeasurement(scoreMeasurement);
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Data/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var measurement = _repository.GetMeasurementById(id);
            ViewBag.ParameterId = measurement?.ParameterId;
            ViewBag.MedicalRecordId = new SelectList(_repository.GetAllMeasurements(), "MedicalRecordId", "MedicalRecordId", measurement?.MedicalRecordId);
            return PartialView(measurement);
        }

        //
        // POST: /Data/Edit/5

        [HttpPost]
        public ActionResult Edit(Measurement measurement)
        {
            var weight = 0.0;
            var height = 0.0;
            var cfPWV = 0.0;
            var cfPWV10 = string.Empty;

            ViewBag.ParameterId = measurement.ParameterId;
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var currentMedicalRecordId = _repository.GetCurrentUserMedicalRecord(userId)?.MedicalRecordId;
            var currentUserMeasurements = _repository.GetCurrentUserMeasurements(userId);

            var BMIid = 0;
            var cfPWV10id = 0;

            if (ModelState.IsValid)
            {
                _repository.EditMeasurement(measurement);

                foreach (var item in currentUserMeasurements)
                {
                    if (item.ParameterId == ParameterType.Weight)
                    {
                        Double.TryParse(item.Value, out weight);
                    }
                    else if (item.ParameterId == ParameterType.Height)
                    {
                        Double.TryParse(item.Value, out height);
                    }
                    else if (item.ParameterId == ParameterType.CfPWV)
                    {
                        Double.TryParse(item.Value, out cfPWV);
                    }
                    else if (item.ParameterId == ParameterType.BMI)
                    {
                        BMIid = item.MeasurementId;
                    }
                    else if (item.ParameterId == ParameterType.CfPWV10)
                    {
                        cfPWV10id = item.MeasurementId;
                    }
                }

                if (currentMedicalRecordId != null)
                {
                    var bmiResult = CountBMI(weight, height);

                    var bmiMeasurement = new Measurement
                    {
                        MeasurementId = BMIid,
                        ParameterId = ParameterType.BMI,
                        MedicalRecordId = (int)currentMedicalRecordId,
                        Value = bmiResult > 0 ? bmiResult.ToString() : string.Empty
                    };

                    _repository.EditMeasurement(bmiMeasurement);

                    var cfPWV10Measurement = new Measurement
                    {
                        MeasurementId = cfPWV10id,
                        ParameterId = ParameterType.CfPWV10,
                        MedicalRecordId = (int)currentMedicalRecordId
                    };

                    if (cfPWV > 10)
                    {
                        cfPWV10 = Constants.Yes;
                    }
                    else if (cfPWV > 0 && cfPWV <= 10)
                    {
                        cfPWV10 = Constants.No;
                    }

                    cfPWV10Measurement.Value = cfPWV10;

                    _repository.EditMeasurement(cfPWV10Measurement);

                    return PartialView("DataTable", currentUserMeasurements.ToList());
                }
                ViewBag.MedicalRecordId = new SelectList(_repository.GetAllUserMedicalRecords(userId), "MedicalRecordId", "MedicalRecordId", measurement.MedicalRecordId);
            }

            return PartialView(measurement);
        }

        //
        // GET: /Data/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var measurement = _repository.GetMeasurementById(id);
            if (measurement == null)
            {
                return HttpNotFound();
            }
            return View(measurement);
        }

        //
        // POST: /Data/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var measurement = _repository.GetMeasurementById(id);
            if (measurement != null)
            {
                _repository.DeleteMeasurement(measurement);
            }
            return RedirectToAction("Index");
        }

        public ActionResult StaticTable()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var measurements = _repository.GetCurrentUserMeasurements(userId);
            return PartialView("StaticTable", measurements.ToList());
        }

        public ActionResult UpdateData()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var lastMeasurements = _repository.GetAllMeasurements();

            _repository.AddUserMedicalRecord(userId);

            var lastMedicalRecordId = _repository.GetLastMedicalRecord()?.MedicalRecordId;

            if (lastMedicalRecordId != null)
            {
                foreach (var measurement in lastMeasurements)
                {
                    measurement.MedicalRecordId = (int)lastMedicalRecordId;
                    _repository.AddMeasurement(measurement);
                }
            }

            var currentUserMedicalRecordId = _repository.GetCurrentUserMedicalRecord(userId)?.MedicalRecordId;
            ViewBag.MedicalRecordId = currentUserMedicalRecordId;
            var measurements = _repository.GetCurrentUserMeasurements(userId);

            return View(measurements);
        }

        public double CountBMI(double weight, double height)
        {
            if (height == 0 || weight == 0)
            {
                return 0;
            }
            var result = weight / Math.Pow(height / 100, 2);
            return result;
        }

        //TODO: алгоритм расчета баллов риска смертности по шкале SCORE (сделано для женского пола, до 60 лет)
        public int CountSCORE(GenderValueType gender, YesNoValueType smoker, double age, double SBPra, double cholesterol)
        {

            if (gender == GenderValueType.Female)
            {
                if (age < 40)
                {
                    return 0;
                }
                if (age >= 40 && age < 45)
                {
                    if (smoker == YesNoValueType.Yes)
                    {
                        return 1;
                    }
                    return 0;
                }
                else if (age >= 45 && age < 55)
                {
                    if (smoker == YesNoValueType.No && (cholesterol < 6 && SBPra < 140 || cholesterol < 5 && SBPra > 140 && SBPra < 160))
                    {
                        return 0;
                    }
                    else if ((smoker == YesNoValueType.No && cholesterol >= 7 && SBPra > 180) ||
                    (smoker == YesNoValueType.Yes && cholesterol < 6 && SBPra >= 180) ||
                    (smoker == YesNoValueType.Yes && cholesterol >= 5 && cholesterol < 8 && SBPra >= 160 && SBPra < 180) ||
                    (smoker == YesNoValueType.Yes && cholesterol >= 8 && SBPra >= 140 && SBPra < 160))
                    {
                        return 2;
                    }
                    else if ((smoker == YesNoValueType.Yes && cholesterol >= 6 && cholesterol < 8 &&
                              SBPra >= 180) ||
                             smoker == YesNoValueType.Yes && cholesterol >= 8 && SBPra >= 160 && SBPra < 180)
                    {
                        return 3;
                    }
                    else if (smoker == YesNoValueType.Yes && cholesterol >= 8 && SBPra >= 180)
                    {
                        return 4;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else if (age >= 55 && age < 60)
                {
                    if (smoker == YesNoValueType.No && (cholesterol < 6 && SBPra >= 180 ||
                                                        cholesterol >= 5 && cholesterol < 8 && SBPra >= 160 &&
                                                        SBPra < 180 ||
                                                        cholesterol >= 8 && SBPra >= 140 && SBPra < 160) ||
                        smoker == YesNoValueType.Yes &&
                        ((cholesterol < 7 && SBPra >= 140 && SBPra < 160) ||
                         (cholesterol >= 6 && SBPra < 140)
                        ))
                    {
                        return 2;
                    }
                    else if (smoker == YesNoValueType.No &&
                             (cholesterol >= 6 && cholesterol < 8 && SBPra >= 180 ||
                              cholesterol >= 8 && SBPra >= 160 && SBPra < 180) ||
                              smoker == YesNoValueType.Yes &&
                              (cholesterol < 6 && SBPra >= 160 && SBPra < 180 ||
                              cholesterol >= 7 && SBPra >= 140 && SBPra < 160))
                    {
                        return 3;
                    }
                    else if (smoker == YesNoValueType.No && cholesterol >= 7 && SBPra >= 180 ||
                             smoker == YesNoValueType.Yes &&
                             (cholesterol < 5 && SBPra >= 180 ||
                              cholesterol >= 6 && cholesterol < 8 && SBPra >= 160 && SBPra < 180))
                    {
                        return 4;
                    }
                    else if (smoker == YesNoValueType.Yes &&
                             (cholesterol >= 5 && cholesterol < 7 && SBPra >= 180 ||
                              cholesterol >= 7 && SBPra >= 160 && SBPra < 180))
                    {
                        return 5;
                    }
                    else if (smoker == YesNoValueType.Yes && cholesterol >= 7 && cholesterol < 8 && SBPra >= 180)
                    {
                        return 6;
                    }
                    else if (smoker == YesNoValueType.Yes && cholesterol >= 8 && SBPra >= 180)
                    {
                        return 7;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }

            //заглушка, пока не до конца готов алгоритм
            return 1;
        }
    }
}