using CardiologicalResearch.Models;

namespace CardiologicalResearch.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CardiologicalResearch.Models.CardioContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CardioContext context)
        {
            context.Parameters.AddOrUpdate(x => x.ParameterId,
                new Parameter()
                {
                    ParameterId = ParameterType.Age,
                    Name = Constants.AgeName,
                    Description = Constants.AgeDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Gender,
                    Name = Constants.GenderName,
                    Description = Constants.GenderDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Weight,
                    Name = Constants.WeightName,
                    Description = Constants.WeightDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Height,
                    Name = Constants.HeightName,
                    Description = Constants.HeightDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.BMI,
                    Name = Constants.BMIName,
                    Description = Constants.BMIDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.SBPra,
                    Name = Constants.SBPllName,
                    Description = Constants.SBPraDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.DBPra,
                    Name = Constants.DBPraName,
                    Description = Constants.DBPraDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.SBPll,
                    Name = Constants.SBPllName,
                    Description = Constants.SBPllDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.SBPrl,
                    Name = Constants.SBPrlName,
                    Description = Constants.SBPrlDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Pulse,
                    Name = Constants.PulseName,
                    Description = Constants.PulseDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.CfPWV,
                    Name = Constants.CfPWVName,
                    Description = Constants.CfPWVDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.BaPWV,
                    Name = Constants.BaPWVName,
                    Description = Constants.BaPWVDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Age,
                    Name = Constants.AgeName,
                    Description = Constants.AgeDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.CfPWV10,
                    Name = Constants.CfPWV10Name,
                    Description = Constants.CfPWV10Description
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Glucose,
                    Name = Constants.GlucoseName,
                    Description = Constants.GlucoseDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Cholesterol,
                    Name = Constants.CholesterolName,
                    Description = Constants.CholesterolDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Stenocardia,
                    Name = Constants.StenocardiaName,
                    Description = Constants.StenocardiaDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Diabetes,
                    Name = Constants.DiabetesName,
                    Description = Constants.DiabetesDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.SCORE,
                    Name = Constants.SCOREName,
                    Description = Constants.SCOREDescription
                },
                new Parameter()
                {
                    ParameterId = ParameterType.Smoker,
                    Name = Constants.SmokeName,
                    Description = Constants.SmokeDescription
                });

            context.Tests.AddOrUpdate(x => x.TestId,
                new Test()
                {
                    TestId = TestType.ArmsIndex15,
                    Name = Constants.ArmsIndex15Name
                },
                new Test()
                {
                    TestId = TestType.LegsIndex15,
                    Name = Constants.LegsIndex15Name
                },
                new Test()
                {
                    TestId = TestType.ABI,
                    Name = Constants.ABIName
                },
                new Test()
                {
                    TestId = TestType.Sclerosis,
                    Name = Constants.SclerosisName
                });
            context.TestResults.AddOrUpdate(x => x.TestResultId,
                new TestResult()
                {
                    TestResultId = TestResultType.Unknown,
                    Name = Constants.Unknown
                },
                new TestResult()
                {
                    TestResultId = TestResultType.Success,
                    Name = Constants.Success
                },
                new TestResult()
                {
                    TestResultId = TestResultType.Danger,
                    Name = Constants.Danger
                },
                new TestResult()
                {
                    TestResultId = TestResultType.Error,
                    Name = Constants.Error
                });
        }
    }
}
