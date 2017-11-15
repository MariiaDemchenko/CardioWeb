using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CardiologicalResearch.Models;
using Newtonsoft.Json;

namespace CardiologicalResearch.Controllers
{
    public class MeasurementsController : ApiController
    {
        private readonly IRepository _repository;
        public int Id;

        public MeasurementsController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Post()
        {
            var content = await Request.Content.ReadAsStringAsync();
            var methodRequest = JsonConvert.DeserializeObject<MethodRequest<MeasurementsParams>>(content);

            if (methodRequest.Method == "measurements_all")
            {
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetAllMeasurements() });
                }
            }

            if (methodRequest.Method == "measurements_user")
            {
                if (methodRequest.Params?.UserId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetAllUserMeasurements((int)methodRequest.Params.UserId)});
                }
            }

            if (methodRequest.Method == "measurements_currentUser")
            {
                if (methodRequest.Params?.UserId!= null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetCurrentUserMeasurements((int)methodRequest.Params.UserId) });
                }
            }

            if (methodRequest.Method == "measurements_medicalRecord")
            {
                if (methodRequest.Params?.MedicalRecordId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetMeasurementsByMedicalRecordId((int)methodRequest.Params.MedicalRecordId) });
                }
            }

            if (methodRequest.Method == "measurements_item")
            {
                if (methodRequest.Params?.MeasurementId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetMeasurementById((int)methodRequest.Params.MeasurementId) });
                }
            }

            if (methodRequest.Method == "measurements_add")
            {
                if (methodRequest.Params?.MedicalRecordId != null && methodRequest.Params?.ParameterId!= null && methodRequest.Params.Value != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.AddMeasurement((int)methodRequest.Params.MedicalRecordId, (int)methodRequest.Params.ParameterId, methodRequest.Params.Value ) });
                }
            }

            if (methodRequest.Method == "measurements_edit")
            {
                if (methodRequest.Params?.MeasurementId != null && methodRequest.Params?.MedicalRecordId != null && methodRequest.Params?.ParameterId != null && methodRequest.Params.Value != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.EditMeasurement((int)methodRequest.Params.MeasurementId, (int)methodRequest.Params.MedicalRecordId, (int)methodRequest.Params.ParameterId, methodRequest.Params.Value) });
                }
            }

            if (methodRequest.Method == "measurements_delete")
            {
                if (methodRequest.Params?.MeasurementId != null)
                {
                    _repository.DeleteMeasurement((int)methodRequest.Params.MeasurementId);
                    return JsonConvert.SerializeObject(new { result = "success"});
                }
            }

            return JsonConvert.SerializeObject(new { result = _repository.GetCurrentUserMeasurements(1) });
        }
    }
}
