using CardiologicalResearch.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace CardiologicalResearch.Controllers
{
    public class ExaminationsController : ApiController
    {
        private readonly IRepository _repository;
        public int Id;

        public ExaminationsController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Post()
        {
            var content = await Request.Content.ReadAsStringAsync();
            var methodRequest = JsonConvert.DeserializeObject<MethodRequest<ExaminationsParams>>(content);

            if (methodRequest.Method == "examinations_all")
            {
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetAllExaminations() });
                }
            }

            if (methodRequest.Method == "examinations_includeMedicalRecords")
            {
                return JsonConvert.SerializeObject(new { result = _repository.GetAllExamination2MedicalRecords() });
            }

            if (methodRequest.Method == "examinations_item")
            {
                if (methodRequest.Params?.ExaminationId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetExaminationById((int)methodRequest.Params.ExaminationId) });
                }
            }

            if (methodRequest.Method == "examinations_add")
            {
                if (methodRequest.Params?.ExaminationDate != null && methodRequest.Params?.MedicalRecordId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.AddExamination((int)methodRequest.Params.MedicalRecordId, (DateTime)methodRequest.Params.ExaminationDate) });
                }
            }

            if (methodRequest.Method == "examinations_edit")
            {
                if (methodRequest.Params?.ExaminationId != null && methodRequest.Params?.ExaminationDate != null && methodRequest.Params?.MedicalRecordId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.EditExamination((int)methodRequest.Params.ExaminationId, (int)methodRequest.Params.MedicalRecordId, (DateTime)methodRequest.Params.ExaminationDate) });
                }
            }

            //if (methodRequest.Method == "examinations_delete")
            //{
            //    if (methodRequest.Params?.ExaminationId != null)
            //    {
            //        return JsonConvert.SerializeObject(new { result = _repository.DeleteExamination((int)methodRequest.Params.ExaminationId) });
            //    }
            //}

            //if (methodRequest.Method == "measurements_edit")
            //{
            //    if (methodRequest.Params?.MeasurementId != null && methodRequest.Params?.MedicalRecordId != null && methodRequest.Params?.ParameterId != null && methodRequest.Params.Value != null)
            //    {
            //        return JsonConvert.SerializeObject(new { result = _repository.EditMeasurement((int)methodRequest.Params.MeasurementId, (int)methodRequest.Params.MedicalRecordId, (int)methodRequest.Params.ParameterId, methodRequest.Params.Value) });
            //    }
            //}

            //if (methodRequest.Method == "measurements_delete")
            //{
            //    if (methodRequest.Params?.MeasurementId != null)
            //    {
            //        _repository.DeleteMeasurement((int)methodRequest.Params.MeasurementId);
            //        return JsonConvert.SerializeObject(new { result = "success" });
            //    }
            //}

            return JsonConvert.SerializeObject(new { result = _repository.GetCurrentUserMeasurements(1) });
        }
    }
}
