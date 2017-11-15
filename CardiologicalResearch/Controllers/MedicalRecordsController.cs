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
    public class MedicalRecordsController : ApiController
    {
        private readonly IRepository _repository;
        public int Id;

        public MedicalRecordsController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Post()
        {
            var content = await Request.Content.ReadAsStringAsync();
            var methodRequest = JsonConvert.DeserializeObject<MethodRequest<MedicalRecordsParams>>(content);

            if (methodRequest.Method == "medicalRecords_all")
            {
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetAllMedicalRecords() });
                }
            }

            if (methodRequest.Method == "medicalRecords_user")
            {
                if (methodRequest.Params?.UserId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetAllUserMedicalRecords((int)methodRequest.Params.UserId) });
                }
            }

            if (methodRequest.Method == "medicalRecords_currentUser")
            {
                if (methodRequest.Params?.UserId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetCurrentUserMedicalRecord((int)methodRequest.Params.UserId) });
                }
            }

            if (methodRequest.Method == "medicalRecords_includeUsers")
            {
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetAllMedicalRecord2Users() });
                }
            }

            if (methodRequest.Method == "medicalRecords_last")
            {
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetLastMedicalRecord() });
                }
            }

            if (methodRequest.Method == "medicalRecords_item")
            {
                if (methodRequest.Params?.MedicalRecordId != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.GetMedicalRecordById((int)methodRequest.Params.MedicalRecordId) });
                }
            }

            if (methodRequest.Method == "medicalRecords_addUser")
            {
                if (methodRequest.Params?.UserId != null )
                {
                    return JsonConvert.SerializeObject(new { result = _repository.AddUserMedicalRecord((int)methodRequest.Params.UserId) });
                }
            }

            if (methodRequest.Method == "medicalRecords_add")
            {
                if (methodRequest.Params?.UserId != null && methodRequest.Params?.MedicalRecordDate != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.AddMedicalRecord((int)methodRequest.Params.UserId, (DateTime)methodRequest.Params?.MedicalRecordDate) });
                }
            }

            if (methodRequest.Method == "medicalRecords_edit")
            {
                if (methodRequest.Params?.MedicalRecordId != null && methodRequest.Params?.UserId != null && methodRequest.Params?.MedicalRecordDate != null)
                {
                    return JsonConvert.SerializeObject(new { result = _repository.EditMedicalRecord((int)methodRequest.Params.MedicalRecordId, (int)methodRequest.Params.UserId, (DateTime)methodRequest.Params?.MedicalRecordDate) });
                }
            }

            if (methodRequest.Method == "medicalRecords_delete")
            {
                if (methodRequest.Params?.MedicalRecordId != null)
                {
                    _repository.DeleteMedicalRecord((int)methodRequest.Params.MedicalRecordId);
                    return JsonConvert.SerializeObject(new { result = "success" });
                }
            }

            return JsonConvert.SerializeObject(new { result = _repository.GetCurrentUserMedicalRecord(1) });
        }
    }
}
