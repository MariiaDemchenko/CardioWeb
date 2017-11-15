using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CardiologicalResearch.Models
{
    public class MethodRequest<T> where T : class
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public T Params { get; set; }
    }

    public class IdParams
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class LoginParams
    {
        [JsonProperty("login")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class MeasurementsParams
    {
        [JsonProperty("measurementId")]
        public int? MeasurementId { get; set; }

        [JsonProperty("userId")]
        public int? UserId { get; set; }

        [JsonProperty("medicalRecordId")]
        public int? MedicalRecordId { get; set; }

        [JsonProperty("parameterId")]
        public int? ParameterId { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class MedicalRecordsParams
    {
        [JsonProperty("medicalRecordId")]
        public int? MedicalRecordId { get; set; }

        [JsonProperty("medicalRecordDate")]
        public DateTime? MedicalRecordDate { get; set; }

        [JsonProperty("userId")]
        public int? UserId { get; set; }
    }

    public class ExaminationsParams
    {
        [JsonProperty("userId")]
        public int? UserId { get; set; }

        [JsonProperty("examinationId")]
        public int? ExaminationId { get; set; }

        [JsonProperty("examinationDate")]
        public DateTime? ExaminationDate { get; set; }

        [JsonProperty("medicalRecordId")]
        public int? MedicalRecordId { get; set; }
    }

    public class ExaminationResultsParams
    {
        [JsonProperty("examinationResultId")]
        public int? ExaminationResultId { get; set; }

        [JsonProperty("examinationId")]
        public int? ExaminationId { get; set; }

        [JsonProperty("testId")]
        public int? TestId { get; set; }

        [JsonProperty("result")]
        public int? Result { get; set; }
    }
}