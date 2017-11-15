using CardiologicalResearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CardiologicalResearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        
        public ActionResult Index()
        {
            ViewBag.Message = "Добро пожаловать";
            return View();
        }

        public ActionResult PersonalAccount()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            if (userId == -1)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult CommonStatistics()
        {
            return View();
        }

        public ActionResult AboutResearch()
        {
            return View();
        }

        public ActionResult PopulationCharacteristics()
        {
            return View();
        }

        public ActionResult CurrentMeasurements()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var userCurrentMeasurements = _repository.GetCurrentUserMeasurements(userId).ToList();
            return View(userCurrentMeasurements);
        }

        public ActionResult CurrentExaminationResults()
        {
            ViewBag.LastTestingDate = null;
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var userCurrentExamination = _repository.GetCurrentUserExamination(userId);
            var userCurrentExaminationResults = _repository.GetCurrentUserExaminationResults(userId);
            ViewBag.UserCurrentTestingDate = userCurrentExamination?.ExaminationDate;
            return View(userCurrentExaminationResults);
        }

        public ActionResult ClassificationTrees()
        {
            return View();
        }

        public ActionResult FeatureSelection()
        {
            return View();
        }

        public ActionResult Trees()
        {
            return View();
        }

        public ActionResult Predictors()
        {
            return View();
        }

        public ActionResult Glossary()
        {
            return View();
        }

        #region TODO

        //TODO: История заполнения данных - убрать представление 
        public ActionResult Measurements()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            List<DataView> userMeasurements;
            using (var db = new CardioContext())
            {
                //GetUserMeasurements
                userMeasurements = db.Database.SqlQuery<DataView>("Select * from [dbo].DataView ").AsQueryable().
                    Where(p => p.UserId == userId).ToList();

                ViewBag.ViewItems = userMeasurements;

            }
            return View(userMeasurements);
        }

        //TODO: История тестирования - убрать представление 
        public ActionResult ExaminationResults()
        {
            double armsIndex15Positive = 0;
            double armsIndex15Negative = 0;
            double legsIndex15Positive = 0;
            double legsIndex15Negative = 0;
            double sclerosisPositive = 0;
            double sclerosisNegative = 0;
            double ABIPositive = 0;
            double ABINegative = 0;

            var userId = WebSecurity.GetUserId(User.Identity.Name);

            List<ExaminationResultsView> userExaminationResults;

            using (var db = new CardioContext())
            {
                var allItems = db.Database.SqlQuery<ExaminationResultsView>("Select * from [dbo].ExaminationResultsView ").AsQueryable();

                userExaminationResults = allItems.Where(p => p.UserId == userId).ToList();

                var negativeTests = new List<String>();
                var negativeTestsCount = new List<int>();
                var positiveTests = new List<String>();
                var positiveTestsCount = new List<int>();
                var testName = new List<String>();
                var testCount = new List<int>();

                var allNegativeStatistics =
                    db.Database.SqlQuery<NegativeTestsCount>("Select * from [dbo].NegativeTestsCount ").AsQueryable();
                var allPositiveStatistics =
                    db.Database.SqlQuery<PositiveTestsCount>("Select * from [dbo].PositiveTestsCount ").AsQueryable();

                var negativeStatistics = allNegativeStatistics.Where(p => p.UserId == userId).ToList();
                var positiveStatistics = allPositiveStatistics.Where(p => p.UserId == userId).ToList();

                foreach (var test in negativeStatistics)
                {
                    negativeTests.Add(test.TestName.ToString());
                    negativeTestsCount.Add(test.TestCount);
                    if (test.TestName.ToString() == Constants.ArmsIndex15Name)
                    {
                        armsIndex15Negative = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.LegsIndex15Name)
                    {
                        legsIndex15Negative = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.SclerosisName)
                    {
                        sclerosisNegative = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.ABIName)
                    {
                        ABINegative = test.TestCount;
                    }
                }

                foreach (var test in positiveStatistics)
                {
                    positiveTests.Add(test.TestName.ToString());
                    positiveTestsCount.Add(test.TestCount);
                    if (test.TestName.ToString() == Constants.ArmsIndex15Name)
                    {
                        armsIndex15Positive = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.LegsIndex15Name)
                    {
                        legsIndex15Positive = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.SclerosisName)
                    {
                        sclerosisPositive = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.ABIName)
                    {
                        ABIPositive = test.TestCount;
                    }
                }

                if (armsIndex15Negative + armsIndex15Positive != 0)
                {
                    testName.Add(Constants.ArmsIndex15Name);
                    testCount.Add((int)(armsIndex15Positive / (armsIndex15Negative + armsIndex15Positive)) * 100);
                }

                if (legsIndex15Negative + legsIndex15Positive != 0)
                {
                    testName.Add(Constants.LegsIndex15Name);
                    testCount.Add((int)(legsIndex15Positive / (legsIndex15Negative + legsIndex15Positive) * 100));
                }

                if (sclerosisNegative + sclerosisPositive != 0)
                {
                    testName.Add(Constants.SclerosisName);
                    testCount.Add((int)(sclerosisPositive / (sclerosisNegative + sclerosisPositive) * 100));
                }

                if (ABINegative + ABIPositive != 0)
                {
                    testName.Add(Constants.ABIName);
                    testCount.Add((int)(ABIPositive / (ABINegative + ABIPositive) * 100));
                }

                ViewBag.ViewItems = userExaminationResults;

                ViewBag.TestName = testName;
                ViewBag.TestCount = testCount;

                ViewBag.NegativeTests = negativeTests;
                ViewBag.NegativeTestsCount = negativeTestsCount;

                ViewBag.PositiveTests = positiveTests;
                ViewBag.PositiveTestsCount = positiveTestsCount;

                ViewBag.AI15Positive = armsIndex15Positive / (armsIndex15Negative + armsIndex15Positive) * 100;
                ViewBag.LI15Positive = legsIndex15Positive / (legsIndex15Negative + legsIndex15Positive) * 100;
                ViewBag.SclerosisPositive = sclerosisPositive / (sclerosisNegative + sclerosisPositive) * 100;
                ViewBag.ABIPositive = ABIPositive / (ABINegative + ABIPositive) * 100;
            }
            return View(userExaminationResults);
        }

        //TODO: добавить новые методы репозитория
        public ActionResult ParametersGraphs()
        {
            double currentBMI = 0;
            var SCORE = 0;

            var userId = WebSecurity.GetUserId(User.Identity.Name);

            using (var db = new CardioContext())
            {
                //GetUserMeasurements
                var measurements = db.Measurements.Join(db.MedicalRecords, m => m.MedicalRecordId,
                    mr => mr.MedicalRecordId, (m, mr) => new { mr.UserId, mr.MedicalRecordDate, m.ParameterId, m.Value });
                var userMeasurements = measurements.Where(p => p.UserId == userId).ToList();

                double.TryParse(userMeasurements.OrderBy(p => p.MedicalRecordDate)
                    .Where(p => p.ParameterId == ParameterType.BMI)
                    .OrderByDescending(p => p.MedicalRecordDate).FirstOrDefault()?.Value, out currentBMI);

                ViewBag.BMI = currentBMI;

                Int32.TryParse(userMeasurements.OrderBy(p => p.MedicalRecordDate)
                    .Where(p => p.ParameterId == ParameterType.SCORE)
                    .OrderByDescending(p => p.MedicalRecordDate).FirstOrDefault()?.Value, out SCORE);

                ViewBag.SCORE = SCORE;
            }
            return View();
        }

        //TODO: переписать все запросы из представлений
        public ActionResult TestingsGraphs()
        {
            var armsIndex15Positive = 0.0;
            var armsIndex15Negative = 0.0;
            var legsIndex15Positive = 0.0;
            var legsIndex15Negative = 0.0;
            var sclerosisPositive = 0.0;
            var sclerosisNegative = 0.0;
            var ABIPositive = 0.0;
            var ABINegative = 0.0;
            List<ExaminationResultsView> items;

            var userId = WebSecurity.GetUserId(User.Identity.Name);

            using (var db = new CardioContext())
            {
                var allItems = db.Database
                    .SqlQuery<ExaminationResultsView>("Select * from [dbo].ExaminationResultsView ").AsQueryable();

                items = allItems.Where(p => p.UserId == userId).ToList();

                var negativeTestName = new List<String>();
                var negativeTestCount = new List<int>();

                var positiveTestName = new List<String>();
                var positiveTestCount = new List<int>();

                var testName = new List<String>();
                var testCount = new List<int>();

                var allNegativeStatistics =
                    db.Database.SqlQuery<NegativeTestsCount>("Select * from [dbo].NegativeTestsCount ").AsQueryable();
                var allPositiveStatistics =
                    db.Database.SqlQuery<PositiveTestsCount>("Select * from [dbo].PositiveTestsCount ").AsQueryable();

                var negativeStatistics = (from p in allNegativeStatistics where p.UserId == userId select p).ToList();
                var positiveStatistics = (from p in allPositiveStatistics where p.UserId == userId select p).ToList();

                foreach (var test in negativeStatistics)
                {
                    negativeTestName.Add(test.TestName.ToString());

                    negativeTestCount.Add(test.TestCount);
                    if (test.TestName.ToString() == Constants.ArmsIndex15Name)
                    {
                        armsIndex15Negative = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.LegsIndex15Name)
                    {
                        legsIndex15Negative = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.SclerosisName)
                    {
                        sclerosisNegative = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.ABIName)
                    {
                        ABINegative = test.TestCount;
                    }
                }

                foreach (var test in positiveStatistics)
                {
                    positiveTestName.Add(test.TestName.ToString());

                    positiveTestCount.Add(test.TestCount);
                    if (test.TestName.ToString() == Constants.ArmsIndex15Name)
                    {
                        armsIndex15Positive = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.LegsIndex15Name)
                    {
                        legsIndex15Positive = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.SclerosisName)
                    {
                        sclerosisPositive = test.TestCount;
                    }
                    else if (test.TestName.ToString() == Constants.ABIName)
                        ABIPositive = test.TestCount;
                }

                if (armsIndex15Negative + armsIndex15Positive != 0)
                {
                    testName.Add(Constants.ArmsIndex15Name);
                    testCount.Add((int)(armsIndex15Positive / (armsIndex15Negative + armsIndex15Positive) * 100));
                }

                if (legsIndex15Negative + legsIndex15Positive != 0)
                {
                    testName.Add(Constants.LegsIndex15Name);
                    testCount.Add((int)(legsIndex15Positive / (legsIndex15Negative + legsIndex15Positive) * 100));
                }

                if (sclerosisNegative + sclerosisPositive != 0)
                {
                    testName.Add(Constants.SclerosisName);
                    testCount.Add((int)(sclerosisPositive / (sclerosisNegative + sclerosisPositive) * 100));
                }

                if (ABINegative + ABIPositive != 0)
                {
                    testName.Add(Constants.ABIName);
                    testCount.Add((int)(ABIPositive / (ABINegative + ABIPositive) * 100));
                }

                ViewBag.ViewItems = items.ToList();

                ViewBag.TestName = testName;
                ViewBag.TestCount = testCount;

                ViewBag.negativeTestName = negativeTestName;
                ViewBag.negativeTestCount = negativeTestCount;

                ViewBag.positiveTestName = positiveTestName;
                ViewBag.positiveTestCount = positiveTestCount;

                ViewBag.AI15Positive = armsIndex15Positive / (armsIndex15Negative + armsIndex15Positive) * 100;
                ViewBag.LI15Positive = legsIndex15Positive / (legsIndex15Negative + legsIndex15Positive) * 100;
                ViewBag.SclerosisPositive = sclerosisPositive / (sclerosisNegative + sclerosisPositive) * 100;
                ViewBag.ABIPositive = ABIPositive / (ABINegative + ABIPositive) * 100;
            }
            return View(items.ToList());
        }

        #endregion
        
        public class DateTimeBinder : IModelBinder
        {
            //#region IModelBinder Members
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                DateTime dateTime;
                if (DateTime.TryParse(controllerContext.HttpContext.Request.QueryString["dateTime"], out dateTime))
                    return dateTime;
                //else
                return new DateTime();//or another appropriate default ;
            }
            // #endregion
        }

        
    }
}
