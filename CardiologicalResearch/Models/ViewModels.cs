using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardiologicalResearch.Models
{
        public class DataView
        {
            public int UserId { get; set; }
        public DateTime MedicalRecordDate {get; set;}
        public ParameterType ParameterId { get; set; }
        public string Value {get;set;}
        }

        public class ExaminationResultsView
        {
            public int UserId { get; set; }
            public DateTime TestingDate { get; set; }
            public TestType TestName { get; set; }
            public TestResultType ExaminationResult { get; set; }
        }

        public class PositiveTests
        {
            public int UserId { get; set; }
            public DateTime MedicalRecordDate { get; set; }
            public DateTime TestingDate { get; set; }
            public int PositiveCount { get; set; }
           
        }

        public class NegativeTests
        {
            public int UserId { get; set; }
            public DateTime MedicalRecordDate { get; set; }
            public DateTime TestingDate { get; set; }
            public int TestName { get; set; }

        }

        public class NegativeTestsCount
        {
            public int UserId { get; set; }
            public TestType TestName { get; set; }
            public int TestCount { get; set; }
        }

        public class PositiveTestsCount
        {
            public int UserId { get; set; }
            public TestType TestName { get; set; }
            public int TestCount { get; set; }
        }

        
}