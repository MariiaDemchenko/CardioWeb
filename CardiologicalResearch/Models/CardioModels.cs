using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;
using System.Runtime.Serialization;

namespace CardiologicalResearch.Models
{
    public class CardioContext : DbContext
    {
        public CardioContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<ParameterValue> ParameterValues { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<ExaminationResult> ExaminationResults { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<SCORE> SCOREs { get; set; }
        public DbSet<SCOREdata> SCOREdatas { get; set; }
    }

    public enum ParameterType
    {
        Age = 1,
        Gender = 2,
        Weight = 3,
        Height = 4,
        Waist = 5,
        BMI = 6,
        SBPra = 7,
        DBPra = 8,
        SBPll = 9,
        SBPrl = 10,
        Pulse = 11,
        CfPWV = 12,
        BaPWV = 13,
        CfPWV10 = 14,
        Glucose = 15,
        Cholesterol = 16,
        Stenocardia = 17,
        Diabetes = 18,
        SCORE = 19,
        Smoker = 20
    }

    public enum TestType
    {
        ArmsIndex15 = 1,
        LegsIndex15 = 2,
        ABI = 3,
        Sclerosis = 4
    }

    public enum TestResultType
    {
        Unknown = 1,
        Success = 2,
        Danger = 3,
        Error = 4
    }

    public enum GenderValueType
    {
        Female = 1,
        Male = 2
    }

    public enum YesNoValueType
    {
        Unknown = 1,
        Yes = 2,
        No = 3
    }

    public class ProjectInitializer : DropCreateDatabaseIfModelChanges<CardioContext>
    {
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
            context.ParameterValues.AddOrUpdate(x => x.ParameterValueId,
                new ParameterValue()
                {
                    ParameterValueId = 1,
                    ParameterId = ParameterType.Gender,
                    IntValue = (int) GenderValueType.Female,
                    StringValue = Constants.Female
                },
                new ParameterValue()
                {
                    ParameterValueId = 2,
                    ParameterId = ParameterType.Gender,
                    IntValue = (int) GenderValueType.Male,
                    StringValue = Constants.Male
                },
                new ParameterValue()
                {
                    ParameterValueId = 3,
                    ParameterId = ParameterType.Diabetes,
                    IntValue = (int) YesNoValueType.Yes,
                    StringValue = Constants.Yes
                },
                new ParameterValue()
                {
                    ParameterValueId = 4,
                    ParameterId = ParameterType.Diabetes,
                    IntValue = (int) YesNoValueType.No,
                    StringValue = Constants.No
                },
                new ParameterValue()
                {
                    ParameterValueId = 5,
                    ParameterId = ParameterType.Stenocardia,
                    IntValue = (int) YesNoValueType.Yes,
                    StringValue = Constants.Yes
                },
                new ParameterValue()
                {
                    ParameterValueId = 6,
                    ParameterId = ParameterType.Stenocardia,
                    IntValue = (int) YesNoValueType.No,
                    StringValue = Constants.No
                });
        }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    [Table("MedicalRecord")]
    public class MedicalRecord
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MedicalRecordId { get; set; }
        public DateTime MedicalRecordDate { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual UserProfile User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Measurement> Measurements { get; set; }
        [JsonIgnore]
        public virtual ICollection<Examination> Examinations { get; set; }
    }

    [Table("Examination")]
    public class Examination
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ExaminationId { get; set; }
        public DateTime ExaminationDate { get; set; }
        public int MedicalRecordId { get; set; }

        [JsonIgnore]
        public virtual MedicalRecord MedicalRecord { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExaminationResult> ExaminationResults { get; set; }

    }

    [Table("ExaminationResult")]
    public class ExaminationResult
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ExaminationResultId { get; set; }
        public int ExaminationId { get; set; }
        public TestType TestId { get; set; }
        public TestResultType Result { get; set; }

        [JsonIgnore]
        public virtual Examination Examination { get; set; }
    }

    [Table("Test")]
    public class Test
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public TestType TestId { get; set; }
        public string Name { get; set; }
    }

    [Table("Parameter")]
    public class Parameter
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public ParameterType ParameterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [Table("TestResult")]
    public class TestResult
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public TestResultType TestResultId { get; set; }
        public string Name { get; set; }
    }

    [Table("ParameterValue")]
    public class ParameterValue
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ParameterValueId { get; set; }
        public ParameterType ParameterId { get; set; }
        public int IntValue { get; set; }
        public string StringValue { get; set; }

        public virtual Parameter Parameter { get; set; }
    }

    [Table("Measurement")]
    public class Measurement
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MeasurementId { get; set; }
        [Display(Name = "Название показателя")]
        public ParameterType ParameterId { get; set; }
        public string Value { get; set; }
        public int MedicalRecordId { get; set; }

        [JsonIgnore]
        public virtual MedicalRecord MedicalRecord { get; set; }
        [JsonIgnore]
        public virtual Parameter Parameter { get; set; }
    }

    [Table("SCORE")]
    public class SCORE
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SCOREId { get; set; }

        public GenderValueType Gender { get; set; }
        public YesNoValueType IsSmoker { get; set; }
        public int MinAge;
        public int MaxAge;

        public int MinBP;
        public int MaxBP;

        public double MinCholesterol;
        public double MaxCholesterol;

        public int SCORElevel;
    }

    [Table("SCOREdata")]
    public class SCOREdata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SCOREDataId { get; set; }

        public GenderValueType Gender { get; set; }
        public YesNoValueType IsSmoker { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public int MinwBP { get; set; }
        public int MaxBP { get; set; }

        public double MinCholesterol { get; set; }
        public double MaxCholesterol { get; set; }

        public int SCORElevel { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
